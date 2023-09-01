using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mini_Banking.DTO
{
    public class Feedback
    {
        private int id;
        private string name;
        private string email;
        private string subject;
        private string message;
        private DateTime createdAt;
        private string customerID;

        public int Id { get => id; set => id = value; }
        public string Name { get => name; set => name = value; }
        public string Email { get => email; set => email = value; }
        public string Subject { get => subject; set => subject = value; }
        public string Message { get => message; set => message = value; }
        public DateTime CreatedAt { get => createdAt; set => createdAt = value; }
        public string CustomerID { get => customerID; set => customerID = value; }

        public Feedback(string name, string email, string subject, string message, string customerID) 
        {
            Name = name;
            Email = email;
            Subject = subject;
            Message = message;  
            CustomerID = customerID;
        }

        public Feedback(DataRow row ) 
        { 
            Id = (int)row["id"];
            Name = row["name"].ToString();
            Email = row["email"].ToString();
            Subject = row["subject"].ToString();
            Message = row["message"].ToString();
            CreatedAt = (DateTime)row["createdAt"];
            CustomerID = row["customerID"].ToString();
        }
    }
}
