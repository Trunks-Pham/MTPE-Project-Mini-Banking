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
    public partial class fSetting : Form
    {
        private Customer customer;
        private CustomerAccount customerAccount;

        public Customer Customer { get => customer; set => customer = value; }
        public CustomerAccount CustomerAccount { get => customerAccount; set => customerAccount = value; }

        public fSetting(Customer customer, CustomerAccount customerAccount)
        {
            InitializeComponent();
            Customer = customer;
            CustomerAccount = customerAccount;
        }

        private void pbSettingBig_Click(object sender, EventArgs e)
        {
            pbSettingMin.Visible = true;
            pbSettingBig.Visible = false;
            System.Windows.Forms.Timer timer = new System.Windows.Forms.Timer();
            timer.Interval = 300; // 0.3 seconds
            timer.Tick += (s, e) =>
            {
                pbSettingMin.Visible = false;
                pbSettingBig.Visible = true;
                timer.Stop();
            };
            timer.Start();
        }

        private void pbHome_Click(object sender, EventArgs e)
        {
            this.Hide();
            this.Close();
            fHome fH = new fHome(customer, customerAccount);
            fH.ShowDialog();
            this.Show();
        }

        private void pbApp_Click(object sender, EventArgs e)
        {
            this.Hide();
            this.Close();
            fApp fA = new fApp(customer, customerAccount);
            fA.ShowDialog();
            this.Show();
        }

        private void pbInfornew_Click(object sender, EventArgs e)
        {
            this.Hide();
            this.Close();
            fInfornew fInfornew = new fInfornew(customer, customerAccount);
            fInfornew.ShowDialog();
            this.Show();
        }

        private void labelInfornew_Click(object sender, EventArgs e)
        {
            this.Hide();
            this.Close();
            fInfornew fInfornew = new fInfornew(customer, customerAccount);
            fInfornew.ShowDialog();
            this.Show();
        }

        private void pbPasswordnew_Click(object sender, EventArgs e)
        {
            this.Hide();
            this.Close();
            fPassnew fPassnew = new fPassnew(customerAccount, customer);
            fPassnew.ShowDialog();
            this.Show();
        }

        private void labelPasswordnew_Click(object sender, EventArgs e)
        {
            this.Hide();
            this.Close();
            fPassnew fPassnew = new fPassnew(customerAccount, customer);
            fPassnew.ShowDialog();
            this.Show();
        }

        private void pbSignout_Click(object sender, EventArgs e)
        {
            //Hide();
            this.Close();
          //  fSiginIn fSiginIn = new fSiginIn();
           // fSiginIn.ShowDialog();
           // this.Show();
        }

        private void labelSignout_Click(object sender, EventArgs e)
        {
            this.Close();
          //  fSiginIn fSiginIn = new fSiginIn();
          //  fSiginIn.ShowDialog();
           // this.Show();
        }

        private void pbExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void labelExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
