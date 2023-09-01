using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mini_Banking.DTO
{
    public class CustomerAccount
    {
        private string accountNumber;
        private double balance;
        private string dateCreated;
        private string status;
        private string id;

        
        public string Status { get => status; set => status = value; }
        public string AccountNumber { get => accountNumber; set => accountNumber = value; }
        public string Id { get => id; set => id = value; }
        public string DateCreated { get => dateCreated; set => dateCreated = value; }
        public double Balance { get => balance; set => balance = value; }

        public CustomerAccount(string accountNumber, string id) 
        { 
            AccountNumber = accountNumber;
            Id = id;
        }

        public CustomerAccount(DataRow row)
        {
            AccountNumber = row["accountNumber"].ToString();
            Balance = (double)row["balance"];
            DateCreated = row["dateCreated"].ToString();
            Status = row["status"].ToString();
            Id = row["id"].ToString(); 
        }
    }
}
