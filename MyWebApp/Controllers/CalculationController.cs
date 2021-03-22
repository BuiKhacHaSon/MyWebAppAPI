using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DAL.Models;
using DAL.Services;
using Microsoft.AspNetCore.Mvc;

namespace MyWebApp.Controllers
{
    [Route("cal")]
    public class CalculationController : Controller
    {
        protected CalculationService calculationService;
        public CalculationController(CalculationService calculationService)
        {
            this.calculationService = calculationService;
        }
        public string Agent
        {
            get
            {
                return HttpContext.Request.Headers["User-Agent"].ToString();
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetAllCalculations()
        {
            var result = await calculationService.GetCalculationsAsync();
            return new OkObjectResult(result);
        }

        [Route("new")]
        [HttpGet]
        public async Task<IActionResult> GetAllNewCalculation()
        {
            IEnumerable<Calculation> result = await calculationService.GetNewCalculationsAsync();
            return new OkObjectResult(result);
        }

        [HttpPost]
        public async Task<IActionResult> CreateCalculation([FromBody] Calculation calculation)
        {
            try
            {
                calculation.Agent = Agent;
                calculation.CreatedAt = DateTime.UtcNow;
                calculation = await calculationService.Calculate(calculation);
                await calculationService.CreateCalculationAsync(calculation);
                return new OkObjectResult(calculation);
            }
            catch (Exception ex)
            {
                return new BadRequestObjectResult(new { Message = ex.Message });
            }
        }

        [Route("{id:int}")]
        [HttpDelete]
        public async Task<IActionResult> DeleteCalculation(int id)
        {
            try
            {
                await calculationService.DeleteAsync(id);
                return new OkResult();
            }
            catch (Exception ex)
            {
                return new BadRequestObjectResult(new { Message = ex.Message });
            }

        }

        [Route("{id:int}")]
        [HttpPut]
        public async Task<IActionResult> CheckRead(int id)
        {
            try
            {
                await calculationService.CheckReadAsync(id);
                return new OkResult();
            }
            catch (Exception ex)
            {
                return new BadRequestObjectResult(new { Message = ex.Message });
            }
        }
    }
}