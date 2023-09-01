using Mini_Banking.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mini_Banking.DAO
{
    public class FeedbackDAO //Phản hồi
    {
        private static FeedbackDAO instance;

        public static FeedbackDAO Instance
        {
            get { if (instance == null) { instance = new FeedbackDAO(); } return instance; }
            private set { instance = value; }
        }
        private FeedbackDAO() { }

        public bool AddNewFeedback(Feedback feedback)
        {
            string query = string.Format("INSERT INTO `feedback`(`customerID`, `name`, `email`, `message`) VALUES ('{0}','{1}','{2}','{3}')",
            feedback.CustomerID, feedback.Name, feedback.Email, feedback.Message);
            int result = DataProvider.Instance.ExecuteNonQuery(query);
            return result > 0;
        }
    }
}
