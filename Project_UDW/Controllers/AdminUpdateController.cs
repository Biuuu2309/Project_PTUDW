using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Project_UDW.Models;
using System.Data.SqlClient;

namespace Project_UDW.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminUpdateController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly SqlConnection conn;
        public AdminUpdateController(IConfiguration configuration)
        {
            _configuration = configuration;
            conn = new SqlConnection(_configuration.GetConnectionString("DBCS"));
        }
        [HttpPost]
        [Route("AddUpHead")]
        public Response AddUpHead([FromBody] Update_Head uphead)
        {
            DAL dal = new DAL();
            return dal.AddUpHead(uphead, conn);
        }
        [HttpGet]
        [Route("GetUpHead")]
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
        [HttpPost]
        [Route("DeleteUpHead")]
        public Response DeleteUpHead([FromBody] Update_Head_Delete upheaddel)
        {
            DAL dal = new DAL();
            return dal.DeleteUpHead(upheaddel, conn);
        }
        [HttpPost]
        [Route("UpdateUpHead")]
        public Response UpdateUpHead([FromBody] Update_Head upuphead)
        {
            DAL dal = new DAL();
            return dal.UpdateUpHead(upuphead, conn);
        }
    }
}
