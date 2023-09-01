using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mini_Banking.DTO;

namespace Mini_Banking.DAO
{
    public class TransactionDAO //Giao Dịch
    {
        private static TransactionDAO instance;

        public static TransactionDAO Instance
        {
            get { if (instance == null) { instance = new TransactionDAO(); } return instance; }
            private set { instance = value; }
        }
        private TransactionDAO() { }

        public Transaction GetNewestTransaction()
        {
            string query = string.Format("select * from transaction order by transactionID desc ");
            DataTable data = DataProvider.Instance.ExecuteQuery(query);
            foreach (DataRow row in data.Rows) 
            {
                Transaction transaction = new(row);
                return transaction;
            }
            return null;
        }

        public bool CreateTransaction(Transaction transaction)
        {
            string query = string.Format("INSERT INTO transaction(`accountNumber`, `transactionType`, `transactionAmount`, `transactionContent`) VALUES ('{0}', '{1}', '{2}', '{3}')",transaction.AccountNumber, transaction.TransactionType, transaction.TransactionAmount, transaction.TransactionContent);
            int result = DataProvider.Instance.ExecuteNonQuery(query);
            return result > 0;
        }

        public DataTable GetTransationTable(string accountNumber)
        {
            string query = string.Format("select date_format(transactionDate, '%H:%i:%s  %d/%m/%Y') as `Thời gian GD`, CONCAT(IF(transactionType = 'Nhận tiền' or 'Rút từ sổ', '+', '-'), ' ', FORMAT(transactionAmount, 0), ' VND') as `Số tiền GD`, transactionContent as `Nội dung GD` from transaction where accountNumber = '{0}' order by transactionID desc  ", accountNumber);
            return DataProvider.Instance.ExecuteQuery(query);
        }

        public DataTable GetTransationTableByTime(string accountNumber, DateTime fromDate, DateTime toDate)
        {
            string query = string.Format("select date_format(transactionDate, '%H:%i:%s  %d/%m/%Y') as `Thời gian GD`, CONCAT(IF(transactionType = 'Nhận tiền' or 'Rút từ sổ', '+', '-'), ' ', FORMAT(transactionAmount, 0), ' VND') as `Số tiền GD`, transactionContent as `Nội dung GD` from transaction where accountNumber = '{0}' and date_format(transactionDate, '%d-%m-%Y') BETWEEN '{1}' and '{2}' order by transactionID desc", accountNumber, fromDate.ToString("dd-MM-yyyy"), toDate.ToString("dd-MM-yyyy"));
            return DataProvider.Instance.ExecuteQuery(query);
        }
    }

}
