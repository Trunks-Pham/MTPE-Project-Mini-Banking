using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mini_Banking.DTO
{
    public class Transaction
    {
        private int transactionID;
        private DateTime transactionDate;
        private string transactionType;
        private double transactionAmount;
        private string transactionContent;
        private string accountNumber;

        public int TransactionID { get => transactionID; set => transactionID = value; }
        public DateTime TransactionDate { get => transactionDate; set => transactionDate = value; }
        public string TransactionType { get => transactionType; set => transactionType = value; }
        public double TransactionAmount { get => transactionAmount; set => transactionAmount = value; }
        public string TransactionContent { get => transactionContent; set => transactionContent = value; }
        public string AccountNumber { get => accountNumber; set => accountNumber = value; }

        public Transaction(string transactionType, double transactionAmount, string transactionContent, string accountNumber) 
        { 
            TransactionType = transactionType;
            TransactionAmount = transactionAmount;
            TransactionContent = transactionContent;
            AccountNumber = accountNumber;
        }

        public Transaction(DataRow row) 
        {
            TransactionID = (int)row["transactionID"];
            TransactionDate = (DateTime)row["transactionDate"];
            TransactionType = row["transactionType"].ToString();
            TransactionAmount = (double)row["transactionAmount"];
            TransactionContent = row["transactionContent"].ToString();
            AccountNumber = row["accountNumber"].ToString();
        }

        public string TransactionMessage(Transaction transaction, string id, string name)
        {
            string message = string.Format("Quý khách đã chuyển tiền thành công!" +
                "\n\nMã giao dịch: {0}" +
                "\n\nSố tiền: {1}" +
                "\n\nSố tài khoản người thụ hưởng: {2}" +
                "\n\nTên người thụ hưởng: {3}" +
                "\n\nLoại giao dịch: {4}" +
                "\n\nNgày giao dịch: {5}" +
                "\n\nNội dung giao dịch: {6}"
                ,transaction.TransactionID, transaction.TransactionAmount.ToString("c", new CultureInfo("vi-VN")), id, name, transaction.TransactionType, transaction.TransactionDate.ToString(), transaction.transactionContent);
            return message;
        }
    }
}
