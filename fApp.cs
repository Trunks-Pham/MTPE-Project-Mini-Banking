using Mini_Banking.DTO;
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

namespace Mini_Banking
{
    public partial class fApp : Form
    {
        private Customer customer;
        private CustomerAccount customerAccount;

        public Customer Customer { get => customer; set => customer = value; }
        public CustomerAccount CustomerAccount { get => customerAccount; set => customerAccount = value; }

        public fApp(Customer customer, CustomerAccount customerAccount)
        {
            InitializeComponent();
            Customer = customer;
            CustomerAccount = customerAccount;
        }

        private void pbReport_Click(object sender, EventArgs e)
        {
            this.Hide();
            this.Close();
            fReport fReport = new fReport(customer, customerAccount);
            fReport.ShowDialog();
            this.Show();
        }

        private void labelReport_Click(object sender, EventArgs e)
        {
            this.Hide();
            this.Close();
            fReport fReport = new fReport(customer, customerAccount);
            fReport.ShowDialog();
            this.Show();
        }

        private void pbFeedback_Click(object sender, EventArgs e)
        {
            this.Hide();
            this.Close();
            fFeedback fFeedback = new fFeedback(customerAccount, customer);
            fFeedback.ShowDialog();
            this.Show();
        }

        private void labelFeedback_Click(object sender, EventArgs e)
        {
            this.Hide();
            this.Close();
            fFeedback fFeedback = new fFeedback(customerAccount, customer);
            fFeedback.ShowDialog();
            this.Show();
        }

        private void pbContact_Click(object sender, EventArgs e)
        {
            this.Hide();
            this.Close();
            fContact fContact = new fContact(customer, customerAccount);
            fContact.ShowDialog();
            this.Show();
        }

        private void labelContact_Click(object sender, EventArgs e)
        {
            this.Hide();
            this.Close();
            fContact fContact = new fContact(customer, customerAccount);
            fContact.ShowDialog();
            this.Show();
        }

        private void pbAppBig_Click(object sender, EventArgs e)
        {
            pbAppBig.Visible = false;
            pbAppMin.Visible = true;
            System.Windows.Forms.Timer timer = new System.Windows.Forms.Timer();
            timer.Interval = 300; // 0.3 seconds
            timer.Tick += (s, e) =>
            {
                pbAppMin.Visible = false;
                pbAppBig.Visible = true;
                timer.Stop();
            };
            timer.Start();
        }

        private void pbHome_Click(object sender, EventArgs e)
        {
            this.Hide();
            this.Close();
            fHome fHome = new fHome(customer, customerAccount);
            fHome.ShowDialog();
            this.Show();
        }

        private void pbSetting_Click(object sender, EventArgs e)
        {
            this.Hide();
            this.Close();
            fSetting fS = new fSetting(customer, customerAccount);
            fS.ShowDialog();
            this.Show();
        }

        private void pbInfor_Click(object sender, EventArgs e)
        {
            MessageBox.Show("MINI BANK là ngân hàng trực thuộc tổng cty công nghệ" +
                        "\nMTPE - Technology nhà sáng lập Founder: Phạm Minh Thảo " +
                        "\n\nPhạm Minh Thảo là học viên tại VTC Academy Plus" +
                        "\nvới chuyên ngành Kỹ Thuật Phần Mềm khóa K22." +
                        "\n\nMini Banking là một giải pháp công nghệ mới về ngân " +
                        "\nhàng số đem lại nhiều giá trị cốt lỗi và thuận tiện cho" +
                        "\nngười sử dụng đặc biệt là người bận rộn quá dư dả về thời" +
                        "\ngian để ra quầy giao dịch trực tiếp." +
                        "\n\nMini Banking là một ngân hàng năng động và tiên phong " +
                        "\ntrong lĩnh vực ngân hàng số tại Việt Nam tuy không phải" +
                        "\nlà ngân hàng số duy nhất nhưng chắc sẽ là ngân hàng số" +
                        "\nđộc nhất!"
                        , "Thông Tin Về MINI BANKING");
        }

        private void labelInfor_Click(object sender, EventArgs e)
        {
            MessageBox.Show("MINI BANK là ngân hàng trực thuộc tổng cty công nghệ" +
                        "\nMTPE - Technology nhà sáng lập Founder: Phạm Minh Thảo " +
                        "\n\nPhạm Minh Thảo là học viên tại VTC Academy Plus" +
                        "\nvới chuyên ngành Kỹ Thuật Phần Mềm khóa K22." +
                        "\n\nMini Banking là một giải pháp công nghệ mới về ngân " +
                        "\nhàng số đem lại nhiều giá trị cốt lỗi và thuận tiện cho" +
                        "\nngười sử dụng đặc biệt là người bận rộn quá dư dả về thời" +
                        "\ngian để ra quầy giao dịch trực tiếp." +
                        "\n\nMini Banking là một ngân hàng năng động và tiên phong " +
                        "\ntrong lĩnh vực ngân hàng số tại Việt Nam tuy không phải" +
                        "\nlà ngân hàng số duy nhất nhưng chắc sẽ là ngân hàng số" +
                        "\nđộc nhất!"
                        , "Thông Tin Về MINI BANKING");
        }
    }
}
