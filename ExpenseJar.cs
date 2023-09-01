using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mini_Banking.DTO
{
    public class ExpenseJar
    {
        private int jarID;
        private string jarName;
        private double jarAmount;
        private DateTime jarDateCreated;
        private string accountNumber;
        private string jarExpenseContent;

        public int JarID { get => jarID; set => jarID = value; }
        public string JarName { get => jarName; set => jarName = value; }
        public double JarAmount { get => jarAmount; set => jarAmount = value; }
        public string JarExpenseContent { get => jarExpenseContent; set => jarExpenseContent = value; }
        public string AccountNumber { get => accountNumber; set => accountNumber = value; }
        public DateTime JarDateCreated { get => jarDateCreated; set => jarDateCreated = value; }

        public ExpenseJar(string jarName, double jarAmount, string accountNumber, string jarExpenseContent) 
        { 
            JarName = jarName;
            JarAmount = jarAmount;
            JarExpenseContent = jarExpenseContent;
            AccountNumber = accountNumber;
        }

        public ExpenseJar(DataRow row) 
        { 
            JarID = (int)row["jarID"];
            JarName = row["jarName"].ToString();
            JarAmount = (double)row["jarAmount"];
            JarDateCreated = (DateTime)row["jarDateCreated"];
            JarExpenseContent = row["jarExpenseContent"].ToString();
            AccountNumber = row["accountNumber"].ToString();
        }
    }
}
