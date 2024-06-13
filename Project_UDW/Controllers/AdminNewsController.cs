using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Project_UDW.Models;
using System.Data.SqlClient;
namespace Project_UDW.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminNewsController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly SqlConnection conn;
        public AdminNewsController(IConfiguration configuration)
        {
            _configuration = configuration;
            conn = new SqlConnection(_configuration.GetConnectionString("DBCS"));
        }
        [HttpPost]
        [Route("AddNews")]
        public Response AddNews([FromBody] News news)
        {
            DAL dal = new DAL();
            return dal.AddNews(news, conn);
        }
        [HttpGet]
        [Route("GetNews")]
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
                return new List<News>(); // Or you can throw an exception
            }
        }
        [HttpPost]
        [Route("DeleteNews")]
        public Response DeleteNews([FromBody] News_Delete delnews)
        {
            DAL dal = new DAL();
            return dal.DeleteNews(delnews, conn);
        }
        [HttpPost]
        [Route("UpdateNews")]
        public Response UpdateNews([FromBody] News updatenews)
        {
            DAL dal = new DAL();
            return dal.UpdateNews(updatenews, conn);
        }
    }
}
