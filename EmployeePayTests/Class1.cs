using System;
using EmployeePayslip.BAL;
using EmployeePayslip.Models;
using NUnit.Framework;

namespace EmployeePayTests
{
    public class EmployeeTests
    {
        private PayslipCalculationBal payslipCalculationBal;
        private PayslipGenerationBal payslipGenerationBal;

        [SetUp]
        public void Setup()
        {
            payslipGenerationBal = new PayslipGenerationBal();
            payslipCalculationBal = new PayslipCalculationBal();
        }

        [Test]
        public void CalculatedPayslip_Test_Slab1()
        {
            var model = EmployeeTestData(18000, 2);

            var result = payslipGenerationBal.CalculatedPayslip(model);

            Assert.IsNotNull(result);
            Assert.AreEqual("john Doe", result.Name);
            Assert.AreEqual(1500, result.GrossIncome);
            Assert.AreEqual(30, result.SuperAmount);
            Assert.AreNotEqual(0, result.GrossIncome);
            Assert.AreNotEqual("john Dooe", result.Name);
        }

        [Test]
        public void CalculatedPayslip_Test_Slab2()
        {
            var model = EmployeeTestData(25000, 5);

            var result = payslipGenerationBal.CalculatedPayslip(model);

            Assert.IsNotNull(result);
            Assert.AreEqual("john Doe", result.Name);
            Assert.AreEqual(2083, result.GrossIncome);
            Assert.AreEqual(104, result.SuperAmount);
            Assert.AreEqual(108, result.IncomeTax);
            Assert.AreNotEqual("john Dooe", result.Name);
        }

        [Test]
        public void CalculatedPayslip_Test_Slab3()
        {
            var model = EmployeeTestData(50000, 5);

            var result = payslipGenerationBal.CalculatedPayslip(model);

            Assert.IsNotNull(result);
            Assert.AreEqual("john Doe", result.Name);
            Assert.AreEqual(4167, result.GrossIncome);
            Assert.AreEqual(208, result.SuperAmount);
            Assert.AreNotEqual(0, result.GrossIncome);
            Assert.AreEqual(650, result.IncomeTax);
            Assert.AreNotEqual("john Dooe", result.Name);
        }

        [Test]
        public void CalculatedPayslip_Test_Slab4()
        {
            var model = EmployeeTestData(100000, 9);

            var result = payslipGenerationBal.CalculatedPayslip(model);

            Assert.IsNotNull(result);
            Assert.AreEqual("john Doe", result.Name);
            Assert.AreEqual(8333, result.GrossIncome);
            Assert.AreEqual(750, result.SuperAmount);
            Assert.AreNotEqual(0, result.GrossIncome);
            Assert.AreEqual(2053, result.IncomeTax);
            Assert.AreNotEqual("john Dooe", result.Name);
        }

        [Test]
        public void CalculatedPayslip_Test_Slab5()
        {
            var model = EmployeeTestData(500000, 10);

            var result = payslipGenerationBal.CalculatedPayslip(model);

            Assert.IsNotNull(result);
            Assert.AreEqual("john Doe", result.Name);
            Assert.AreEqual(41667, result.GrossIncome);
            Assert.AreEqual(4167, result.SuperAmount);
            Assert.AreNotEqual(0, result.GrossIncome);
            Assert.AreEqual(16519, result.IncomeTax);
            Assert.AreNotEqual("john Dooe", result.Name);
        }

        [Test]
        public void CalculatedPayslip_Test_Failure_AnnualIncome()
        {
            var model = EmployeeTestData(-55000, 10);

            var result = payslipGenerationBal.CalculatedPayslip(model);

            Assert.IsNotNull(result);
            Assert.IsFalse(result.GrossIncome > 0);
        }

        [Test]
        public void CalculatedPayslip_Test_Failure_Superrate()
        {
            var model = EmployeeTestData(55000, 0);

            var result = payslipGenerationBal.CalculatedPayslip(model);

            Assert.IsNotNull(result);
            Assert.IsFalse(result.SuperAmount > 0);
        }

        [Test]
        public void CalculatedPayslip_Test_Failure_Name_Failure()
        {
            var model = EmployeeTestData(50000, 5, "", "");

            var result = payslipGenerationBal.CalculatedPayslip(model);

            Assert.IsFalse(result.Name.Length == 0);
        }

        [Test]
        public void CalcTaxableIncome_Test()
        {
            var result = payslipCalculationBal.CalcTaxableIncome(60050);

            Assert.IsNotNull(result);
            Assert.AreEqual(922, result);
            Assert.AreNotEqual(1000, result);
        }

        public EmployeeDetailsBo EmployeeTestData(double annualIncome, byte superRate, string fName = "john",
            string lName = "Doe")
        {
            var employeeDetailsBo = new EmployeeDetailsBo
            {
                FirstName = fName,
                LastName = lName,
                AnnualSalary = annualIncome,
                StartDate = DateTime.Now,
                SuperRate = superRate
            };

            return employeeDetailsBo;
        }
    }
}
