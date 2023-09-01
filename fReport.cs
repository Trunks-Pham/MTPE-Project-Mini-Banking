using Mini_Banking.DAO;
using Mini_Banking.DTO;
using MySql.Data.MySqlClient;
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
    public partial class fReport : Form
    {
        private Customer customer;
        private CustomerAccount customerAccount;

        public Customer Customer { get => customer; set => customer = value; }
        public CustomerAccount CustomerAccount { get => customerAccount; set => customerAccount = value; }

        public fReport(Customer customer, CustomerAccount customerAccount)
        {
            InitializeComponent();
            Customer = customer;
            CustomerAccount = customerAccount;

        }

        private void pbBack_Click(object sender, EventArgs e)
        {
            this.Hide();
            this.Close();
            fApp fApp = new fApp(customer, customerAccount);
            fApp.ShowDialog();
            this.Show();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            DateTime fromDate = dtpFrom.Value;
            DateTime toDate = dtpTo.Value;
            LoadTransationTableByTime(CustomerAccount.AccountNumber, fromDate, toDate);
        }

        void LoadTransationTableByTime(string accountNumber, DateTime fromDate, DateTime toDate)
        {
            dgvReport.DataSource = TransactionDAO.Instance.GetTransationTableByTime(accountNumber, fromDate, toDate);
        }

        void LoadDateTimePicker()
        {
            dtpFrom.Value = new(DateTime.Now.Year, DateTime.Now.Month, 1);
            dtpTo.Value = dtpFrom.Value.AddMonths(1).AddDays(-1);
            LoadTransationTableByTime(CustomerAccount.AccountNumber, dtpFrom.Value, dtpTo.Value);
        }

        private void fReport_Load(object sender, EventArgs e)
        {
            LoadDateTimePicker();
        }
    }
}
