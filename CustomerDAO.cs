using Mini_Banking.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mini_Banking.DAO
{
    public class CustomerDAO // Khách hàng
    {
        private static CustomerDAO instance;

        public static CustomerDAO Instance
        {
            get { if (instance == null) { instance = new CustomerDAO(); } return instance; }
            private set { instance = value; }
        }
        private CustomerDAO() { }


        public bool CheckIDandPhoneNumber(string Id, string PhoneNumber)
        {
            string query = string.Format(" select * from customer where id = '{0}' and phoneNumber = '{1}' ", Id, PhoneNumber);

            DataTable data = DataProvider.Instance.ExecuteQuery(query);
            return data.Rows.Count > 0;
        }

        public bool CheckEmail(string email)
        {
            string query = string.Format(" select * from customer where email = '{0}'", email);

            DataTable data = DataProvider.Instance.ExecuteQuery(query);
            return data.Rows.Count > 0;
        }
        public bool AddNewCustomer(Customer customer)
        {
            string query = string.Format("INSERT INTO `customer`(`id`, `name`, `dateOfBirth`, `gender`, `email`, `phoneNumber`, `address`, `loginName`) VALUES ('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}')", 
                customer.Id, customer.Name, customer.DateOfBirth.ToString("yyyy-MM-dd"), customer.Gender, customer.Email, customer.PhoneNumber, customer.Address, customer.LoginName);
            int result = DataProvider.Instance.ExecuteNonQuery(query);
            return result > 0;
        }
        
        public Customer GetCustomer(string loginName)
        {
            string query = string.Format("select * from customer where loginName = '{0}'", loginName);
            DataTable data = DataProvider.Instance.ExecuteQuery(query);
            foreach (DataRow row in data.Rows)
            {
                Customer customer = new(row);
                return customer;
            }
            return null;
        }

         public bool UpdateInfor(Customer customer)
         {
             string query = string.Format(" UPDATE customer SET address = '{0}'  where id = '{1}' ",customer.Address, customer.Id);
             int result = DataProvider.Instance.ExecuteNonQuery(query);
             return result > 0;
         }


        public string GetCustomerNameByID(string id)
        {
            string query = string.Format("select name from customer where id = '{0}'", id);
            string name = DataProvider.Instance.ExecuteScalar(query).ToString();
            return name;
        }

    }
}
