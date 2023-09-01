using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using Mini_Banking.DTO;
using Mini_Banking.DAO;
using System.Security.Cryptography;

namespace Mini_Banking
{
    public partial class fSignup : Form
    {
        public fSignup()
        {
            InitializeComponent();
            LimitInput();
        }

        // Giới hạn CCCD và SĐT là 10 số và chỉ được nhập số
        private void LimitInput()
        {
            txbID.MaxLength = 12;
            txbNumphone.MaxLength = 10;
            txbOTP.MaxLength = 6;
        }

        // Chỉ được nhập số tại SDT - CCCD
        private void Onlynum_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        //Chỉ được nhập chữ tại Tên
        private void Onlystring_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsLetter(e.KeyChar) && !char.IsControl(e.KeyChar) && !char.IsWhiteSpace(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void btnOTP_Click(object sender, EventArgs e)
        {
            //Kiểm tra Email
            if (CustomerDAO.Instance.CheckEmail(txbEmail.Text))
            {
                MessageBox.Show("Email này đã được đăng ký!\nVui lòng sử dụng email khác!", "Thông báo");
                return;
            }

            // Tạo một số ngẫu nhiên 6 chữ số
            Random rnd = new Random();
            int randomNumber = rnd.Next(100000, 999999);
            txbOTP.Tag = randomNumber.ToString();

            // Thiết lập chi tiết email
            string to = txbEmail.Text; // Lấy từ txbEmail mà khách nhập
            string from = "minibanking.project1@gmail.com";
            string subject = "MiniBank - Ngân Hàng Công Nghệ Số";
            string body = $"Mã xác minh của bạn là: {randomNumber}";

            // Thiết lập client SMTP
            SmtpClient client = new SmtpClient("smtp.gmail.com", 587);
            client.EnableSsl = true;
            client.UseDefaultCredentials = false;
            client.Credentials = new NetworkCredential(from, "ovtyaozbxzzohcic");

            // Tạo tin nhắn email
            MailMessage message = new MailMessage(from, to);
            message.Subject = subject;
            message.Body = body;

            // Gửi email
            try
            {
                client.Send(message);
                MessageBox.Show("Gửi Email Thành Công!");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi Khi Gửi Email: {ex.Message}");
            }
        }

        //So sánh OTP từ Server gửi về Client và OTP khách hàng nhập ở textBox
        private bool VerifyOTP()
        {
            // So sánh mã OTP
            if (txbOTP.Text == txbOTP.Tag.ToString())
            {
                return true;
            }
            return false;
        }

        private string GenerateAccountNumber(DateTime dateOfBirth, string id)
        {
            // Trích xuất tháng và ngày từ ngày sinh
            int month = dateOfBirth.Month;
            int day = dateOfBirth.Day;

            // Lấy 4 chữ số cuối của id
            string lastFourDigitsOfId = id.Substring(id.Length - 4);

            // Kết hợp "2301", tháng, ngày và 4 chữ số cuối của id thành một chuỗi duy nhất
            string accountNumber = $"2301{month:00}{day:00}{lastFourDigitsOfId}";

            return accountNumber;
        }

        public string CreateMD5(string input)
        {
            using (var provider = System.Security.Cryptography.MD5.Create())
            {
                StringBuilder builder = new StringBuilder();
                foreach (byte b in provider.ComputeHash(Encoding.UTF8.GetBytes(input)))
                    builder.Append(b.ToString("x2").ToLower());
                return builder.ToString();
            }
        }

        private void btnSignInfor_Click(object sender, EventArgs e)
        {
            // Kiểm tra xem người dùng đã nhập OTP chưa
            if (string.IsNullOrEmpty(txbOTP.Text))
            {
                MessageBox.Show("Vui lòng nhập mã OTP đã được gửi đến email của bạn.", "Thông Báo");
                return;
            }

            // Kiểm tra xem người dùng đã đồng ý với các điều khoản chưa
            if (!ckbAgree.Checked)
            {
                MessageBox.Show("Vui lòng đồng ý với các điều khoản bằng cách tích vào hộp kiểm.", "Thông Báo");
                return;
            }

            // Kiểm tra mật khẩu 1 và mật khẩu 2 có trùng khớp không
            string password1 = txbPasswordUp1.Text;
            string password2 = txbPasswordUp2.Text;

            if (password1 != password2)
            {
                MessageBox.Show("Mật khẩu không trùng khớp. Vui lòng nhập lại.", "Cảnh Báo Bảo Mật");
                return;
            }

            // Check tên đăng nhập
            if (CustomerLoginDAO.Instance.CheckUserName(xtbUsernameUp.Text))
            {
                MessageBox.Show("Tên đăng nhập đã tồn tại! Vui lòng nhập tên khác!", "Cảnh Báo Bảo Mật");
                return;
            }
            if (!VerifyOTP())
            {
                MessageBox.Show("Mã OTP không chính xác!", "Cảnh Báo Bảo Mật");
                return;
            }
            // Tạo đối tượng
            CustomerLogin customerLogin = new CustomerLogin(xtbUsernameUp.Text, CreateMD5(password1), txbID.Text);

            if (SignUp(customerLogin))
            {
                Customer customer = new(customerLogin.LoginName, txbID.Text, txbName.Text, dtpDateOfBirth.Value, cbGender.Text, txbEmail.Text, txbNumphone.Text, txbAddress.Text);
                if (CustomerDAO.Instance.AddNewCustomer(customer))
                {
                    // Tạo customer account - STK
                    CustomerAccount customerAccount = new CustomerAccount(GenerateAccountNumber(customer.DateOfBirth, customer.Id), customer.Id);

                    if (CustomerAccountDAO.Instance.AddNewAccountNum(customerAccount))
                    {

                        MessageBox.Show("Bạn Đã Đăng Ký Tài Khoản Thành Công!\nVui Lòng Thực Hiện Đăng Nhập!", "Thông Báo!");
                        this.Hide();
                        this.Close();
                        fSiginIn fSiginIn = new fSiginIn();
                        fSiginIn.ShowDialog();
                        this.Show();
                    }
                }

            }
            else
            {
                MessageBox.Show("Đăng Ký Không Thành Công!\nVui Lòng Kiểm Tra và Đăng Ký Lại!", "Thông Báo");
                return;
            }
        }


        private void pbView1_Click(object sender, EventArgs e)
        {
            txbPasswordUp1.UseSystemPasswordChar = false;
            pbView1.Visible = false;
            pbHide1.Visible = true;
        }

        private void pbHide1_Click(object sender, EventArgs e)
        {
            txbPasswordUp1.UseSystemPasswordChar = true;
            pbView1.Visible = true;
            pbHide1.Visible = false;
        }

        private void pbView2_Click(object sender, EventArgs e)
        {
            txbPasswordUp2.UseSystemPasswordChar = false;
            pbView2.Visible = false;
            pbHide2.Visible = true;
        }

        private void pbHide2_Click(object sender, EventArgs e)
        {
            txbPasswordUp2.UseSystemPasswordChar = true;
            pbView2.Visible = true;
            pbHide2.Visible = false;
        }

        bool CheckInforSignUp()
        {
            string id = txbID.Text;
            string name = txbName.Text;
            DateTime dateOfBirth = dtpDateOfBirth.Value;
            string gender = cbGender.Text;
            string phoneNumber = txbNumphone.Text;
            string email = txbEmail.Text;
            string address = txbAddress.Text;

            if (CustomerDAO.Instance.CheckIDandPhoneNumber(id, phoneNumber))
            {
                MessageBox.Show(" Số CCCD or SĐT đã tồn tại !", "Thông Báo!");
                return false;
            }
            if (txbPasswordUp1.Text.Length < 8)
            {
                MessageBox.Show("Mật khẩu tối thiểu 8 ký tự! \nVui lòng kiểm tra và nhập lại!", "Cảnh Báo Bảo Mật");
            }


            if (!CheckBirthday(dateOfBirth))
            {
                MessageBox.Show(" Bạn chưa đủ 18 tuổi để đăng ký!", "Thông Báo!");
                return false;
            }


            if (!email.Contains('@') && !email.Contains('.'))
            {
                MessageBox.Show("Email không hợp lệ!\nVui lòng kiểm tra và nhập lại!", "Thông báo!");
                return false;
            }
            return true;
        }

        bool CheckBirthday(DateTime birthday)
        {
            // Tính tuổi
            int now = DateTime.Now.Year;
            int dob = birthday.Year;
            int age = now - dob;
            if (age < 18)
            {
                return false;
            }
            return true;
        }

        bool SignUp(CustomerLogin customerLogin)
        {
            if (CheckInforSignUp())
            {
                return CustomerLoginDAO.Instance.SignUp(customerLogin.Password, customerLogin.LoginName);
            }
            return false;
        }
    }
}