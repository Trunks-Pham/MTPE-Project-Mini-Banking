using Mini_Banking.DTO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Mini_Banking
{
    public partial class fInforView : Form
    {
        private Customer customer;
        private CustomerAccount customerAccount;

        public Customer Customer { get => customer; set => customer = value; }
        public CustomerAccount CustomerAccount { get => customerAccount; set => customerAccount = value; }

        public fInforView(Customer customer, CustomerAccount customerAccount)
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

        private void fInforView_Load(object sender, EventArgs e)
        {
            LoadCustomerInfoView();
        }

        void LoadCustomerInfoView()
        {
            labelName.Text = customer.Name;
            labelAccNum.Text = customerAccount.AccountNumber.ToString();
            labelUsername.Text = Customer.LoginName;
            labelId.Text = customer.Id;
            labelEmail.Text = customer.Email;
            labelNumphone.Text = customer.PhoneNumber;
            labelDateOfBirth.Text = customer.DateOfBirth.ToShortDateString();
            labelGender.Text = customer.Gender;
            labelAddress.Text = customer.Address;

        }


        private void pbCopyNum_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(labelAccNum.Text);
            MessageBox.Show(" Đã Sao Chép Số Tài Khoản Vào Bộ Nhớ Đệm !", "Thông Báo!");
        }
    }
}
