using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mini_Banking.DTO
{
    public class InterestRateByTerm
    {
        private int id;
        private int month;
        private double interestRate;

        public int Id { get => id; set => id = value; }
        public int Month { get => month; set => month = value; }
        public double InterestRate { get => interestRate; set => interestRate = value; }

        public InterestRateByTerm() 
        {
            Id = id;
            Month = month;
            InterestRate = interestRate;
        }

        public InterestRateByTerm(DataRow row)
        {
            Id = (int)row["id"];
            Month = (int)row["month"];
            InterestRate = (double)row["interestRate"];
        }
    }
}
