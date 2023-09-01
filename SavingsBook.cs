using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mini_Banking.DTO
{
    public class SavingsBook
    {
        private int savingsBookID;
        private string savingsName;
        private int savingsTerm;
        private double savingsAmount;
        private double interestRate;
        private double interestAmountPerTerm;
        private string accountNumber;
        private DateTime savingsDate;

        public int SavingsBookID { get => savingsBookID; set => savingsBookID = value; }
        public string SavingsName { get => savingsName; set => savingsName = value; }
        public int SavingsTerm { get => savingsTerm; set => savingsTerm = value; }
        public double SavingsAmount { get => savingsAmount; set => savingsAmount = value; }
        public double InterestRate { get => interestRate; set => interestRate = value; }
        public double InterestAmountPerTerm { get => interestAmountPerTerm; set => interestAmountPerTerm = value; }
        public string AccountNumber { get => accountNumber; set => accountNumber = value; }
        public DateTime SavingsDate { get => savingsDate; set => savingsDate = value; }

        public SavingsBook(string savingsName, int savingsTerm, double savingsAmount, double interestRate, double interestAmountPerTerm,string accountNumber)
        {
            SavingsName = savingsName;
            SavingsTerm = savingsTerm;
            SavingsAmount = savingsAmount;
            InterestRate = interestRate;
            InterestAmountPerTerm = interestAmountPerTerm;
            AccountNumber = accountNumber;
        }

        public SavingsBook(DataRow row) 
        {
            SavingsBookID = (int)row["savingsBookID"];
            SavingsName = row["savingsName"].ToString();
            SavingsTerm = (int)row["savingsTerm"];
            SavingsAmount = (double)row["savingsAmount"];
            InterestRate = (double)row["interestRate"];
            InterestAmountPerTerm = (double)row["interestAmountPerTerm"];
            AccountNumber = row["accountNumber"].ToString();
            SavingsDate = (DateTime)row["savingsDate"];
        }
    }
}
