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
    public class UserNewsController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly SqlConnection conn;

        public UserNewsController(IConfiguration configuration)
        {
            _configuration = configuration;
            conn = new SqlConnection(_configuration.GetConnectionString("DBCS"));
        }

        [HttpGet]
        [Route("GetUserNews")]
        public IEnumerable<News> GetNews()
        {
            DAL dal = new DAL();
            Response response = dal.GetNews(conn);

            if (response.StatusCode == 200)
            {
                return (List<News>)response.Data;
            }
            else
            {
                return new List<News>();
            }
        }

        [HttpGet]
        [Route("GetUserNewsDetail")]
        public IActionResult GetUserNewsDetail(string namenews)
        {
            DAL dal = new DAL();
            Response response = dal.GetNewsDetail(conn, namenews);

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
