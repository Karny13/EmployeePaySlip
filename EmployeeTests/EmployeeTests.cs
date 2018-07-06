using System;
using EmployeePayslip.BAL;
using EmployeePayslip.Models;
using NUnit.Framework;

namespace EmployeeTests
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
            //Arrange
            var model = EmployeeTestData(18000, 2);

            //Act
            var result = payslipGenerationBal.CalculatedPayslip(model);

            //Assert
            Assert.IsNotNull(result);
            Assert.AreEqual("john Doe", result.Name);
            Assert.AreNotEqual("john Dooe", result.Name);
        }

        [Test]
        public void CalculatedPayslip_Test_Slab1_ValidData_Passed_GetValidGrossIncome()
        {
            //Arrange
            var model = EmployeeTestData(18000, 2);

            //Act
            var result = payslipGenerationBal.CalculatedPayslip(model);

            //Assert
            Assert.AreEqual(1500, result.GrossIncome);
        }

        [Test]
        public void CalculatedPayslip_Test_Slab1_Data_Passed_GetInvalidGrossIncome()
        {
            //Arrange
            var model = EmployeeTestData(18000, 2);

            //Act
            var result = payslipGenerationBal.CalculatedPayslip(model);

            //Assert
            Assert.AreNotEqual(0, result.GrossIncome);
        }

        [Test]
        public void CalculatedPayslip_Test_Slab2()
        {
            //Arrange
            var model = EmployeeTestData(25000, 5);

            //Act
            var result = payslipGenerationBal.CalculatedPayslip(model);

            //Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(2083, result.GrossIncome);
            Assert.AreNotEqual("john Dooe", result.Name);
        }

        [Test]
        public void CalculatedPayslip_Test_Slab2_GetCorrectIncomeTax()
        {
            //Arrange
            var model = EmployeeTestData(25000, 5);

            //Act
            var result = payslipGenerationBal.CalculatedPayslip(model);

            //Assert
            Assert.AreEqual(108, result.IncomeTax);
        }

        [Test]
        public void CalculatedPayslip_Test_Slab2_When_Name_Is_Matched()
        {
            //Arrange
            var model = EmployeeTestData(25000, 5);

            //Act
            var result = payslipGenerationBal.CalculatedPayslip(model);

            //Assert
            Assert.AreEqual("john Doe", result.Name);
        }

        [Test]
        public void CalculatedPayslip_Test_Slab3()
        {
            //Arrange
            var model = EmployeeTestData(50000, 5);

            //Act
            var result = payslipGenerationBal.CalculatedPayslip(model);

            //Assert
            Assert.IsNotNull(result);
            Assert.AreNotEqual("john Dooe", result.Name);
        }

        [Test]
        public void CalculatedPayslip_Test_Slab4()
        {
            //Arrange
            var model = EmployeeTestData(100000, 9);

            //Act
            var result = payslipGenerationBal.CalculatedPayslip(model);

            //Assert
            Assert.IsNotNull(result);
            Assert.AreNotEqual("john Dooe", result.Name);
        }

        [Test]
        public void CalculatedPayslip_Test_Slab5()
        {
            //Arrange
            var model = EmployeeTestData(500000, 10);

            //Act
            var result = payslipGenerationBal.CalculatedPayslip(model);

            //Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(16519, result.IncomeTax);
        }

        [Test]
        public void CalculatedPayslip_Test_Slab5_SuperAmountIsMatched()
        {
            //Arrange
            var model = EmployeeTestData(500000, 10);

            //Act
            var result = payslipGenerationBal.CalculatedPayslip(model);

            //Assert
            Assert.AreEqual(4167, result.SuperAmount);
        }

        [Test]
        public void CalculatedPayslip_Test_Slab5_SuperAmountIsNotMatched()
        {
            //Arrange
            var model = EmployeeTestData(500000, 10);

            //Act
            var result = payslipGenerationBal.CalculatedPayslip(model);

            //Assert
            Assert.AreNotEqual(0, result.SuperAmount);
        }

        [Test]
        public void CalculatedPayslip_Test_Failure_AnnualIncome()
        {
            //Arrange
            var model = EmployeeTestData(-55000, 10);

            //Act
            var result = payslipGenerationBal.CalculatedPayslip(model);

            //Assert
            Assert.IsNotNull(result);
            Assert.IsFalse(result.GrossIncome > 0);
        }

        [Test]
        public void CalculatedPayslip_Test_Failure_Superrate()
        {
            //Arrange
            var model = EmployeeTestData(55000, 0);

            //Act
            var result = payslipGenerationBal.CalculatedPayslip(model);

            //Assert
            Assert.IsNotNull(result);
            Assert.IsFalse(result.SuperAmount > 0);
        }

        [Test]
        public void CalculatedPayslip_Test_Failure_Name_Failure()
        {
            //Arrange
            var model = EmployeeTestData(50000, 5, "", "");

            //Act
            var result = payslipGenerationBal.CalculatedPayslip(model);

            //Assert
            Assert.IsFalse(result.Name.Length == 0);
        }

        [Test]
        public void CalcTaxableIncome_Test()
        {
            //Act
            var result = payslipCalculationBal.CalcTaxableIncome(60050);

            //Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(922, result);
            Assert.AreNotEqual(1000, result);
        }

        [Test]
        public void CalculatedPayslip_Test_AnnualSalary_And_SuperRate_Is_Valid()
        {
            //Arrange
            var model = EmployeeTestData(20000, 5, "", "");

            //Act
            var result = payslipGenerationBal.ValidateEmployeeModel(model);

            //Assert
            Assert.IsTrue(result);
        }

        [Test]
        public void CalculatedPayslip_Test_AnnualSalary_Is_Invalid()
        {
            //Arrange
            var model = EmployeeTestData(0, 5, "", "");

            //Act
            var result = payslipGenerationBal.ValidateEmployeeModel(model);

            //Assert
            Assert.IsFalse(result);
        }

        [Test]
        public void CalculatedPayslip_Test_SuperRate_Is_Invalid()
        {
            //Arrange
            var model = EmployeeTestData(20000, 0, "", "");

            //Act
            var result = payslipGenerationBal.ValidateEmployeeModel(model);

            //Assert
            Assert.IsFalse(result);
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