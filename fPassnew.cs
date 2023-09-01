using Mini_Banking.DTO;
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
using Mini_Banking.DAO;

namespace Mini_Banking
{
    public partial class fPassnew : Form
    {
        private Customer customer;
        private CustomerAccount customerAccount;

        public Customer Customer { get => customer; set => customer = value; }
        public CustomerAccount CustomerAccount { get => customerAccount; set => customerAccount = value; }

        public fPassnew(CustomerAccount customerAccount, Customer customer)
        {
            InitializeComponent();
            CustomerAccount = customerAccount;
            Customer = customer;
        }

        private void pbBack_Click(object sender, EventArgs e)
        {
            this.Hide();
            this.Close();
            fSetting fSetting = new fSetting(customer, customerAccount);
            fSetting.ShowDialog();
            this.Show();
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

        private void btnSendotp_Click(object sender, EventArgs e)
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

        private void btnUpdatepass_Click(object sender, EventArgs e)
        {
            string oldpassword = txbOldPass.Text;
            string newpassword = txbNewpass1.Text;
            string newpassword1 = txbNewpass2.Text;
            string email = txbEmail.Text;
            string otp = txbOTP.Text;

            // Kiểm tra mật khẩu cũ có đúng hay không
            if (!CustomerLoginDAO.Instance.Checkpassword(CreateMD5(oldpassword), Customer.LoginName))
            {
                // Nếu không khớp, hiển thị thông báo lỗi và yêu cầu người dùng nhập lại
                MessageBox.Show("Mật khẩu cũ không chính xác!\nVui lòng thử lại!", "Cảnh Báo Bảo Mật");
                return;
            }

            // Kiểm tra mật khẩu mới 1 và mật khẩu mới 2 có trùng khớp không
            if (newpassword != newpassword1)
            {
                MessageBox.Show("Mật khẩu không trùng khớp. Vui lòng nhập lại.", "Cảnh Báo Bảo Mật");
                return;
            }

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

            // Kiểm tra xem người dùng đã đồng ý với các điều khoản chưa
            if (!cbAgree.Checked)
            {
                MessageBox.Show("Vui Lòng Đồng Ý Với Các Điều Khoản Bằng Cách Tích Vào Tôi Xác Nhận!", "Thông Báo!");
                return;
            }

            // CẬP NHẬT MẬT KHẨU MỚI
            if (CustomerLoginDAO.Instance.ChangePassword(CreateMD5(newpassword), Customer.LoginName))
            {
                MessageBox.Show("Chúc bạn đăng nhập lại thành công !", "Thông báo bảo mật");
                Close();
            }
            else
            {
                MessageBox.Show("Đổi mật khẩu thất bại!", "Thông báo bảo mật");
                return;
            }

        }

        private void labelInforPass_Click(object sender, EventArgs e)
        {
            MessageBox.Show(
                "1. Phòng tránh một số nguy hiểm về bảo mật." +
                "\n\n2. Đảm bảo rằng ai đó không thể lấy được mật khẩu của\n    bạn và sử dụng nó cho mục đích xấu." +
                "\n\n3. Ngăn chặn việc người khác vào tài khoản của bạn." +
                "\n\n4. Ngăn chặn việc sử dụng mật khẩu đã lưu." +
                "\n\n5. Giới hạn quyền truy cập do keylogger thu được." +
                "\n\n\nTuy Nhiên ! \n\nViệc thay đổi mật khẩu định kỳ cũng có vài nhược điểm. " +
                "\n\nViệc thay đổi mật khẩu sẽ khiến người dùng khó nhớ. " +
                "\n\nThay vì tạo ra một mật khẩu mạnh và ghi nhớ nó, bạn phải\ncố gắng nhớ mật khẩu mới sau mỗi vài tháng. " +
                "\n\nNgười dùng có thể sử dụng các ký tự để đổi mật khẩu\nví dụ như Aloi0921@, Khongnhonua@132, v.v.. " +
                "\n\nViệc phải thay đổi mật khẩu định kỳ cho nhiều tài khoản\nvà luôn nhớ các mật khẩu duy nhất,và mật khẩu mạnh \ncho nhiều dịch vụ là rất khó." +
                "\n\nVì vậy, việc thay đổi mật khẩu định kỳ là một thực hành tốt\nnhưng quan trọng là cân bằng lợi ích với những nhược điểm.",
                "Những Lý Do Bạn Nên Thay Đổi Mật Khẩu Định Kỳ !");
        }

        private void pbOldview_Click(object sender, EventArgs e)
        {
            txbOldPass.UseSystemPasswordChar = false;
            pbOldview.Visible = false;
            pbOldhide.Visible = true;
        }

        private void pbOldhide_Click(object sender, EventArgs e)
        {
            txbOldPass.UseSystemPasswordChar = true;
            pbOldview.Visible = true;
            pbOldhide.Visible = false;
        }

        private void pbNewview1_Click(object sender, EventArgs e)
        {
            txbNewpass1.UseSystemPasswordChar = false;
            pbNewview1.Visible = false;
            pbNewhide1.Visible = true;
        }

        private void pbNewhide1_Click(object sender, EventArgs e)
        {
            txbNewpass1.UseSystemPasswordChar = true;
            pbNewview1.Visible = true;
            pbNewhide1.Visible = false;
        }

        private void pbNewview2_Click(object sender, EventArgs e)
        {
            txbNewpass2.UseSystemPasswordChar = false;
            pbNewview2.Visible = false;
            pbNewhide2.Visible = true;
        }

        private void pbNewhide2_Click(object sender, EventArgs e)
        {
            txbNewpass2.UseSystemPasswordChar = true;
            pbNewview2.Visible = true;
            pbNewhide2.Visible = false;
        }

        private void fPassnew_Load(object sender, EventArgs e)
        {
            LoadInFor();
        }

        void LoadInFor()
        {
            txbEmail.Text = customer.Email;
        }
    }
}
