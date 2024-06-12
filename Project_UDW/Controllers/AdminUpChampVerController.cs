using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Project_UDW.Models;
using System.Data.SqlClient;

namespace Project_UDW.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminUpChampVerController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly SqlConnection conn;
        public AdminUpChampVerController(IConfiguration configuration)
        {
            _configuration = configuration;
            conn = new SqlConnection(_configuration.GetConnectionString("DBCS"));
        }
        [HttpPost]
        [Route("AddUpChampVer")]
        public Response AddUpChampVer([FromBody] UpChampVer upchampver)
        {
            DAL dal = new DAL();
            return dal.AddUpChampVer(upchampver, conn);
        }
        [HttpGet]
        [Route("GetUpChampVer")]
        public IEnumerable<UpChampVer> GetUpChampVer()
        {
            DAL dal = new DAL();
            Response response = dal.GetUpChampVer(conn);

            if (response.StatusCode == 200)
            {
                return (List<UpChampVer>)response.Data;
            }
            else
            {
                return new List<UpChampVer>();
            }
        }
        [HttpPost]
        [Route("DeleteUpChampVer")]
        public Response DeleteUpChampVer([FromBody] UpChampVer_Delete delupchampver)
        {
            DAL dal = new DAL();
            return dal.DeleteUpChampVer(delupchampver, conn);
        }
        [HttpPost]
        [Route("UpdateUpChampVer")]
        public Response UpdateUpChampVer([FromBody] UpChampVer upupchampver)
        {
            DAL dal = new DAL();
            return dal.UpdateUpChampVer(upupchampver, conn);
        }
    }
}
