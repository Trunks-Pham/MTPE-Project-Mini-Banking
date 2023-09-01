using Mini_Banking.DTO;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mini_Banking.DAO
{
    public class CustomerAccountDAO //Số Tài Khoản Khách Hàng
    {
        private static CustomerAccountDAO instance;

        public static CustomerAccountDAO Instance
        {
            get { if (instance == null) { instance = new CustomerAccountDAO(); } return instance; }
            private set { instance = value; }
        }
        private CustomerAccountDAO() { }

        public bool AddNewAccountNum( CustomerAccount customerAccount)
        {
            string query = string.Format("INSERT INTO `customerAccount`(`accountNumber`, `id`) VALUES ('{0}','{1}')",
                                            customerAccount.AccountNumber,customerAccount.Id);
            int result = DataProvider.Instance.ExecuteNonQuery(query);
            return result > 0;
        }

        public CustomerAccount GetCustomerAccount(string id)
        {
            string query = string.Format(" select * from customerAccount where id = '{0}'", id);
            DataTable data = DataProvider.Instance.ExecuteQuery(query);
            foreach (DataRow row in data.Rows)
            {
                CustomerAccount customerAccount = new(row);
                return customerAccount;
            } return null;
        }
        public string GetCustomerIDByAccountNumber(string accountNumber)
        {
            string query = string.Format("select id from customerAccount where accountNumber = '{0}'", accountNumber);
            object id = DataProvider.Instance.ExecuteScalar(query);
            if (id == null || string.IsNullOrEmpty(id.ToString()))
            {
                return string.Empty;
            }
            return id.ToString();
        }

        public bool IncreaseUpdate(string accountNumber, double amount)
        {
            string query = string.Format("UPDATE customerAccount SET balance = balance + {0} WHERE accountNumber = '{1}'", amount, accountNumber);
            int result = DataProvider.Instance.ExecuteNonQuery(query);
            return result > 0;
        }

        public bool DecreaseUpdate(string accountNumber, double amount)
        {
            string query = string.Format("UPDATE customerAccount SET balance = balance - {0} WHERE accountNumber = '{1}'", amount, accountNumber);
            int result = DataProvider.Instance.ExecuteNonQuery(query);
            return result > 0;
        }
    }
}
