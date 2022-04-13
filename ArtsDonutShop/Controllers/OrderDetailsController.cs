using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ArtsDonutShop.Models;

namespace ArtsDonutShop.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderDetailsController : ControllerBase
    {
        OrderDetailsModel orddetail = new OrderDetailsModel();

        private readonly ILogger<OrderDetailsController> _logger;

        public OrderDetailsController(ILogger<OrderDetailsController> logger)
        {
            _logger = logger;
        }

        #region GET Item Lists
        [HttpGet]
        [Route("Order Detail Information")]
        public IActionResult GetOrdersDetails()
        {
            _logger.LogInformation("Get Order Detail executing...");
            return Ok(orddetail.GetOrdersDetails());
        }
        #endregion

        #region GET Order Information By Order No
        [HttpGet]
        [Route("Order Detail Information By Order No")]
        public IActionResult GetOrderDetailsItem(int ordNo)
        {
            try
            {
                _logger.LogInformation("Get Order Detail by Order No executing...");
                return Ok(orddetail.GetOrderDetailsItem(ordNo));
            }
            catch (System.Exception es)
            {

                return BadRequest(es.Message);
            }
        }
        #endregion

        #region POST Add New Order Detail
        [HttpPost]
        [Route("AddNewOrderDetail")]

        public IActionResult AddOrder(OrderDetailsModel newOrder)
        {
            try
            {
                _logger.LogInformation("Add New Order Detail executing...");
                return Created("", orddetail.AddOrder(newOrder));
            }
            catch (System.Exception es)
            {

                return BadRequest(es.Message);
            }
        }
        #endregion

        #region DELETE Order Detail
        [HttpDelete]
        [Route("DeleteOrderDetail")]

        public IActionResult DeleteOrderDetail(int ordNo)
        {
            try
            {
                _logger.LogInformation("Delete Order Detail executing...");
                return Accepted(orddetail.DeleteOrderDetail(ordNo));
            }
            catch (System.Exception es)
            {

                return BadRequest(es.Message);
            }
        }
        #endregion

        #region UPDATE Order Detail
        [HttpPut]
        [Route("Update Order Detail")]
        public IActionResult UpdateItem(OrderDetailsModel updOrderD)
        {
            try
            {
                _logger.LogInformation("Update Order Detail executing...");
                return Accepted(orddetail.UpdateOrder(updOrderD));
            }
            catch (System.Exception es)
            {

                return BadRequest(es.Message);
            }
        }
        #endregion    
    }
}
