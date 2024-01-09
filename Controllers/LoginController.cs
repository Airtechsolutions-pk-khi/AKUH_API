using AKUH_API.Models;
using AKUH_API.Repositories;
using Microsoft.AspNetCore.Mvc;
 

namespace AKUH_API.Controllers
{     
    [ApiController]
    [Route("[Controller]")]
    public class LoginController : ControllerBase
    {
        LoginRepository _loginRepo;

        public LoginController()
        {
            _loginRepo = new LoginRepository();
        }
        [HttpGet]
        [Route("login/{email}/{password}")]
        public async Task<RspLogin> loginCustomer(string email, string password)
        {
            var data =  _loginRepo.GetCustomerInfo(email, password);
            return await data;
        }

    }
}