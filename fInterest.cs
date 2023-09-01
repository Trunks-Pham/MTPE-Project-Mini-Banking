using Mini_Banking.DAO;
using Mini_Banking.DTO;
using MySql.Data.MySqlClient;
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

namespace Mini_Banking
{
    public partial class fInterest : Form
    {
        private Customer customer;
        private CustomerAccount customerAccount;

        public Customer Customer { get => customer; set => customer = value; }
        public CustomerAccount CustomerAccount { get => customerAccount; set => customerAccount = value; }

        public fInterest(Customer customer, CustomerAccount customerAccount)
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

        private void fInterest_Load(object sender, EventArgs e)
        {
            LoadSavingBookListWithTerm();
        }

        public double DepositWithTerm(double depositAmount, double interestRate, DateTime startDate)
        {
            // Tính số ngày thực gửi
            TimeSpan depositDuration = DateTime.Now - startDate;
            int actualDepositDays = (int)depositDuration.TotalDays;
            // Tính số tiền lãi
            double interestAmount = depositAmount * interestRate * actualDepositDays / 365;
            return interestAmount;
        }

        void LoadSavingBookListWithTerm()
        {
            dgvTKTK.DataSource = SavingsBookDAO.Instance.GetSavingBookListWithTerm(customerAccount.AccountNumber);
        }

        private void btnTKTK_Click(object sender, EventArgs e)
        {
            string name = txbNameTKTK.Text.Trim();
            double amount = double.Parse(txbMoneyTKTK.Text.Replace(" ", ""));
            int rateID = cmbMonth.SelectedIndex + 1;
            double rate = InterestRateByTermDAO.Instance.GetRateByID(rateID);
            int month = InterestRateByTermDAO.Instance.GetMonthByID(rateID);
            //Check lỗi nhập tiền
            if (amount < 100000 || amount > customerAccount.Balance)
            {
                MessageBox.Show("Số tiền không hợp lệ!\nVui lòng kiểm tra lại thông tin!", "Thông báo");
                return;
            }

            //Tạo đối tượng savingBook
            SavingsBook savingsBook = new(name, month, amount, rate, DepositWithTerm(amount, rate, DateTime.Now), customerAccount.AccountNumber);

            if (SavingsBookDAO.Instance.InsertSavingBook(savingsBook))
            {
                MessageBox.Show("Chúc Mừng Bạn Gửi Tiền Thành Công!" +
                "\n\nSố tiền bạn gửi là: " + amount.ToString("c", new CultureInfo("vi-VN")), " Hoàn Tất Gửi Tiền!");
                CustomerAccountDAO.Instance.DecreaseUpdate(customerAccount.AccountNumber, amount);
                LoadSavingBookListWithTerm();
            }
            else
            {
                MessageBox.Show("Gửi tiền thất bại! Vui lòng thao tác lại!", "Thông báo");
                return;
            }
        }

        private void txbMoneyTKTK_TextChanged(object sender, EventArgs e)
        {
            // Lấy giá trị hiện tại của TextBox
            string currentText = txbMoneyTKTK.Text;

            // Xóa các khoảng trắng và dấu phân cách hàng nghìn (nếu có)
            currentText = currentText.Replace(" ", "").Replace(",", "");

            // Kiểm tra xem giá trị hiện tại có phải là một số hợp lệ không
            if (long.TryParse(currentText, out long number))
            {
                // Nếu là một số hợp lệ, định dạng lại số và cập nhật giá trị của TextBox
                string formattedNumber = number.ToString("#,##0").Replace(",", " ");
                txbMoneyTKTK.Text = formattedNumber;

                // Di chuyển con trỏ về cuối TextBox
                txbMoneyTKTK.SelectionStart = txbMoneyTKTK.Text.Length;
            }
        }
    }
}
