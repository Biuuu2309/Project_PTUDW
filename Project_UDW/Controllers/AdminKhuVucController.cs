using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Project_UDW.Models;
using System.Data.SqlClient;

namespace Project_UDW.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminKhuVucController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly SqlConnection conn;
        public AdminKhuVucController(IConfiguration configuration)
        {
            _configuration = configuration;
            conn = new SqlConnection(_configuration.GetConnectionString("DBCS"));
        }
        [HttpPost]
        [Route("AddKhuVuc")]
        public Response AddKhuVuc([FromBody] KhuVuc khuvuc)
        {
            DAL dal = new DAL();
            return dal.AddKhuVuc(khuvuc, conn);
        }
        [HttpGet]
        [Route("GetKhuVuc")]
        public IEnumerable<KhuVuc> GetKhuVuc()
        {
            DAL dal = new DAL();
            Response response = dal.GetKhuVuc(conn);

            if (response.StatusCode == 200)
            {
                return (List<KhuVuc>)response.Data;
            }
            else
            {
                return new List<KhuVuc>(); 
            }
        }
        [HttpPost]
        [Route("DeleteKhuVuc")]
        public Response DeleteKhuVuc([FromBody] KhuVuc_Delete delkhuvuc)
        {
            DAL dal = new DAL();
            return dal.DeleteKhuVuc(delkhuvuc, conn);
        }
        [HttpPost]
        [Route("UpdateKhuVuc")]
        public Response UpdateKhuVuc([FromBody] KhuVuc updatekhuvuc)
        {
            DAL dal = new DAL();
            return dal.UpdateKhuVuc(updatekhuvuc, conn);
        }
    }
}
