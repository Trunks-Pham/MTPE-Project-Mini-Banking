using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mini_Banking.DTO
{
    public class JarTransaction
    {
        private int transactionID;
        private string jarTransactionName;
        private double jarTransactionAmount;
        private DateTime jarTransactionDate;
        private int jarID;

        public int TransactionID { get => transactionID; set => transactionID = value; }
        public string JarTransactionName { get => jarTransactionName; set => jarTransactionName = value; }
        public double JarTransactionAmount { get => jarTransactionAmount; set => jarTransactionAmount = value; }
        public DateTime JarTransactionDate { get => jarTransactionDate; set => jarTransactionDate = value; }
        public int JarID { get => jarID; set => jarID = value; }

        public JarTransaction(int transactionID, string jarTransactionName, double jarTransactionAmount, DateTime jarTransactionDate, int jarID) 
        {
            TransactionID = transactionID;
            JarTransactionName = jarTransactionName;
            JarTransactionAmount = jarTransactionAmount;
            JarTransactionDate = jarTransactionDate;
            JarID = jarID;
        }

        public JarTransaction(DataRow row) 
        {
            TransactionID = (int)row["transactionID"];
            JarTransactionName = row["jarTransactionName"].ToString();
            JarTransactionAmount = (double)row["jarTransactionAmount"];
            JarTransactionDate = (DateTime)row["jarTransactionDate"];
            JarID = (int)row["jarID"];
        }
    }
}
