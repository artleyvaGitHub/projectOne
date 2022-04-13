using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ArtsDonutShop.Models;
using Serilog;

namespace ArtsDonutShop.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ItemDetailsController : ControllerBase
    {
        ItemDetailsModel item = new ItemDetailsModel();

        private readonly ILogger<ItemDetailsController> _logger;

        public ItemDetailsController(ILogger<ItemDetailsController> logger)
        {
            _logger = logger;
        }

        #region GET Item Lists
        [HttpGet]
        [Route("Item Information")]
        public IActionResult GetItems()
        {
            _logger.LogInformation("Get Item Information executing...");
            return Ok(item.GetItems());
        }
        #endregion

        #region GET Item Information By Item No
        [HttpGet]
        [Route("Item Information By Item No")]
        public IActionResult GetItemDetail(int itemNo)
        {
            try
            {
                _logger.LogInformation("Get Item Information By Item No executing...");
                return Ok(item.GetItemDetail(itemNo));
            }
            catch (System.Exception es)
            {

                return BadRequest(es.Message);
            }
        }
        #endregion

        #region POST Add New Item
        [HttpPost]
        [Route("AddNewItem")]

        public IActionResult AddItem(ItemDetailsModel newItem)
        {
            try
            {
                _logger.LogInformation("Add New Item executing...");
                return Created("", item.AddItem(newItem));
            }
            catch (System.Exception es)
            {

                return BadRequest(es.Message);
            }
        }
        #endregion

        #region DELETE Item
        [HttpDelete]
        [Route("DeleteItem")]

        public IActionResult DeleteItem(int itemNo)
        {
            try
            {
                return Accepted(item.DeleteItem(itemNo));
            }
            catch (System.Exception es)
            {

                return BadRequest(es.Message);
            }
        }
        #endregion

        #region UPDATE Item
        [HttpPut]
        [Route("Update Item")]
        public IActionResult UpdateItem(ItemDetailsModel updItem)
        {
            try
            {
                _logger.LogInformation("Update Item executing...");
                return Accepted(item.UpdateItem(updItem));
            }
            catch (System.Exception es)
            {

                return BadRequest(es.Message);
            }
        }
        #endregion    
    }
}
