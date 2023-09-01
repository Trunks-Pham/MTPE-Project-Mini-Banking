using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using Mini_Banking.DAO;
using Mini_Banking.DTO;
using MySql.Data.MySqlClient;

namespace Mini_Banking
{
    public partial class fContact : Form
    {
        private Customer customer;
        private CustomerAccount customerAccount;

        public Customer Customer { get => customer; set => customer = value; }
        public CustomerAccount CustomerAccount { get => customerAccount; set => customerAccount = value; }

        public fContact(Customer customer, CustomerAccount customerAccount)
        {
            InitializeComponent();
            LimitInput();

            Customer = customer;
            CustomerAccount = customerAccount;
        }

        // Giới hạnSĐT là 10 số và chỉ được nhập số
        private void LimitInput()
        {
            txbNumC.MaxLength = 10;
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

        private void txbNumC_TextChanged(object sender, EventArgs e)
        {
            txbNumC.RightToLeft = RightToLeft.Yes;
        }



        private void btnContact_Click(object sender, EventArgs e)
        {
            string customerID = customer.Id;
            string name = txbNameC.Text;
            string email = txbEmailC.Text;
            string phone = txbNumC.Text;
            string content = txbContentC.Text;

            // Kiểm tra thông tin liên hệ có bỏ trống không
            if (string.IsNullOrWhiteSpace(name) || string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(phone) || string.IsNullOrWhiteSpace(content))
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin liên hệ!");
                return;
            }
            else
            {
                Contact contact = new Contact(txbNameC.Text, txbEmailC.Text, txbNumC.Text, txbContentC.Text, customerID);

                if (ContactDAO.Instance.AddNewContact(contact))
                {
                    MessageBox.Show("Yêu cầu của bạn đã được gửi thành công !\nChúng tôi sẽ sớm liên hệ bạn !");
                }
                else
                {
                    MessageBox.Show("Có lỗi xảy ra khi gửi yêu cầu của bạn. Vui lòng thử lại sau!");
                }
            }


        }

        // Phương thức Load của form
        private void fContact_Load(object sender, EventArgs e)
        {
            LoadCustomerInfoContact();
        }

        void LoadCustomerInfoContact()
        {
            txbNameC.Text = Customer.Name;
            txbEmailC.Text = Customer.Email;
            txbNumC.Text = Customer.PhoneNumber.ToString();
        }

        private void fContact_Leave(object sender, EventArgs e)
        {
            LoadCustomerInfoContact();
        }
    }
}
