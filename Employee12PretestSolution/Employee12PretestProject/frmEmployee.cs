using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Employee12PretestProject
{
    public partial class frmEmployee : Form
    {
        public frmEmployee()
        {
            InitializeComponent();
        }

        //  Global Constants
        const decimal MINHOURS =  0.00M;            //  Min hours worked by emp
        const decimal MAXHOURS = 84.00M;            //  Max hours worked by emp
        const decimal MINHRATE =  0.00M;            //  Min employee hourly rate
        const decimal MAXHRATE = 99.99M;            //  Max employee hourly rate

        //  Global Variables
        static int     totalEmps  =       0;        //  Total # of employees
        static decimal totalGross =       0.00M;    //  Total gross all employees
        static decimal lowGross   = 1000000.00M;    //  Lowest  employee gross
        static decimal highGross  =      -1.00M;    //  Highest employee gross
        static decimal avgGross   =       0.00M;    //  Average employee gross

        private Employee employee = null;           //  An employee
        List<Employee> employees  = new List<Employee>(); // List of all employees

        //  Code that will execute when the Calculate button is clicked.
        private void btnCalculate_Click(object sender, EventArgs e)
        {
            CalculateIndividualEmployeeGrossPay();
        }

        private void CalculateIndividualEmployeeGrossPay()
        {
            decimal grossPay = 0.00M;

            //  If each of our employee "checks" validate
            if (IsValidData())
            {
                //  Instantiate a new Employee object
                employee = new Employee(txtFirstName.Text,
                                        txtLastName.Text,
                                        Convert.ToDecimal(txtHoursWorked.Text),
                                        Convert.ToDecimal(txtHourlyRate.Text));

                //  Calculate gross pay by calling the
                //  Employee class' CalculateGrossPay() method.
                grossPay = employee.CalculateGrossPay();

                //  Put gross pay value into the
                //  grossPay textbox
                txtGrossPay.Text = grossPay.ToString("c");

                //  Update all right-hand-side textboxes
                ConfigureStats(grossPay);

                //  Add the current employee to the
                //  employees List.
                employees.Add(employee);

                //  Print out stats for current employee
                PrintEmployeeStats();
            }
        }

        //  Validate first name, last name, hours worked, and
        //  the hourly rate for each employee using methods
        //  written/located in the Validator class (Validator.cs).
        private bool IsValidData()
        {
            bool success  = true;
            string errMsg = "";

            //  Validate the presence (i.e. a value) in 
            //  the txtFirstName textbox.
            errMsg += Validator.IsPresent(
                                txtFirstName.Text,
                                txtFirstName.Tag.ToString());

            //  Validate the presence (i.e. a value) in 
            //  the txtLastName textbox.
            errMsg += Validator.IsPresent(
                                txtLastName.Text,
                                txtLastName.Tag.ToString());

            //  Validate the value in the hours worked 
            //  textbox is numeric (decimal).
            errMsg += Validator.IsDecimal(
                                txtHoursWorked.Text,
                                txtHoursWorked.Tag.ToString());

            //  Validate the value in the hourly rate 
            //  textbox is numeric (decimal).
            errMsg += Validator.IsDecimal(
                                txtHourlyRate.Text,
                                txtHourlyRate.Tag.ToString());

            //  Validate the value in the hours worked 
            //  textbox is within range (>= 0 && <= 84).
            errMsg += Validator.IsWithinRange(
                                txtHoursWorked.Text,
                                txtHoursWorked.Tag.ToString(),
                                MINHOURS, MAXHOURS);

            //  Validate the value in the hours worked 
            //  textbox is within range (>= 0 && <= 84).
            errMsg += Validator.IsWithinRange(
                                txtHourlyRate.Text,
                                txtHourlyRate.Tag.ToString(),
                                MINHRATE, MAXHRATE);

            if (errMsg != "")
            {
                //  There were one or more validationerrors
                MessageBox.Show(errMsg, "INPUT ERROR(s)",
                                MessageBoxButtons.OK, 
                                MessageBoxIcon.Error);
                success = false;
            }

            return success;
        }

        private void ConfigureStats(decimal grossPay)
        {
            UpdateTotalEmployees();

            UpdateTotalGrossPay(grossPay);

            UpdateLowestGrossPay(grossPay);

            UpdateHighestGrossPay(grossPay);

            UpdateAverageGrossPay();
        }

        private void UpdateTotalEmployees()
        {
            //  Employee instantiated. So, increment
            //  totalEmps by 1.
            ++totalEmps;

            txtTotalEmployees.Text = totalEmps.ToString();
        }

        private void UpdateTotalGrossPay(decimal grossPay)
        {
            //  Update totalGross accumulator by
            //  adding the current gross pay to it.
            totalGross += grossPay;

            //  Update the total gross accumulator
            //  value in the totalGross textbox
            txtTotalGrossPay.Text = totalGross.ToString("c");
        }

        private void UpdateLowestGrossPay(decimal grossPay)
        {
            if (grossPay < lowGross)
            {
                lowGross = grossPay;
                txtLowGrossPay.Text = lowGross.ToString("c");
            }
        }

        private void UpdateHighestGrossPay(decimal grossPay)
        {
            if (grossPay > highGross)
            {
                highGross = grossPay;
                txtHighGrossPay.Text = highGross.ToString("c");
            }
        }

        private void UpdateAverageGrossPay()
        {
            avgGross = totalGross / totalEmps;
            txtAverageGrossPay.Text = avgGross.ToString("c");
        }

        private void PrintEmployeeStats()
        {
            //  Fill up the MessageBox the "old-fashioned" way
            string outputStr = "EMPLOYEE INFO\n\n";

            //outputStr += "First Name: " + employee.FirstName + "\n";
            //outputStr += "Last Name:  " + employee.LastName + "\n";
            //outputStr += "Hrs Worked: " + employee.HoursWorked.ToString("n2") + "\n";
            //outputStr += "Hrly  Rate: " + employee.HourlyRate.ToString("c") + "\n";
            //outputStr += "Gross Pay:  " + employee.CalculateGrossPay().ToString("c");

            outputStr += employee;

            MessageBox.Show(outputStr, "THIS EMPLOYEE'S STATS",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Information);

            ClearAll();
        }

        //  Code that will execute when the Show List button is clicked.
        private void btnShowList_Click(object sender, EventArgs e)
        {
            PrintOutAllValuesInList();
        }

        private void PrintOutAllValuesInList()
        {
            //  Fill up the List the "old-fashioned" way
            string outputStr = "LIST CONTENTS\n\n";

            //foreach(Employee emp in employees)
            //{
            //    outputStr += "First Name: " + emp.FirstName + "\n";
            //    outputStr += "Last Name:  " + emp.LastName + "\n";
            //    outputStr += "Hrs Worked: " + emp.HoursWorked.ToString("n2") + "\n";
            //    outputStr += "Hrly  Rate: " + emp.HourlyRate.ToString("c") + "\n";
            //    outputStr += "Gross Pay:  " + emp.CalculateGrossPay().ToString("c") + "\n\n";
            //}

            foreach (Employee emp in employees)
            {
                outputStr += emp + "\n\n";
            }

            MessageBox.Show(outputStr, "ALL EMPLOYEE'S STATS",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Information);

            ClearAll();
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            ClearAll();
        }

        private void ClearAll()
        {
            txtFirstName.Text   = "";
            txtLastName.Text    = "";
            txtHoursWorked.Text = "";
            txtHourlyRate.Text  = "";
            txtGrossPay.Text    = "";
            txtFirstName.Focus();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            ExitProgramOrNot();
        }

        private void ExitProgramOrNot()
        {
            DialogResult dialog = MessageBox.Show(
                "Do You Really Want To Exit?",
                "EXIT NOW?",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (dialog == DialogResult.Yes)
            {
                Application.Exit();
            }
        }
    }
}
