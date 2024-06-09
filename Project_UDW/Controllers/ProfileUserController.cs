using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Project_UDW.Models;
using System.Data.SqlClient;

namespace Project_UDW.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProfileUserController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly SqlConnection conn;
        public ProfileUserController(IConfiguration configuration)
        {
            _configuration = configuration;
            conn = new SqlConnection(_configuration.GetConnectionString("DBCS"));
        }
        [HttpGet]
        [Route("GetProfileUser")]
        public IEnumerable<Users> GetProfileUser()
        {
            DAL dal = new DAL();
            Response response = dal.GetProfileUser(conn);
            if (response.StatusCode == 200)
            {
                return (List<Users>)response.Data;
            }
            else
            {
                return new List<Users>(); 
            }
        }
    }
}
