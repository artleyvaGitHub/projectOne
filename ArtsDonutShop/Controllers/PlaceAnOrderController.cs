using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ArtsDonutShop.Models;
using Serilog;

namespace ArtsDonutShop.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlaceAnOrderController : ControllerBase
    {
        PlaceAnOrderModel placeanorder = new PlaceAnOrderModel();

        private readonly ILogger<PlaceAnOrderController> _logger;

        public PlaceAnOrderController(ILogger<PlaceAnOrderController> logger)
        {
            _logger = logger;
        }

        #region Place An Order
        [HttpPost]
        [Route("PlaceAnOrder")]
        public IActionResult PlaceAnOrder(int accNo, List<PlaceAnOrderModel> placeAnOrders )
        {
            try
            {
                _logger.LogInformation("Place An Order executing...");
                return Created("", placeanorder.PlaceAnOrder(accNo, placeAnOrders));
            }
            catch (System.Exception es)
            {

                return BadRequest(es.Message);
            }

        }
        #endregion
    }
}
