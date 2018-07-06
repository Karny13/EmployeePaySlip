using System;
using EmployeePayslip.Interfaces;
using EmployeePayslip.Models;

namespace EmployeePayslip.BAL
{
    public class PayslipGenerationBal
    {
        private const int Months = 12;
        private const string DateFormat = "dd MMM";
        private readonly IPayslipInterface _iPay;

        public PayslipGenerationBal()
        {
            _iPay = new PayslipCalculationBal();
        }

        public PayslipBo CalculatedPayslip(EmployeeDetailsBo model)
        {
            var paySlip = new PayslipBo
            {
                Name = $"{model.FirstName} {model.LastName}",

                GrossIncome = CalculateGrossIncome(model.AnnualSalary),

                IncomeTax = _iPay.CalcTaxableIncome(model.AnnualSalary),

                NetIncome = CalculateNetIncome(CalculateGrossIncome(model.AnnualSalary),
                    _iPay.CalcTaxableIncome(model.AnnualSalary)),

                SuperAmount = CalculateSuperAmount(model.SuperRate, CalculateGrossIncome(model.AnnualSalary)),

                PayPeriod =
                    $"{model.StartDate.ToString(DateFormat)} - {model.StartDate.AddDays(30).ToString(DateFormat)}"
            };

            return paySlip;
        }

        public bool ValidateEmployeeModel(EmployeeDetailsBo employeeDetailsBo)
        {
            if (employeeDetailsBo.AnnualSalary <= 0 || employeeDetailsBo.SuperRate <= 0)
                return false;

            return true;
        }

        private double CalculateGrossIncome(double annualSalary)
        {
            return Math.Round(annualSalary / Months);
        }

        private double CalculateSuperAmount(byte superRate, double grossIncome)
        {
            return Math.Round(superRate * grossIncome / 100);
        }

        private double CalculateNetIncome(double grossIncome, double incomeTax)
        {
            return Math.Round(grossIncome - incomeTax);
        }
    }
}