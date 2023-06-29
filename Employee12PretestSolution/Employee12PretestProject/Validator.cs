using System;
using System.Windows.Forms;

namespace Employee12PretestProject
{
    public static class Validator
    {
        private static string lineEnd = "\n";

        public static string LineEnd
        {
            get
            {
                return lineEnd;
            }
            set
            {
                lineEnd = value;
            }
        }

        public static string IsPresent(string value, string name)
        {
            string msg = "";
            if (value == "")
            {
                msg += name + " is a required field." + LineEnd;
            }

            return msg;
        }

        public static string IsDecimal(string value, string name)
        {
            string msg = "";
            if (!Decimal.TryParse(value, out _))
            {
                msg += name + " must be a valid decimal value." + LineEnd;
            }

            return msg;
        }

        // The IsInt32 was omitted from this program.
        //public static string IsInt32(string value, string name)
        //{
        //    string msg = "";
        //    if (!Int32.TryParse(value, out _))
        //    {
        //        msg += name + " must be a valid integer value." + LineEnd;
        //    }

        //    return msg;
        //}

        public static string IsWithinRange(string value, string name, decimal min,
            decimal max)
        {
            string msg = "";
            if (Decimal.TryParse(value, out decimal number))
            {
                if (number < min || number > max)
                {
                    msg += name + " must be between " + min + " and " + max + "." + LineEnd;
                }
            }

            return msg;
        }
    }
}
