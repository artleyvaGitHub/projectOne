using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ArtsDonutShop.Models;
using Serilog;

namespace ArtsDonutShop.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        LoginModels ulogin = new LoginModels();

        private readonly ILogger<LoginController> _logger;

        public LoginController(ILogger<LoginController> logger)
        {
            _logger = logger;
        }

        #region GET Order Lists
        [HttpGet]
        [Route("Login Information")]
        public IActionResult GetLoginList()
        {
            _logger.LogInformation("Get Login Information executing...");
            return Ok(ulogin.GetLoginList());
        }
        #endregion


        #region POST Add New User
        [HttpPost]
        [Route("AddNewUser")]

        public IActionResult AddNewUser(LoginModels newUser)
        {
            try
            {
                _logger.LogInformation("Add New User executing...");
                return Created("", ulogin.AddNewUser(newUser));
            }
            catch (System.Exception es)
            {

                return BadRequest(es.Message);
            }
        }
        #endregion

    }
}
