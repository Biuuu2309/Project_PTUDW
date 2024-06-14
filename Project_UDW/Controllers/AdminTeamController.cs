using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Project_UDW.Models;
using System.Data.SqlClient;

namespace Project_UDW.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminTeamController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly SqlConnection conn;
        public AdminTeamController(IConfiguration configuration)
        {
            _configuration = configuration;
            conn = new SqlConnection(_configuration.GetConnectionString("DBCS"));
        }
        [HttpPost]
        [Route("AddTeam")]
        public Response AddTeam([FromBody] Team team)
        {
            DAL dal = new DAL();
            return dal.AddTeam(team, conn);
        }
        [HttpGet]
        [Route("GetTeam")]
        public IEnumerable<Team> GetTeam()
        {
            DAL dal = new DAL();
            Response response = dal.GetTeam(conn);

            if (response.StatusCode == 200)
            {
                return (List<Team>)response.Data;
            }
            else
            {
                return new List<Team>(); // Or you can throw an exception
            }
        }
        [HttpPost]
        [Route("DeleteTeam")]
        public Response DeleteTeam([FromBody] Team_Delete delteam)
        {
            DAL dal = new DAL();
            return dal.DeleteTeam(delteam, conn);
        }
        [HttpPost]
        [Route("UpdateTeam")]
        public Response UpdateTeam([FromBody] Team updateteam)
        {
            DAL dal = new DAL();
            return dal.UpdateTeam(updateteam, conn);
        }
    }
}
