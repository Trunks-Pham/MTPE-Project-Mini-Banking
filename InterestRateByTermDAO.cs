using Mini_Banking.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mini_Banking.DAO
{
    public class InterestRateByTermDAO // Liên hệ hỗ trợ
    {
        private static InterestRateByTermDAO instance;

        public static InterestRateByTermDAO Instance
        {
            get { if (instance == null) { instance = new InterestRateByTermDAO(); } return instance; }
            private set { instance = value; }
        }
        private InterestRateByTermDAO() { }

        public double GetRateByID(int id)
        {
            string query = string.Format("select interestRate from interestRateByTerm where id = {0}", id);
            return Convert.ToDouble(DataProvider.Instance.ExecuteScalar(query));
        }
        public int GetMonthByID(int id)
        {
            string query = string.Format("select month from interestRateByTerm where id = {0}", id);
            return Convert.ToInt32(DataProvider.Instance.ExecuteScalar(query));
        }
    }
}
