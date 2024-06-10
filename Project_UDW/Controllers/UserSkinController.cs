using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Project_UDW.Models;
using System.Data.SqlClient;


namespace Project_UDW.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserSkinController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly SqlConnection conn;
        public UserSkinController(IConfiguration configuration)
        {
            _configuration = configuration;
            conn = new SqlConnection(_configuration.GetConnectionString("DBCS"));
        }
        [HttpGet]
        [Route("GetUserSkin")]
        public IEnumerable<Skin> GetSkin()
        {
            DAL dal = new DAL();
            Response response = dal.GetSkin(conn);

            if (response.StatusCode == 200)
            {
                return (List<Skin>)response.Data;
            }
            else
            {
                return new List<Skin>();
            }
        }
        [HttpGet]
        [Route("GetUserSkinDetail")]
        public IActionResult GetSkinDetail(string champName)
        {
            DAL dal = new DAL();
            Response response = dal.GetSkinDetail(conn, champName);

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
