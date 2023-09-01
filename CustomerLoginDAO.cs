using Mini_Banking.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mini_Banking.DAO
{
    public class CustomerLoginDAO //Tài Khoản Khách Hàng ĐĂNG NHẬP
    {
        private static CustomerLoginDAO instance;

        public static CustomerLoginDAO Instance
        {
            get { if (instance == null) { instance = new CustomerLoginDAO(); } return instance; }
            private set { instance = value; }
        }
        private CustomerLoginDAO() { }
        public bool CheckUserName(string userName)
        {
            string query = string.Format("select * from customerLogin where loginName = '{0}'",userName);
            DataTable data = DataProvider.Instance.ExecuteQuery(query);
            return data.Rows.Count > 0;
        }
        public bool SignUp(string password, string loginName)
        {
            string query = string.Format("insert into customerLogin(`loginName`, `password`) values ('{0}', '{1}')", loginName, password);
            int result = DataProvider.Instance.ExecuteNonQuery(query);
            return result > 0;
        }

        public bool Login ( string loginName, string password)
        {
            string query = string.Format("select * from customerLogin where loginName = '{0}' and password = '{1}'", loginName, password);
            DataTable data = DataProvider.Instance.ExecuteQuery(query);
            return data.Rows.Count > 0;
        }

        public bool Checkpassword(string password, string loginName)
        {
            string query = string.Format("select * from customerLogin where password = '{0}' and loginName = '{1}'", password,loginName);
            DataTable data = DataProvider.Instance.ExecuteQuery(query);
            return data.Rows.Count > 0;
        }

        public bool ChangePassword(string password, string loginName)
        {
            string query = string.Format(" UPDATE `customerlogin` SET `password`='{1}' WHERE `loginName`='{0}'", loginName, password );
            int result = DataProvider.Instance.ExecuteNonQuery(query);
            return result > 0;
        }
    }
}
