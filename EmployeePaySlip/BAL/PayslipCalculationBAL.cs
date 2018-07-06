using System;
using EmployeePayslip.Interfaces;

namespace EmployeePayslip.BAL
{
    public class PayslipCalculationBal : IPayslipInterface
    {
        public double CalcTaxableIncome(double annualIncome)
        {
            var taxableIncome = annualIncome > 18201 && annualIncome <= 37000 ? Tax(annualIncome - 18200, 19, 0) :
                annualIncome > 37001 && annualIncome <= 87000 ? Tax(annualIncome - 37000, 32.5, 3572) :
                annualIncome > 87001 && annualIncome <= 180000 ? Tax(annualIncome - 87000, 37, 19822) :
                Tax(annualIncome - 180000, 45, 54232);

            return Math.Round(taxableIncome);
        }

        private double Tax(double texableAmount, double rate, double additionalAmount)
        {
            return (texableAmount * rate / 100 + additionalAmount) / 12;
        }
    }
}