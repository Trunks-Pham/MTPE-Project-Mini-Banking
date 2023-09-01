using Mini_Banking.DAO;
using Mini_Banking.DTO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Mini_Banking
{
    public partial class fJar : Form
    {
        private Customer customer;
        private CustomerAccount customerAccount;
        public fJar(CustomerAccount customerAccount, Customer customer)
        {
            InitializeComponent();
            this.customerAccount = customerAccount;
            Customer = customer;
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

        private void txbAmountJar_TextChanged(object sender, EventArgs e)
        {
            // Lấy giá trị hiện tại của TextBox
            string currentText = txbAmountJar.Text;

            // Xóa các khoảng trắng và dấu phân cách hàng nghìn (nếu có)
            currentText = currentText.Replace(" ", "").Replace(",", "");

            // Kiểm tra xem giá trị hiện tại có phải là một số hợp lệ không
            if (long.TryParse(currentText, out long number))
            {
                // Nếu là một số hợp lệ, định dạng lại số và cập nhật giá trị của TextBox
                string formattedNumber = number.ToString("#,##0").Replace(",", " ");
                txbAmountJar.Text = formattedNumber;

                // Di chuyển con trỏ về cuối TextBox
                txbAmountJar.SelectionStart = txbAmountJar.Text.Length;
            }
        }

        void LoadJarTable()
        {
            dgvJar.DataSource = ExpenseJarDAO.Instance.GetJarList(CustomerAccount.AccountNumber);
        }

        private void btnJarOK_Click(object sender, EventArgs e)
        {
            string name = txbNameJar.Text;
            double amount = Convert.ToDouble(txbAmountJar.Text.Replace(" ", ""));
            string content = txbContentJar.Text;

            //Check lỗi nhập tiền
            if (amount < 100000 || amount > customerAccount.Balance)
            {
                MessageBox.Show("Số tiền không hợp lệ!\nVui lòng kiểm tra lại thông tin!", "Thông báo");
                return;
            }

            //Tạo hủ
            ExpenseJar jar = new(name, amount, CustomerAccount.AccountNumber, content);
            if (ExpenseJarDAO.Instance.CreateJar(jar))
            {
                MessageBox.Show("Bạn đã tạo hủ thành công!", "Chúc mừng");

                //Tạo LSGD
                Transaction transaction = new("Chuyển tiền", amount, content, customerAccount.AccountNumber);
                TransactionDAO.Instance.CreateTransaction(transaction);

                //Cập nhật số dư
                CustomerAccountDAO.Instance.DecreaseUpdate(CustomerAccount.AccountNumber, amount);
                customerAccount = CustomerAccountDAO.Instance.GetCustomerAccount(customer.Id);

                //Load Thông tin hủ
                LoadJarTable();
            }
        }

        private void fJar_Load(object sender, EventArgs e)
        {
            LoadJarTable();
        }
    }
}
