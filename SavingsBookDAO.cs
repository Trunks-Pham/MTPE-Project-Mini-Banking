using Mini_Banking.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mini_Banking.DAO
{
    public class SavingsBookDAO //SỔ Tiết kiệm
    {
        private static SavingsBookDAO instance;

        public static SavingsBookDAO Instance
        {
            get { if (instance == null) { instance = new SavingsBookDAO(); } return instance; }
            private set { instance = value; }
        }
        private SavingsBookDAO() { }

        public bool InsertSavingBook(SavingsBook savingsBook)
        {
            string query = string.Format("insert into savingsBook(`accountNumber`, `savingsName`, `savingsTerm`, `savingsAmount`, `interestRate`, `interestAmountPerTerm`) values('{0}', '{1}', {2}, {3}, {4}, {5})"
                ,savingsBook.AccountNumber, savingsBook.SavingsName, savingsBook.SavingsTerm, savingsBook.SavingsAmount, savingsBook.InterestRate, savingsBook.InterestAmountPerTerm);
            int result = DataProvider.Instance.ExecuteNonQuery(query);
            return result > 0;
        }

        public DataTable GetSavingBookListWithoutTerm(string accountNumber)
        {
            string query = string.Format("select date_format(savingsDate, '%d/%m/%Y') as `Ngày gửi`, savingsName as `Tên sổ`, concat(FORMAT(savingsAmount, 0), ' VND')  as `Số tiền gửi`, concat(interestRate * 100, '%') as `Lãi suất`, concat(FORMAT(interestAmountPerTerm, 0), ' VND') as `Tiền lãi` from savingsBook where accountNumber = '{0}' and savingsTerm = 0;", accountNumber);
            return DataProvider.Instance.ExecuteQuery(query);
        }

        public DataTable GetSavingBookListWithTerm(string accountNumber)
        {
            string query = string.Format("select date_format(savingsDate, '%d/%m/%Y') as `Ngày gửi`, savingsName as `Tên sổ`, concat(FORMAT(savingsAmount, 0), ' VND')  as `Số tiền gửi`, concat(savingsTerm, ' tháng') as `Kỳ hạn`,concat(interestRate * 100, '%') as `Lãi suất`, concat(FORMAT(interestAmountPerTerm, 0), ' VND') as `Tiền lãi` from savingsBook where accountNumber = '{0}' and savingsTerm > 0;", accountNumber);
            return DataProvider.Instance.ExecuteQuery(query);
        }

        public DataTable GetBookByID(int id)
        {
            string query = string.Format("select * from savingsBook where savingsTerm > 0 and savingsBookID = {0}", id);
            return DataProvider.Instance.ExecuteQuery(query);
        }

        public List<SavingsBook> GetSavingsBookComboBox(string accountNumber)
        {
            List<SavingsBook> list = new();
            string query = string.Format("select * from savingsBook where accountNumber = '{0}' and savingsTerm > 0", accountNumber);
            DataTable data = DataProvider.Instance.ExecuteQuery(query);
            foreach (DataRow row in data.Rows)
            {
                SavingsBook book = new(row);
                list.Add(book);
            }
            return list;
        }
    }
}
