using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mini_Banking.DTO
{
    public class Customer
    {
        private string id;
        private string name;
        private DateTime dateOfBirth; 
        private string gender;
        private string email;
        private string phoneNumber;
        private string address;
        private string loginName;

        public string Name { get => name; set => name = value; }
        public string Gender { get => gender; set => gender = value; }
        public string Email { get => email; set => email = value; }
        public string PhoneNumber { get => phoneNumber; set => phoneNumber = value; }
        public string Address { get => address; set => address = value; }
        public string LoginName { get => loginName; set => loginName = value; }
        public string Id { get => id; set => id = value; }
        public DateTime DateOfBirth { get => dateOfBirth; set => dateOfBirth = value; }


        public Customer(string loginName ,string id, string name, DateTime dateOfBirth, string gender, string email, string phoneNumber, string address) 
        {
            Id = id;
            Name = name;
            DateOfBirth = dateOfBirth;
            Gender = gender;
            Email = email;
            PhoneNumber = phoneNumber;
            Address = address;
            LoginName = loginName;
        }

        public Customer(DataRow row) 
        { 
            Id = row["id"].ToString();    
            Name =row["name"].ToString();
            DateOfBirth = (DateTime)row["dateofBirth"];
            Gender = row["gender"].ToString();
            Email = row["email"].ToString();
            PhoneNumber = row["phoneNumber"].ToString();
            Address = row["address"].ToString();
            LoginName = row["loginName"].ToString();
        
        }
    }
}