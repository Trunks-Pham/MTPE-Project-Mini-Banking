using Mini_Banking.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mini_Banking.DAO
{
    public class ContactDAO // Liên hệ hỗ trợ
    {
        private static ContactDAO instance;

        public static ContactDAO Instance
        {
            get { if (instance == null) { instance = new ContactDAO(); } return instance; }
            private set { instance = value; }
        }
        private ContactDAO() { }

        public bool AddNewContact( Contact contact )
        {
            string query = string.Format("INSERT INTO `contact`(`customerID`, `name`, `email`, `phone`, `content`) VALUES ('{0}','{1}','{2}','{3}','{4}')",
            contact.CustomerID, contact.Name, contact.Email, contact.Phone, contact.Content);
            int result = DataProvider.Instance.ExecuteNonQuery(query);
            return result > 0;
        }
    }
}
