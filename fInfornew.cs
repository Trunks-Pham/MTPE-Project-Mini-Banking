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
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;
using Mini_Banking.DTO;
using Mini_Banking.DAO;

namespace Mini_Banking
{
    public partial class fInfornew : Form
    {
        private Customer customer;
        private CustomerAccount customerAccount;

        public Customer Customer { get => customer; set => customer = value; }
        public CustomerAccount CustomerAccount { get => customerAccount; set => customerAccount = value; }

        public fInfornew(Customer customer, CustomerAccount customerAccount)
        {
            InitializeComponent();
            Customer = customer;
            CustomerAccount = customerAccount;
        }

        private void pbBack_Click(object sender, EventArgs e)
        {
            this.Hide();
            this.Close();
            fSetting fSetting = new fSetting(customer, customerAccount);
            fSetting.ShowDialog();
            this.Show();
        }

        private void btnOTP_Click(object sender, EventArgs e)
        {
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
                MessageBox.Show("Gửi Email Thành Công!", "Thông Báo");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi Khi Gửi Email: {ex.Message}", "Thông Báo!");
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


        private void pbHide_Click(object sender, EventArgs e)
        {
            txbPassword.UseSystemPasswordChar = true;
            pbHide.Visible = false;
            pbView.Visible = true;
        }

        private void pbView_Click(object sender, EventArgs e)
        {
            txbPassword.UseSystemPasswordChar = false;
            pbView.Visible = false;
            pbHide.Visible = true;
        }

        private void txbOTP_TextChanged(object sender, EventArgs e)
        {
            txbOTP.RightToLeft = RightToLeft.Yes;
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

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            string customerID = Customer.Id;
            string address = txbAddress.Text;
            string email = txbEmail.Text;
            string opt = txbOTP.Text;
            string password = txbPassword.Text;


            // Kiểm tra xem người dùng đã nhập OTP chưa
            if (string.IsNullOrEmpty(txbOTP.Text))
            {
                MessageBox.Show("Vui Lòng Nhập Mã OTP Đã Được Gửi Đến Email Của Bạn.", "Thông Báo");
                return;
            }

            // Kiểm tra OTP hợp lệ hay không
            if (!VerifyOTP())
            {
                MessageBox.Show("Mã OTP Không Chính Xác!", "Thông Báo");
                return;
            }

            //Kiểm tra mật khẩu có đúng hay không
            if (!CustomerLoginDAO.Instance.Checkpassword(CreateMD5(password), Customer.LoginName))
            {
                MessageBox.Show("Mật Khẩu Không Chính Xác", "Cảnh Báo Bảo Mật");
            }

            // Kiểm tra xem người dùng đã đồng ý với các điều khoản chưa
            if (!ckbAgree.Checked)
            {
                MessageBox.Show("Vui Lòng Đồng Ý Với Các Điều Khoản Bằng Cách Tích Vào Tôi Đồng Ý!", "Thông Báo!");
                return;
            }

            // Cập nhật thông tin
            Customer.Address = address;
            if (CustomerDAO.Instance.UpdateInfor(Customer))
            {
                MessageBox.Show("Hoàn Tất Cập Nhật Thông Tin", "Thông Báo Cập Nhật Thông Tin",MessageBoxButtons.OK);
                this.Hide();
                fInforView fInfor = new fInforView(customer,customerAccount);
                fInfor.ShowDialog();

            }


        }

        private void fInfornew_Load(object sender, EventArgs e)
        {
            LoadInfor();
        }

        void LoadInfor()
        {
            txbEmail.Text = customer.Email;
        }
    }
}
