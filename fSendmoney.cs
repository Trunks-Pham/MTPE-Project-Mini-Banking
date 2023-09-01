using Mini_Banking.DAO;
using Mini_Banking.DTO;
using QRCoder;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

namespace Mini_Banking
{
    public partial class fSendmoney : Form
    {
        private Customer customer;
        private CustomerAccount customerAccount;
        public fSendmoney(CustomerAccount customerAccount, Customer customer)
        {
            InitializeComponent();
            Customer = customer;
            CustomerAccount = customerAccount;
        }

        public Customer Customer { get => customer; set => customer = value; }
        public CustomerAccount CustomerAccount { get => customerAccount; set => customerAccount = value; }

        private void pbBack_Click(object sender, EventArgs e)
        {
            this.Hide();
            this.Close();
            fHome fHome = new fHome(customer, customerAccount);
            fHome.ShowDialog();
            this.Show();
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

        private void txbMoney_TextChanged(object sender, EventArgs e)
        {
            // Lấy giá trị hiện tại của TextBox
            string currentText = txbMoney.Text;

            // Xóa các khoảng trắng và dấu phân cách hàng nghìn (nếu có)
            currentText = currentText.Replace(" ", "").Replace(",", "");

            // Kiểm tra xem giá trị hiện tại có phải là một số hợp lệ không
            if (long.TryParse(currentText, out long number))
            {
                // Nếu là một số hợp lệ, định dạng lại số và cập nhật giá trị của TextBox
                string formattedNumber = number.ToString("#,##0").Replace(",", " ");
                txbMoney.Text = formattedNumber;

                // Di chuyển con trỏ về cuối TextBox
                txbMoney.SelectionStart = txbMoney.Text.Length;
            }
        }

        private void btnSendOK_Click(object sender, EventArgs e)
        {
            // Hủ chi tiêu
            string enteredAmount = txbMoney.Text;

            // Generate QR Code
            QRCodeGenerator qrGenerator = new QRCodeGenerator();
            QRCodeData qrCodeData = qrGenerator.CreateQrCode(enteredAmount, QRCodeGenerator.ECCLevel.Q);
            QRCode qrCode = new QRCode(qrCodeData);
            Bitmap qrCodeImage = qrCode.GetGraphic(25);
            pbQRcode.Image = qrCodeImage;
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

        private void btnSendmoney_Click(object sender, EventArgs e)
        {
            //Kiểm Tra đã nhập đủ thông tin hay chưa
            string namebank = txbBankName.Text;
            string numacc = txbNumAccount.Text;
            string nameuser = txbNameUser.Text;
            string content = txbContent.Text;
            string password = txbPassword.Text;
            if (string.IsNullOrWhiteSpace(namebank) || string.IsNullOrWhiteSpace(numacc) || string.IsNullOrWhiteSpace(nameuser) || string.IsNullOrWhiteSpace(content))
            {
                MessageBox.Show("Vui Lòng Nhập Đầy Đủ Thông Tin!", "Cảnh Báo!");
                return;
            }

            // Kiểm Tra Đã Click vào checkBox ckbIagree hay chưa
            if (!rdbNumAcc1.Checked || rdbQR1.Checked)
            {
                MessageBox.Show("Vui Lòng Lựa Chọn Phương Thức Phù Hợp!", "Cảnh Báo Bảo Mật");
                return;
            }


            //Kiểm tra mật khẩu có đúng hay không
            if (!CustomerLoginDAO.Instance.Checkpassword(CreateMD5(password), Customer.LoginName))
            {
                MessageBox.Show("Mật Khẩu Không Chính Xác", "Cảnh Báo Bảo Mật");
                return;
            }
            SendMoney();

        }

        private void rdbNumAcc1_CheckedChanged(object sender, EventArgs e)
        {
            pbQR1.Enabled = false;
            pbQR1.Visible = false;
            txbNumAccount.Enabled = true;
            btnSendmoney.Enabled = true;
        }

        private void rdbQR1_CheckedChanged(object sender, EventArgs e)
        {
            txbNumAccount.Enabled = false;
            pbQR1.Enabled = true;
            pbQR1.Visible = true;
            btnSendmoney.Enabled = true;
        }

        private void rdbQR_CheckedChanged(object sender, EventArgs e)
        {
            txbNumAcc.Enabled = false;
            pbQRcode.Enabled = true;
            pbQRcode.Visible = true;
        }

        private void rdbNumAcc_CheckedChanged(object sender, EventArgs e)
        {
            txbNumAcc.Enabled = true;
            pbQRcode.Enabled = false;
            pbQRcode.Visible = false;
        }

        void SendMoney()
        {
            txbBankName.Text = "Mini Bank";
            string numacc = txbNumAccount.Text;
            string name = txbNameUser.Text;
            string money = txbMoney1.Text;
            string content = txbContent.Text;
            double amount = Convert.ToDouble(txbMoney1.Text.Replace(" ", ""));

            // Kiểm tra thông tin liên hệ có bỏ trống không
            if (string.IsNullOrWhiteSpace(numacc) || string.IsNullOrWhiteSpace(name) || string.IsNullOrWhiteSpace(money) || string.IsNullOrWhiteSpace(content))
            {
                MessageBox.Show("Vui Lòng Nhập Đầy Đủ Thông Tin Liên Hệ!", "Thông Báo Giao Dịch");
                return;
            }

            // Tiền chuyển không được quá số dư

            if (amount > customerAccount.Balance)
            {
                MessageBox.Show("Đã Vượt Quá Số Dư Hiện Tại", "Thông Báo Giao Dịch");
                return;
            }

            if (amount < 1000)
            {
                MessageBox.Show("Số Tiền Có Thể Chuyển Tối Thiểu Là 1 000 Đồng!", "Thông Báo Giao Dịch");
                return;
            }

            //Kiểm tra mật khẩu có đúng hay không
            if (!CustomerLoginDAO.Instance.Checkpassword(CreateMD5(txbPassword.Text), Customer.LoginName))
            {
                MessageBox.Show("Mật Khẩu Không Chính Xác", "Cảnh Báo Bảo Mật");
                return;
            }

            //Chuyển tiền
            Transaction transactionSend = new("Chuyển tiền", amount, content, customerAccount.AccountNumber);
            if (TransactionDAO.Instance.CreateTransaction(transactionSend))
            {
                transactionSend = TransactionDAO.Instance.GetNewestTransaction();
                if (CustomerAccountDAO.Instance.DecreaseUpdate(CustomerAccount.AccountNumber, amount) && CustomerAccountDAO.Instance.IncreaseUpdate(numacc, amount))
                {
                    customerAccount = CustomerAccountDAO.Instance.GetCustomerAccount(customer.Id);
                    if (MessageBox.Show(transactionSend.TransactionMessage(transactionSend, txbNumAccount.Text, txbNameUser.Text), "Hoàn Tất Chuyển Tiền!", MessageBoxButtons.OK) == DialogResult.OK)
                    {
                        Transaction transactionReceive = new("Nhận tiền", amount, content, numacc);
                        TransactionDAO.Instance.CreateTransaction(transactionReceive);
                        Close();
                        fHome fHome = new fHome(customer, customerAccount);
                        fHome.ShowDialog();
                    }
                    return;
                }
                else return;
            }
            else
            {
                MessageBox.Show("Chuyển Tiền Thất Bại!", "Thông Báo Giao Dịch");
                return;
            }
        }

        private void pbSearchInfor_Click(object sender, EventArgs e)
        {
            // Thuật toán tìm kiếm in tư khách hàng
            string accountNumber = txbNumAccount.Text;
            string id = CustomerAccountDAO.Instance.GetCustomerIDByAccountNumber(accountNumber);
            if (string.IsNullOrWhiteSpace(id))
            {
                MessageBox.Show("Số tài khoản không chính xác!");
                return;
            }

            // Không thể tự gửi tiền cho bản thân
            if (id.Equals(customer.Id))
            {
                MessageBox.Show(" Số tài khoản không được trùng với số tài khoản của chính bạn!");
                return;
            }
            // Hiển thị tên ở textbox
            txbNameUser.Text = CustomerDAO.Instance.GetCustomerNameByID(id);

        }

        // chỉ nhận số
        private void Onlynum_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        //Chỉ được nhập chữ
        private void Onlystring_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsLetter(e.KeyChar) && !char.IsControl(e.KeyChar) && !char.IsWhiteSpace(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void txbMoney1_TextChanged(object sender, EventArgs e)
        {
            // Lấy giá trị hiện tại của TextBox
            string currentText = txbMoney1.Text;

            // Xóa các khoảng trắng và dấu phân cách hàng nghìn (nếu có)
            currentText = currentText.Replace(" ", "").Replace(",", "");

            // Kiểm tra xem giá trị hiện tại có phải là một số hợp lệ không
            if (long.TryParse(currentText, out long number))
            {
                // Nếu là một số hợp lệ, định dạng lại số và cập nhật giá trị của TextBox
                string formattedNumber = number.ToString("#,##0").Replace(",", " ");
                txbMoney1.Text = formattedNumber;

                // Di chuyển con trỏ về cuối TextBox
                txbMoney1.SelectionStart = txbMoney1.Text.Length;
            }
        }
    }
}
