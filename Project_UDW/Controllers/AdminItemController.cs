using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Project_UDW.Models;
using System.Data.SqlClient;

namespace Project_UDW.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminItemController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly SqlConnection conn;
        public AdminItemController(IConfiguration configuration)
        {
            _configuration = configuration;
            conn = new SqlConnection(_configuration.GetConnectionString("DBCS"));
        }
        [HttpPost]
        [Route("AddItem")]
        public Response AddItem([FromBody] Item item)
        {
            DAL dal = new DAL();
            return dal.AddItem(item, conn);
        }
        [HttpGet]
        [Route("GetItem")]
        public IEnumerable<Item> GetItem()
        {
            DAL dal = new DAL();
            Response response = dal.GetItem(conn);

            if (response.StatusCode == 200)
            {
                return (List<Item>)response.Data;
            }
            else
            {
                return new List<Item>(); // Or you can throw an exception
            }
        }
        [HttpPost]
        [Route("DeleteItem")]
        public Response DeleteItem([FromBody] Item_Delete delitem)
        {
            DAL dal = new DAL();
            return dal.DeleteItem(delitem, conn);
        }
        [HttpPost]
        [Route("UpdateItem")]
        public Response UpdateItem([FromBody] Item updateitem)
        {
            DAL dal = new DAL();
            return dal.UpdateItem(updateitem, conn);
        }
    }
}
