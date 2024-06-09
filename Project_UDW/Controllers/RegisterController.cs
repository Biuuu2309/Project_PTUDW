using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Project_UDW.Models;
using System.Data.SqlClient;

namespace Project_UDW.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegisterController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly SqlConnection conn;

        public RegisterController(IConfiguration configuration)
        {
            _configuration = configuration;
            conn = new SqlConnection(_configuration.GetConnectionString("DBCS"));
        }

        [HttpPost]
        [Route("RegisterNewUser")]
        public Response RegisterNewUser([FromBody] Users us)
        {
            DAL dal = new DAL();
            return dal.Registration(us, conn);
        }
        // Assuming this is your Login endpoint
        [HttpPost]
        [Route("Login")]
        public Response Login([FromBody] LoginDTO loginDto)
        {
            DAL dal = new DAL();
            return dal.Login(new Users { Email = loginDto.Email, Password = loginDto.Password }, conn);
        }
    }

}
