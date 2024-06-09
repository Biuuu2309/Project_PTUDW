using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Project_UDW.Models;
using System.Data.SqlClient;

namespace Project_UDW.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserChampionController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly SqlConnection conn;
        public UserChampionController(IConfiguration configuration)
        {
            _configuration = configuration;
            conn = new SqlConnection(_configuration.GetConnectionString("DBCS"));
        }
        [HttpGet]
        [Route("GetUserChampion")]
        public IEnumerable<Champions> GetChampions()
        {
            DAL dal = new DAL();
            Response response = dal.GetChampions(conn);

            if (response.StatusCode == 200)
            {
                return (List<Champions>)response.Data;
            }
            else
            {
                return new List<Champions>(); // Or you can throw an exception
            }
        }
    }
}
