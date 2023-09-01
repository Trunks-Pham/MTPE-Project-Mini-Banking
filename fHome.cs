using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Mini_Banking.DAO;
using Mini_Banking.DTO;
using MySql.Data.MySqlClient;
namespace Mini_Banking
{
    public partial class fHome : Form
    {
        private Customer customer;
        private CustomerAccount customerAccount;

        public Customer Customer { get => customer; set => customer = value; }
        public CustomerAccount CustomerAccount { get => customerAccount; set => customerAccount = value; }

        public fHome(Customer customer, CustomerAccount customerAccount)
        {
            InitializeComponent();
            Customer = customer;
            CustomerAccount = customerAccount;
        }

        private void pbApp_Click(object sender, EventArgs e)
        {
            this.Hide();
            this.Close();
            fApp fA = new fApp(customer, customerAccount);
            fA.ShowDialog();
            this.Show();
        }

        private void pbHomeBig_Click(object sender, EventArgs e)
        {
            pbHomeBig.Visible = false;
            pbHomeMin.Visible = true;
            System.Windows.Forms.Timer timer = new System.Windows.Forms.Timer();
            timer.Interval = 300; // 0.3 seconds
            timer.Tick += (s, e) =>
            {
                pbHomeMin.Visible = false;
                pbHomeBig.Visible = true;
                timer.Stop();
            };
            timer.Start();
        }

        private void pbSetting_Click(object sender, EventArgs e)
        {
            this.Hide();
            this.Close();
            fSetting fSetting = new fSetting(customer, customerAccount);
            fSetting.ShowDialog();
            this.Show();
        }

        private void pbCopyNum_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(labelAccountNum.Text);
            MessageBox.Show(" Đã Sao Chép Số Tài Khoản vào Bộ Nhớ Đệm !");
        }

        private void pbViewMoney_Click(object sender, EventArgs e)
        {
            labelNumBalance.Visible = true;
            pbViewMoney.Visible = false;
            pbHideMoney.Visible = true;
        }

        private void pbHideMoney_Click(object sender, EventArgs e)
        {
            labelNumBalance.Visible = false;
            pbHideMoney.Visible = false;
            pbViewMoney.Visible = true;
        }

        private void pbSendmoney_Click(object sender, EventArgs e)
        {
            this.Hide();
            this.Close();
            fSendmoney fSendmoney = new fSendmoney(customerAccount, customer);
            fSendmoney.ShowDialog();
            this.Show();
        }

        private void pbSavemoney_Click(object sender, EventArgs e)
        {
            this.Hide();
            this.Close();
            fSavemoney fSavemoney = new fSavemoney(customer, customerAccount);
            fSavemoney.ShowDialog();
            this.Show();
        }

        private void pbWithmoney_Click(object sender, EventArgs e)
        {
            this.Hide();
            this.Close();
            fWithMoney fWithMoney = new fWithMoney(customer, customerAccount);
            fWithMoney.ShowDialog();
            this.Show();
        }

        private void pbJar_Click(object sender, EventArgs e)
        {
            this.Hide();
            this.Close();
            fJar fJar = new fJar(customerAccount, customer);
            fJar.ShowDialog();
            this.Show();
        }

        private void labelJar1_Click(object sender, EventArgs e)
        {
            this.Hide();
            this.Close();
            fJar fJar = new fJar(customerAccount, customer);
            fJar.ShowDialog();
            this.Show();
        }

        private void labelJar2_Click(object sender, EventArgs e)
        {
            this.Hide();
            this.Close();
            fJar fJar = new fJar(customerAccount, customer);
            fJar.ShowDialog();
            this.Show();
        }

        private void pbInterest_Click(object sender, EventArgs e)
        {
            this.Hide();
            this.Close();
            fInterest fInterest = new fInterest(customer, customerAccount);
            fInterest.ShowDialog();
            this.Show();
        }

        private void labelInterest1_Click(object sender, EventArgs e)
        {
            this.Hide();
            this.Close();
            fInterest fInterest = new fInterest(customer, customerAccount);
            fInterest.ShowDialog();
            this.Show();
        }

        private void labelInterest2_Click(object sender, EventArgs e)
        {
            this.Hide();
            this.Close();
            fInterest fInterest = new fInterest(customer, customerAccount);
            fInterest.ShowDialog();
            this.Show();
        }

        private void pbReportall_Click(object sender, EventArgs e)
        {
            this.Hide();
            this.Close();
            fReportall fReportall = new fReportall(customerAccount, customer);
            fReportall.ShowDialog();
            this.Show();
        }

        private void labelReportall1_Click(object sender, EventArgs e)
        {
            this.Hide();
            this.Close();
            fReportall fReportall = new fReportall(customerAccount, customer);
            fReportall.ShowDialog();
            this.Show();
        }

        private void labelReportall2_Click(object sender, EventArgs e)
        {
            this.Hide();
            this.Close();
            fReportall fReportall = new fReportall(customerAccount, customer);
            fReportall.ShowDialog();
            this.Show();
        }

        // Phương thức Load của form
        private void fHome_Load(object sender, EventArgs e)
        {
            LoadCustomerInfo();
        }

        void LoadCustomerInfo()
        {
            labelName.Text = Customer.Name;
            CustomerAccount = CustomerAccountDAO.Instance.GetCustomerAccount(Customer.Id);
            labelAccountNum.Text = CustomerAccount.AccountNumber;
            labelNumBalance.Text = CustomerAccount.Balance.ToString("#,##0").Replace(",", " ");
        }

        private void pbAvatar_Click(object sender, EventArgs e)
        {
            this.Hide();
            this.Close();
            fInforView fInforView = new fInforView(customer, customerAccount);
            fInforView.ShowDialog();
            this.Show();
        }

    }
}
