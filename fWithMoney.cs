using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BarcodeStandard;
using QRCoder;
using MySql.Data.MySqlClient;
using Mini_Banking.DTO;
using System.Globalization;
using Mini_Banking.DAO;

namespace Mini_Banking
{
    public partial class fWithMoney : Form
    {
        private Customer customer;
        private CustomerAccount customerAccount;

        public Customer Customer { get => customer; set => customer = value; }
        public CustomerAccount CustomerAccount { get => customerAccount; set => customerAccount = value; }

        public fWithMoney(Customer customer, CustomerAccount customerAccount)
        {
            InitializeComponent();
            Customer = customer;
            CustomerAccount = customerAccount;
        }

        private void pbBack_Click(object sender, EventArgs e)
        {
            this.Hide();
            this.Close();
            fHome fHome = new fHome(customer, customerAccount);
            fHome.ShowDialog();
            this.Show();
        }

        private void fWithMoney_Load(object sender, EventArgs e)
        {
            // Gọi phương thức để truy xuất thông tin khách hàng khi form được tải
            cbxSavingsBook.DataSource = SavingsBookDAO.Instance.GetSavingsBookComboBox(CustomerAccount.AccountNumber);
            cbxSavingsBook.DisplayMember = "savingsName";
        }

        private void txbNumwmoney_TextChanged(object sender, EventArgs e)
        {
            // Lấy giá trị hiện tại của TextBox
            string currentText = txbNumwmoney.Text;

            // Xóa các khoảng trắng và dấu phân cách hàng nghìn (nếu có)
            currentText = currentText.Replace(" ", "").Replace(",", "");

            // Kiểm tra xem giá trị hiện tại có phải là một số hợp lệ không
            if (long.TryParse(currentText, out long number))
            {
                // Nếu là một số hợp lệ, định dạng lại số và cập nhật giá trị của TextBox
                string formattedNumber = number.ToString("#,##0").Replace(",", " ");
                txbNumwmoney.Text = formattedNumber;

                // Di chuyển con trỏ về cuối TextBox
                txbNumwmoney.SelectionStart = txbNumwmoney.Text.Length;
            }
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

        private void btnOk_Click(object sender, EventArgs e)
        {
            string bankName = "MINI BANK";
            string accountNumber = customerAccount.AccountNumber;
            string name = customer.Name;
            double enteredAmount = Convert.ToDouble(txbNumwmoney.Text.Replace(" ", ""));

            if (!CustomerLoginDAO.Instance.Checkpassword(CreateMD5(txbPassword1.Text), Customer.LoginName))
            {
                MessageBox.Show("Mật Khẩu Không Chính Xác", "Cảnh Báo Bảo Mật");
                return;
            }

            if (enteredAmount > customerAccount.Balance)
            {
                MessageBox.Show("Số Tiền Bạn Muốn Đã Vượt Qúa Số Dư", "Cảnh Báo");
                return;
            }

            string qrString = string.Format("NGÂN HÀNG: {0}" +
                "\nSỐ TÀI KHOẢN: {1}" +
                "\nTÊN KHÁCH HÀNG: {2}" +
                "\nSỐ TIỀN: {3}", bankName, accountNumber, name, enteredAmount.ToString("c", new CultureInfo("vi-VN")));

            // Generate QR Code
            QRCodeGenerator qrGenerator = new QRCodeGenerator();
            QRCodeData qrCodeData = qrGenerator.CreateQrCode(qrString, QRCodeGenerator.ECCLevel.Q);
            QRCode qrCode = new QRCode(qrCodeData);
            Bitmap qrCodeImage = qrCode.GetGraphic(25);
            pbQR.Image = qrCodeImage;
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

        private void btnWithMoneySav_Click(object sender, EventArgs e)
        {
            // This event is triggered when the btnOK Button is clicked
            string bankName = "MINI BANK";
            SavingsBook savingsBook = cbxSavingsBook.Items[cbxSavingsBook.SelectedIndex] as SavingsBook;
            string booksName = savingsBook.SavingsName;
            string accountNumber = customerAccount.AccountNumber;
            string name = customer.Name;
            double enteredAmount = Convert.ToDouble(txbMoney.Text.Replace(" ", ""));

            if (string.IsNullOrWhiteSpace(booksName) || string.IsNullOrWhiteSpace(accountNumber) || string.IsNullOrWhiteSpace(name))
            {
                MessageBox.Show("Vui Lòng Nhập Đầy Đủ Thông Tin!", "Cảnh Báo!");
                return;
            }

            if (!CustomerLoginDAO.Instance.Checkpassword(CreateMD5(txbPassword.Text), Customer.LoginName))
            {
                MessageBox.Show("Mật Khẩu Không Chính Xác", "Cảnh Báo Bảo Mật");
                return;
            }

            if (enteredAmount > savingsBook.SavingsAmount || enteredAmount < 100000)
            {
                MessageBox.Show("Số Tiền Bạn Muốn Rút Không Hợp Lệ \nTối thiểu 100.000 đ\nTối đa " + savingsBook.SavingsAmount.ToString("c", new CultureInfo("vi-VN")), "Cảnh Báo");
                return;
            }

            if (ckbFaceMoney.Checked)
            {
                string qrString = string.Format("NGÂN HÀNG: {0}" +
                    "\nTÊN SỐ: {1}" +
                    "\nSỐ TÀI KHOẢN: {2}" +
                    "\nTÊN KHÁCH HÀNG: {3}" +
                    "\nSỐ TIỀN: {4}", bankName, booksName, accountNumber, name, enteredAmount.ToString("c", new CultureInfo("vi-VN")));

                // Generate QR Code
                QRCodeGenerator qrGenerator = new QRCodeGenerator();
                QRCodeData qrCodeData = qrGenerator.CreateQrCode(qrString, QRCodeGenerator.ECCLevel.Q);
                QRCode qrCode = new QRCode(qrCodeData);
                Bitmap qrCodeImage = qrCode.GetGraphic(10);
                pbQRcode.Image = qrCodeImage;
            }
            else if (ckbBalance.Checked)
            {
                //Cộng tiền dô tài khoản chính ( số dư ) và trừ tiền trong sổ ( sổ tiết kiệm được chọn để rút tiền )

                Transaction transaction = new("Nhận tiền", enteredAmount, "Rút tiền từ sổ tiết kiệm: " + booksName, customerAccount.AccountNumber);
                if (TransactionDAO.Instance.CreateTransaction(transaction))
                {
                    transaction = TransactionDAO.Instance.GetNewestTransaction();
                    if (CustomerAccountDAO.Instance.IncreaseUpdate(CustomerAccount.AccountNumber, enteredAmount))
                    {
                        customerAccount = CustomerAccountDAO.Instance.GetCustomerAccount(customer.Id);
                        if (MessageBox.Show("Rút tiền thành công!", "Hoàn Tất Rút Tiền!", MessageBoxButtons.OK) == DialogResult.OK)
                        {
                            this.Hide();
                            this.Close();
                            fHome fHome = new fHome(customer, customerAccount);
                            fHome.ShowDialog();
                        }
                        return;
                    }
                    else return;
                }
            }
            else
            {

            }
        }

        private void pbView1_Click(object sender, EventArgs e)
        {
            txbPassword1.UseSystemPasswordChar = false;
            pbView1.Visible = false;
            pbHide1.Visible = true;
        }

        private void pbHide1_Click(object sender, EventArgs e)
        {
            txbPassword1.UseSystemPasswordChar = true;
            pbHide1.Visible = false;
            pbView1.Visible = true;
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

        // Chỉ được nhập số 
        private void Onlynum_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void ckbFaceMoney_CheckedChanged(object sender, EventArgs e)
        {
            pbQRcode.Enabled = true;
            pbQRcode.Visible = true;
        }

        private void ckbBalance_CheckedChanged(object sender, EventArgs e)
        {
            pbQRcode.Enabled = false;
            pbQRcode.Visible = false;
        }
    }
}