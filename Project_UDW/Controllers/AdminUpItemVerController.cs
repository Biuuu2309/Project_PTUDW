using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Project_UDW.Models;
using System.Data.SqlClient;

namespace Project_UDW.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminUpItemVerController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly SqlConnection conn;
        public AdminUpItemVerController(IConfiguration configuration)
        {
            _configuration = configuration;
            conn = new SqlConnection(_configuration.GetConnectionString("DBCS"));
        }
        [HttpPost]
        [Route("AddUpItemVer")]
        public Response AddUpItemVer([FromBody] UpItemVer upitemver)
        {
            DAL dal = new DAL();
            return dal.AddUpItemVer(upitemver, conn);
        }
        [HttpGet]
        [Route("GetUpItemVer")]
        public IEnumerable<UpItemVer> GetUpItemVer()
        {
            DAL dal = new DAL();
            Response response = dal.GetUpItemVer(conn);

            if (response.StatusCode == 200)
            {
                return (List<UpItemVer>)response.Data;
            }
            else
            {
                return new List<UpItemVer>();
            }
        }
        [HttpPost]
        [Route("DeleteUpItemVer")]
        public Response DeleteUpItemVer([FromBody] UpItemVer_Delete delupitemver)
        {
            DAL dal = new DAL();
            return dal.DeleteUpItemVer(delupitemver, conn);
        }
        [HttpPost]
        [Route("UpdateUpItemVer")]
        public Response UpdateUpItemVer([FromBody] UpItemVer upupitemver)
        {
            DAL dal = new DAL();
            return dal.UpdateUpItemVer(upupitemver, conn);
        }
    }
}
