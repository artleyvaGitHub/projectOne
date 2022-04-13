using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ArtsDonutShop.Models;
using Serilog;

namespace ArtsDonutShop.Controllers
{
    
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        OrdersModel orders = new OrdersModel();
        
        private readonly ILogger<OrdersController> _logger;

        public OrdersController(ILogger<OrdersController> logger)
        {
            _logger = logger;
        }

        #region GET Order Lists
        [HttpGet]
        [Route("Order Information")]
        public IActionResult GetOrders()
        {
            _logger.LogInformation("Get Order Information executing...");
            return Ok(orders.GetOrders());
        }
        #endregion

        #region GET Order Information By OrderNo
        [HttpGet]
        [Route("Order Information By OrderNo")]
        public IActionResult GetOrderDetail(int ordNo)
        {
            try
            {
                _logger.LogInformation("Get Order Information By Order Number executing...");
                return Ok(orders.GetOrderDetail(ordNo));
            }
            catch (System.Exception es)
            {

                return BadRequest(es.Message);
            }
        }
        #endregion

        #region POST Add New Order
        [HttpPost]
        [Route("AddNewOrder")]

        public IActionResult AddOrder(OrdersModel newOrder)
        {
            try
            {
                _logger.LogInformation("Add New Order executing...");
                return Created("", orders.AddOrder(newOrder));
            }
            catch (System.Exception es)
            {

                return BadRequest(es.Message);
            }
        }
        #endregion

        #region DELETE Customer
        [HttpDelete]
        [Route("DeleteOrder")]

        public IActionResult DeleteOrder(int ordNo)
        {
            try
            {
                _logger.LogInformation("Delete Order executing...");
                return Accepted(orders.DeleteOrder(ordNo));
            }
            catch (System.Exception es)
            {

                return BadRequest(es.Message);
            }
        }
        #endregion

        #region UPDATE Order
        [HttpPut]
        [Route("Update Order")]
        public IActionResult UpdateOrder(OrdersModel updOrder)
        {
            try
            {
                _logger.LogInformation("Update Order executing...");
                return Accepted(orders.UpdateOrder(updOrder));
            }
            catch (System.Exception es)
            {

                return BadRequest(es.Message);
            }
        }
        #endregion
    }
}
