using System;

namespace EmployeePayslip.Models
{
    public class EmployeeDetailsBo
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public double AnnualSalary { get; set; }
        public byte SuperRate { get; set; }
        public DateTime StartDate { get; set; }
    }
}