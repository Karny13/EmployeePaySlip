using EmployeePayslip.BAL;
using EmployeePayslip.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EmployeePaySlip.Controllers
{
    [Route("[controller]")]
    public class PayslipController : Controller
    {
        private readonly PayslipGenerationBal _payslipGenerationBal;

        public PayslipController()
        {
            _payslipGenerationBal= new PayslipGenerationBal();
        }

        [HttpPost]
        public IActionResult CalculatedPayslip([FromBody] EmployeeDetailsBo model)
        {
            if (!_payslipGenerationBal.ValidateEmployeeModel(model))
                return StatusCode(StatusCodes.Status400BadRequest);

            var payslip = _payslipGenerationBal.CalculatedPayslip(model);
            return Ok(payslip);
        }
    }
}