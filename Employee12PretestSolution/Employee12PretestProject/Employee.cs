using System;

namespace Employee12PretestProject
{
    public class Employee
    {
        //  Program constants
        const decimal MAXSTRAIGHT  = 40.00M;
        const decimal OVERTIMERATE =  1.50M;

        //  "Regular" instance variable
        private decimal _grossPay;

        //  Constructor
        public Employee(string  fn,  string ln,
                        decimal hw, decimal hr)
        {
            FirstName   = fn;
            LastName    = ln;
            HoursWorked = hw;
            HourlyRate  = hr;
        }

        //  Auto-Implemented Properties
        public string FirstName {  get; set; }
        public string LastName { get; set; }
        public decimal HoursWorked { get; set; }
        public decimal HourlyRate { get; set; }

        public decimal CalculateGrossPay()
        {
            if (HoursWorked <= MAXSTRAIGHT)
            {
                //  No overtime worked by this employee
                _grossPay = HoursWorked * HourlyRate;
            }
            else
            {
                //  person worked 50 hours at $20/hour
                //  1. 40 * 20  (straight or non-OT pay)   $800
                //  2. (50 - 40) * 20 * 1.5                $300
                //  3. $800 + $300 = $1100.00
                //  Some overtime was worked by this employee
                _grossPay = ((MAXSTRAIGHT * HourlyRate) +
                            ((HoursWorked - MAXSTRAIGHT) *
                             HourlyRate * OVERTIMERATE));
            }

            return _grossPay;
        }

        public override string ToString()
        {
            return "NAME: " + FirstName + " " + LastName + "\n" +
                  "HOURS: " + HoursWorked.ToString("n2") + "\n" +
                   "RATE: " + HourlyRate.ToString("c")   + "\n" +
                  "GROSS: " + _grossPay.ToString("c")    + "\n";
        }
    }
}
