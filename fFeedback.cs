using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Mini_Banking.DAO;
using Mini_Banking.DTO;
using MySql.Data.MySqlClient;

namespace Mini_Banking
{
    public partial class fFeedback : Form
    {
        private Customer customer;
        private CustomerAccount customerAccount;

        public Customer Customer { get => customer; set => customer = value; }
        public CustomerAccount CustomerAccount { get => customerAccount; set => customerAccount = value; }

        public fFeedback(CustomerAccount customerAccount, Customer customer)
        {
            InitializeComponent();
            Customer = customer;
            CustomerAccount = customerAccount;
        }


        // Chỉ được nhập số tại SDT
        private void Onlynum_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        //Chỉ được nhập chữ tại Tên
        private void Onlystring_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsLetter(e.KeyChar) && !char.IsControl(e.KeyChar) && !char.IsWhiteSpace(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void pbBack_Click(object sender, EventArgs e)
        {
            this.Hide();
            this.Close();
            fApp fApp = new fApp(customer, customerAccount);
            fApp.ShowDialog();
            this.Show();
        }

        private void btnSendF_Click(object sender, EventArgs e)
        {
            // Thông tin phản hồi
            string customerID = customer.Id;
            string name = txbNameF.Text;
            string email = Customer.Email;
            string subject = "Khách Hàng Đóng Góp Ý Kiến";
            string message = txbContentF.Text;

            // Kiểm tra thông tin phản hồi
            if (string.IsNullOrWhiteSpace(name) || string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(subject) || string.IsNullOrWhiteSpace(message))
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin phản hồi!");
                return;
            }

            else
            {
                Feedback feedback = new Feedback(txbNameF.Text, txbEmailF.Text, subject, txbContentF.Text, customerID);
                if (FeedbackDAO.Instance.AddNewFeedback(feedback))
                {
                    MessageBox.Show(" Cảm ơn bạn đã đóng góp ý kiến !\n Chúng tôi sẽ cải thiện hệ thống của mình,\n để bạn có một trải nghiệm tuyệt vời !", "Thông báo");
                }
            }

        }

        private void fFeedback_Load(object sender, EventArgs e)
        {
            LoadCustomerInfoFeedback();
        }

        void LoadCustomerInfoFeedback()
        {
            txbNameF.Text = Customer.Name;
            txbEmailF.Text = Customer.Email;
        }

        private void txbEmailF_Leave(object sender, EventArgs e)
        {
            LoadCustomerInfoFeedback();
        }
    }
}
