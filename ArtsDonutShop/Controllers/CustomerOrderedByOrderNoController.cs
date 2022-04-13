using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ArtsDonutShop.Models;
using Serilog;

namespace ArtsDonutShop.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerOrderedByOrderNoController : ControllerBase
    {
        CustomerOrderedByOrderNo custorddetail = new CustomerOrderedByOrderNo();

        private readonly ILogger<CustomerOrderedByOrderNoController> _logger;

        public CustomerOrderedByOrderNoController(ILogger<CustomerOrderedByOrderNoController> logger)
        {
            _logger = logger;
        }

        #region GET Customer Ordered Detail by Order No
        [HttpGet]
        [Route("CustomerOrderedDetailbyOrderNo")]
        public IActionResult GetCustomerOrderedDetails(int ordNo)
        {
            _logger.LogInformation("Customer Ordered Detail By Order Number executing...");
            return Ok(custorddetail.GetCustomerOrderedDetails(ordNo));
            
        }
        #endregion
        
        
    }
}
