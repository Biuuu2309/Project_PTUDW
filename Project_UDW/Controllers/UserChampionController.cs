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

        [HttpGet]
        [Route("GetUserChampionDetail")]
        public IActionResult GetUserChampionDetail(string champName)
        {
            DAL dal = new DAL();
            Response response = dal.GetChampionDetail(conn, champName);

            if (response.StatusCode == 200)
            {
                return Ok(response.Data);
            }
            else
            {
                return NotFound();
            }
        }
        [HttpGet]
        [Route("GetUserChampionSearch")]
        public IActionResult GetUserChampionSearch(string champName)
        {
            DAL dal = new DAL();
            Response response = dal.GetUserChampionSearch(conn, champName);

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
