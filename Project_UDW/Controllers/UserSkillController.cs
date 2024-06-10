using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Project_UDW.Models;
using System.Data.SqlClient;

namespace Project_UDW.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserSkillController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly SqlConnection conn;
        public UserSkillController(IConfiguration configuration)
        {
            _configuration = configuration;
            conn = new SqlConnection(_configuration.GetConnectionString("DBCS"));
        }
        [HttpGet]
        [Route("GetUserSkill")]
        public IEnumerable<Skills> GetSkills()
        {
            DAL dal = new DAL();
            Response response = dal.GetSkills(conn);

            if (response.StatusCode == 200)
            {
                return (List<Skills>)response.Data;
            }
            else
            {
                return new List<Skills>();
            }
        }
        [HttpGet]
        [Route("GetUserSkillDetail")]
        public IActionResult GetSkillDetail(string champName)
        {
            DAL dal = new DAL();
            Response response = dal.GetSkillDetail(conn, champName);

            if (response.StatusCode == 200)
            {
                return Ok(response.Data);
            }
            else
            {
                return NotFound();
            }
        }
    }
}
