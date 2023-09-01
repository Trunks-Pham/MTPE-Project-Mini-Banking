using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mini_Banking.DTO
{
    public class CustomerLogin
    {
        private string loginName;
        private string password;
        private string accountStatus;

        public string LoginName { get => loginName; set => loginName = value; }
        public string Password { get => password; set => password = value; }
        public string AccountStatus { get => accountStatus; set => accountStatus = value; }

        public CustomerLogin(string loginName, string password,string id)
        {
            LoginName = loginName;
            Password = password;
        }

        public CustomerLogin(DataRow row) 
        {
            LoginName = row["loginName"].ToString();
            Password = row["password"].ToString();
            AccountStatus = row["accountStatus"].ToString();
        }
    }
}
