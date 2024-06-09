using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Project_UDW.Models;
using System.Data.SqlClient;

namespace Project_UDW.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminChampionsController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly SqlConnection conn;
        public AdminChampionsController(IConfiguration configuration)
        {
            _configuration = configuration;
            conn = new SqlConnection(_configuration.GetConnectionString("DBCS"));
        }
        [HttpPost]
        [Route("AddChampions")]
        public Response AddChampions([FromBody] Champions champ)
        {
            DAL dal = new DAL();
            return dal.AddChampions(champ, conn);
        }
        [HttpGet]
        [Route("GetChampions")]
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
        [HttpPost]
        [Route("DeleteChamp")]
        public Response DeleteChamp([FromBody] Champions_Delete delchamp)
        {
            DAL dal = new DAL();
            return dal.DeleteChamp(delchamp, conn);
        }
        [HttpPost]
        [Route("UpdateChamp")]
        public Response UpdateChamp([FromBody] Champions updatechamp)
        {
            DAL dal = new DAL();
            return dal.UpdateChamp(updatechamp, conn);
        }
       
    }
}
