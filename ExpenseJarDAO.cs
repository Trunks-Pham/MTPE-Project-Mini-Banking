using Mini_Banking.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mini_Banking.DAO
{
    public class ExpenseJarDAO //Hủ Chi Tiêu
    {
        private static ExpenseJarDAO instance;

        public static ExpenseJarDAO Instance
        {
            get { if (instance == null) { instance = new ExpenseJarDAO(); } return instance; }
            private set { instance = value; }
        }
        private ExpenseJarDAO() { }

        public bool CreateJar(ExpenseJar jar)
        {
            string query = string.Format("INSERT INTO expensejar(`jarName`, `jarAmount`, `accountNumber`, `jarExpenseContent`) VALUES ('{0}', '{1}','{2}','{3}')",jar.JarName,jar.JarAmount,jar.AccountNumber,jar.JarExpenseContent);
            int result = DataProvider.Instance.ExecuteNonQuery(query);
            return result > 0;
        }

        public DataTable GetJarList(string accountNumber)
        {
            string query = string.Format("SELECT date_format(jarDateCreated, '%d/%m/%Y') as `Thời Gian Tạo`, `jarName` as `Tên Hủ`, `jarAmount` as `Số Tiền`, `jarExpenseContent` as `Ý Nghĩa Chi Tiêu` FROM `expensejar` WHERE accountNumber = '{0}'", accountNumber);
            return DataProvider.Instance.ExecuteQuery(query);
        }
    }
}
