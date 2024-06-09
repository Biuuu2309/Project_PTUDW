using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Project_UDW.Models;
using System.Data.SqlClient;

namespace Project_UDW.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminDetailsChampController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly SqlConnection conn;
        public AdminDetailsChampController(IConfiguration configuration)
        {
            _configuration = configuration;
            conn = new SqlConnection(_configuration.GetConnectionString("DBCS"));
        }
        [HttpPost]
        [Route("AddDetailsUpChamp")]
        public Response AddDetailsUpChamp([FromBody] DetailsChamp adddetailsupchamp)
        {
            DAL dal = new DAL();
            return dal.AddDetailsUpChamp(adddetailsupchamp, conn);
        }
        [HttpGet]
        [Route("GetDetailsUpChamp")]
        public IEnumerable<DetailsChamp> GetDetailsUpChamp()
        {
            DAL dal = new DAL();
            Response response = dal.GetDetailsUpChamp(conn);

            if (response.StatusCode == 200)
            {
                return (List<DetailsChamp>)response.Data;
            }
            else
            {
                return new List<DetailsChamp>(); // Or you can throw an exception
            }
        }
        [HttpPost]
        [Route("DeleteDetailsUpChamp")]
        public Response DeleteDetailsUpChamp([FromBody] DetailsChamp_Delete deldetailsupchamp)
        {
            DAL dal = new DAL();
            return dal.DeleteDetailsUpChamp(deldetailsupchamp, conn);
        }
        [HttpPost]
        [Route("UpdateDetailsUpChamp")]
        public Response UpdateDetailsUpChamp([FromBody] DetailsChamp updetailsupchamp)
        {
            DAL dal = new DAL();
            return dal.UpdateDetailsUpChamp(updetailsupchamp, conn);
        }
    }
}
