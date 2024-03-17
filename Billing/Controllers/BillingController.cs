using Billing.Data;
using Billing.Entity;
using Billing.Services;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Billing.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BillingController : ControllerBase
    {
        private readonly ILogger<BillingController> _logger;
        private readonly IBillingService _billingService;

        public BillingController(ILogger<BillingController> logger, IBillingService billingService)
        {
            _logger = logger;
            _billingService = billingService;
        }

        [HttpPost]
        public async Task<IActionResult> GetBill(Usage input) 
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return StatusCode(StatusCodes.Status400BadRequest, ModelState);
                }

                var total = await _billingService.ComputeBill(input);
                return StatusCode(StatusCodes.Status201Created, $"BillingAmount : {total}");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);                
                return StatusCode(StatusCodes.Status503ServiceUnavailable);
            } 
        }
    }
}
