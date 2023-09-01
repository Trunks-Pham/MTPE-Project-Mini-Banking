using Mini_Banking.DAO;
using Mini_Banking.DTO;
using System.Globalization;
using System.Security.Cryptography;
using System.Text;

namespace Mini_Banking
{
    public partial class fSiginIn : Form
    {
        public fSiginIn()
        {
            InitializeComponent();
        }

        private void fSiginIn_FormClosing(object sender, FormClosingEventArgs e)
        {
            Thread.CurrentThread.CurrentUICulture = new CultureInfo("vi-VN");
            if (MessageBox.Show("Bạn Có Thật Sự Muốn Thoát Không ?", "Thông Báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
            {
                e.Cancel = true;
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
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

        private void btnSignin_Click(object sender, EventArgs e)
        {
            string loginName = txbUserName.Text;
            string password = txbPassword.Text;



            if (CustomerLoginDAO.Instance.Login(loginName, CreateMD5(password)))
            {
                // Thực hiện các thao tác tiếp theo sau khi đăng nhập thành công

                Customer customer = CustomerDAO.Instance.GetCustomer(loginName);
                CustomerAccount customerAccount = CustomerAccountDAO.Instance.GetCustomerAccount(customer.Id);

                this.Hide();
                fHome fHome = new fHome(customer, customerAccount);
                fHome.ShowDialog();
                this.Show();
            }
            else
            {
                // Nếu không khớp, hiển thị thông báo lỗi và yêu cầu người dùng nhập lại
                MessageBox.Show("Tên đăng nhập hoặc mật khẩu không đúng!\nVui lòng thử lại!","Cảnh Báo Bảo Mật!");
            }
        }

        private void pbView_Click(object sender, EventArgs e)
        {
            txbPassword.UseSystemPasswordChar = false;
            pbView.Visible = false;
            pbHide.Visible = true;
        }

        private void pbHide_Click(object sender, EventArgs e)
        {
            txbPassword.UseSystemPasswordChar = true;
            pbHide.Visible = false;
            pbView.Visible = true;
        }

        private void btnSignup_Click(object sender, EventArgs e)
        {
            fSignup fSignup = new fSignup();
            this.Hide();
            fSignup.ShowDialog();
            this.Show();
        }
    }
}