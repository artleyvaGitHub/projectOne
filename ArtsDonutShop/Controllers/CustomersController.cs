using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ArtsDonutShop.Models;

namespace ArtsDonutShop.Controllers
{
    

    [Route("api/[controller]")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        CustomersModel model = new CustomersModel();
        
        private readonly ILogger<CustomersController> _logger;

        public CustomersController(ILogger<CustomersController> logger)
        {
            _logger = logger;
        }

        #region GET Customers Lists
        [HttpGet]
        [Route("Customer Information")]
        public IActionResult GetCustomers()
        {
            _logger.LogInformation("Get Customer Information executing...");
            return Ok(model.GetCustomers());
        }
        #endregion

        #region GET Customer Information By ID
        [HttpGet]
        [Route("Customer Information By ID")]
        public IActionResult GetCustomerByAccNo(int accNo)
        {
            try
            {
                _logger.LogInformation("Get Customer Information By ID executing...");
                return Ok(model.GetCustomerDetail(accNo));
            }
            catch (System.Exception es)
            {

                return BadRequest(es.Message);
            }
        }
        #endregion

        #region POST Add New Customer
        [HttpPost]
        [Route("AddNewCustomer")]
       
        public IActionResult AddCustomer(CustomersModel newCustomer)
        {
            try
            {
                _logger.LogInformation("Add New Customer executing...");
                return Created("", model.AddCustomer(newCustomer));
            }
            catch (System.Exception es)
            {

                return BadRequest(es.Message);
            }
        }
        #endregion

        #region DELETE Customer
        [HttpDelete]
        [Route("DeleteCustomer")]

        public IActionResult DeleteCustomer(int accNo)
        {
            try
            {
                _logger.LogInformation("Deleting Existing Customer executing...");
                return Accepted(model.DeleteCustomer(accNo));
            }
            catch (System.Exception es)
            {

                return BadRequest(es.Message);
            }
        }
        #endregion

        #region UPDATE Customer
        [HttpPut]
        [Route("Update Customer")]
        public IActionResult UpdateCustomer(CustomersModel updCustomer)
        {
            try
            {
                _logger.LogInformation("Update Customer executing...");
                return Accepted(model.UpdateCustomer(updCustomer));
            }
            catch (System.Exception es)
            {

                return BadRequest(es.Message);
            }
        }
        #endregion



        //#region Customers List ===for my reference only
        //[HttpGet]
        //[Route("Customers List")]
        //public IActionResult GetCustomersListings()
        //{
        //    _logger.LogInformation("Get Customer List executing...");
        //    CustomersModel model = new CustomersModel();
        //    return Ok(model.GetCustomersList());

        //}
        //#endregion

    }
}
