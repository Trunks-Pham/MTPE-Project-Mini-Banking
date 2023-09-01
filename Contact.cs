using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mini_Banking.DTO
{
    public class Contact
    {
        private int id;
        private string customerID;
        private string name;
        private string email;
        private string phone;
        private string content;
        private DateTime createdAt;

        public int Id { get => id; set => id = value; }
        public string Name { get => name; set => name = value; }
        public string Email { get => email; set => email = value; }
        public string Phone { get => phone; set => phone = value; }
        public string Content { get => content; set => content = value; }
        public DateTime CreatedAt { get => createdAt; set => createdAt = value; }
        public string CustomerID { get => customerID; set => customerID = value; }

        public Contact(string name, string email, string phone, string content, string customerID)
        {
            Name = name;
            Email = email;
            Phone = phone;
            Content = content;
            CustomerID = customerID;
        }

        public Contact(DataRow row)
        {
            Id = (int)row["id"];
            Name = row["name"].ToString();
            Email = row["email"].ToString();
            Phone = row["phone"].ToString();
            Content = row["content"].ToString();
            CreatedAt = (DateTime)row["createdAt"];
            CustomerID = row["customerID"].ToString();
        }
    }
}
