using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Project_UDW.Models;
using System.Data.SqlClient;

namespace Project_UDW.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminSkinController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly SqlConnection conn;
        public AdminSkinController(IConfiguration configuration)
        {
            _configuration = configuration;
            conn = new SqlConnection(_configuration.GetConnectionString("DBCS"));
        }
        [HttpPost]
        [Route("AddSkin")]
        public Response AddSkin([FromBody] Skin skin)
        {
            DAL dal = new DAL();
            return dal.AddSkin(skin, conn);
        }
        [HttpGet]
        [Route("GetSkin")]
        public IEnumerable<Skin> GetSkin()
        {
            DAL dal = new DAL();
            Response response = dal.GetSkin(conn);

            if (response.StatusCode == 200)
            {
                return (List<Skin>)response.Data;
            }
            else
            {
                return new List<Skin>();
            }
        }
        [HttpPost]
        [Route("DeleteSkin")]
        public Response DeleteSkin([FromBody] Skin_Delete delskin)
        {
            DAL dal = new DAL();
            return dal.DeleteSkin(delskin, conn);
        }
        [HttpPost]
        [Route("UpdateSkin")]
        public Response UpdateSkin([FromBody] Skin updateskin)
        {
            DAL dal = new DAL();
            return dal.UpdateSkin(updateskin, conn);
        }
    }
}
