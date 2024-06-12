using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Project_UDW.Models;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace Project_UDW.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserUpdateController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly SqlConnection conn;

        public UserUpdateController(IConfiguration configuration)
        {
            _configuration = configuration;
            conn = new SqlConnection(_configuration.GetConnectionString("DBCS"));
        }

        [HttpGet]
        [Route("GetUserUpdate")]
        public IEnumerable<Update_Head> GetUpHead()
        {
            DAL dal = new DAL();
            Response response = dal.GetUpHead(conn);

            if (response.StatusCode == 200)
            {
                return (List<Update_Head>)response.Data;
            }
            else
            {
                return new List<Update_Head>(); 
            }
        }

        [HttpGet]
        [Route("GetUserUpdateDetail")]
        public IActionResult GetUserUpdateDetail(string version_update)
        {
            DAL dal = new DAL();
            Response response = dal.GetUpdateDetail(conn, version_update);

            if (response.StatusCode == 200)
            {
                return Ok(response.Data);
            }
            else
            {
                return NotFound();
            }
        }
    }
}
