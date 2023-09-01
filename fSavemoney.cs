using Mini_Banking.DAO;
using Mini_Banking.DTO;
using MySql.Data.MySqlClient;
using System.Data;
using System.Globalization;

namespace Mini_Banking
{
    public partial class fSavemoney : Form
    {
        private Customer customer;
        private CustomerAccount customerAccount;

        public Customer Customer { get => customer; set => customer = value; }
        public CustomerAccount CustomerAccount { get => customerAccount; set => customerAccount = value; }

        public fSavemoney(Customer customer, CustomerAccount customerAccount)
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

        private void txbSavemoneys_TextChanged(object sender, EventArgs e)
        {
            // Lấy giá trị hiện tại của TextBox
            string currentText = txbSavemoneys.Text;

            // Xóa các khoảng trắng và dấu phân cách hàng nghìn (nếu có)
            currentText = currentText.Replace(" ", "").Replace(",", "");

            // Kiểm tra xem giá trị hiện tại có phải là một số hợp lệ không
            if (long.TryParse(currentText, out long number))
            {
                // Nếu là một số hợp lệ, định dạng lại số và cập nhật giá trị của TextBox
                string formattedNumber = number.ToString("#,##0").Replace(",", " ");
                txbSavemoneys.Text = formattedNumber;

                // Di chuyển con trỏ về cuối TextBox
                txbSavemoneys.SelectionStart = txbSavemoneys.Text.Length;
            }
        }

        private void Inforsavemoneys_Click(object sender, EventArgs e)
        {
            MessageBox.Show("1. Được hưởng lãi suất thực theo ngày tính theo\n số dư còn lại cuối mỗi ngày. " +
                "\n\n2. Có thể rút tiền được bất cứ lúc nào trong giờ hành chính\n giúp bạn chủ động trong việc huy động nguồn tiền hơn." +
                "\n\n3. Có thể sử dụng số tiết kiệm này để đảm bảo vay vốn\n hay bảo lãnh cho người thứ ba vay vốn ngân hàng.", " Lý Do Bạn Nên Gửi Tiền Không Kỳ Hạn !");
        }

        public double DepositWithoutTerm(DateTime startDate)
        {
            // Lấy thông tin số tiền nhập từ textBox txbSavemoneys
            double depositAmount = double.Parse(txbSavemoneys.Text.Replace(" ", ""));
            // Định nghĩa lãi suất
            double interestRate = 0.015;
            // Tính số ngày thực gửi
            TimeSpan depositDuration = DateTime.Now - startDate;
            int actualDepositDays = (int)depositDuration.TotalDays;
            // Tính số tiền lãi
            double interestAmount = depositAmount * interestRate * actualDepositDays / 360;
            return interestAmount;
        }

        void LoadSavingBookList()
        {
            dgvListsavemoneys.DataSource = SavingsBookDAO.Instance.GetSavingBookListWithoutTerm(customerAccount.AccountNumber);
        }

        private void btnSavemoneys_Click(object sender, EventArgs e)
        {
            double amount = double.Parse(txbSavemoneys.Text.Replace(" ", ""));
            //Check lỗi nhập tiền
            if (amount < 1000000 || amount > customerAccount.Balance)
            {
                MessageBox.Show("Số tiền không hợp lệ!\nVui lòng kiểm tra lại thông tin!", "Thông báo");
                return;
            }

            //Tạo đối tượng savingBook
            SavingsBook savingsBook = new("Gửi tiền", 0, amount, 0.015, DepositWithoutTerm(DateTime.Now), customerAccount.AccountNumber);

            if (SavingsBookDAO.Instance.InsertSavingBook(savingsBook))
            {
                MessageBox.Show("Chúc Mừng Bạn Gửi Tiền Thành Công!" +
                "\n\nSố tiền bạn gửi là:" + amount.ToString("c", new CultureInfo("vi-VN")), " Hoàn Tất Gửi Tiền!");
                CustomerAccountDAO.Instance.DecreaseUpdate(customerAccount.AccountNumber, amount);

                //Tạo lịch sử giao dịch
                Transaction transaction = new("Gửi tiền", amount, "Gửi tiền không kỳ hạn", CustomerAccount.AccountNumber);
                TransactionDAO.Instance.CreateTransaction(transaction);
                LoadSavingBookList();
            }
            else
            {
                MessageBox.Show("Gửi tiền thất bại! Vui lòng thao tác lại!", "Thông báo");
                return;
            }
        }

        private void fSavemoney_Load(object sender, EventArgs e)
        {
            LoadSavingBookList();
        }
    }
}
