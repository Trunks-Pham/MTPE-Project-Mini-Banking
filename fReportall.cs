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
    public partial class fReportall : Form
    {
        private Customer customer;
        private CustomerAccount customerAccount;

        public Customer Customer { get => customer; set => customer = value; }
        public CustomerAccount CustomerAccount { get => customerAccount; set => customerAccount = value; }

        public fReportall(CustomerAccount customerAccount, Customer customer)
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

        void LoadTransationTable()
        {
            dgvReportall.DataSource = TransactionDAO.Instance.GetTransationTable(CustomerAccount.AccountNumber);
        }

        private void fReportall_Load(object sender, EventArgs e)
        {
            LoadTransationTable();
        }
    }


}
