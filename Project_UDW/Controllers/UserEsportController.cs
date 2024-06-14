using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Project_UDW.Models;
using System.Data.SqlClient;

namespace Project_UDW.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserEsportController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly SqlConnection conn;

        public UserEsportController(IConfiguration configuration)
        {
            _configuration = configuration;
            conn = new SqlConnection(_configuration.GetConnectionString("DBCS"));
        }

        [HttpGet]
        [Route("GetUserKhuVuc")]
        public IActionResult GetKhuVuc()
        {
            DAL dal = new DAL();
            Response response = dal.GetKhuVuc(conn);

            if (response.StatusCode == 200)
            {
                return Ok(response.Data);
            }
            else
            {
                return NotFound(response.StatusMessage);
            }
        }

        [HttpGet]
        [Route("GetUserTeamDetail")]
        public IActionResult GetUserTeamDetail(string makhuvuc)
        {
            DAL dal = new DAL();
            Response response = dal.GetUserTeamDetail(conn, makhuvuc);

            if (response.StatusCode == 200)
            {
                return Ok(response.Data);
            }
            else
            {
                return NotFound(response.StatusMessage);
            }
        }
    }
}
