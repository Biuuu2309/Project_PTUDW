using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Project_UDW.Models;
using System.Data.SqlClient;

namespace Project_UDW.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminSkillController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly SqlConnection conn;
        public AdminSkillController(IConfiguration configuration)
        {
            _configuration = configuration;
            conn = new SqlConnection(_configuration.GetConnectionString("DBCS"));
        }
        [HttpPost]
        [Route("AddSkills")]
        public Response AddSkills([FromBody] Skills skills)
        {
            DAL dal = new DAL();
            return dal.AddSkills(skills, conn);
        }
        [HttpGet]
        [Route("GetSkills")]
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
        [HttpPost]
        [Route("DeleteSkill")]
        public Response DeleteSkill([FromBody] Skills_Delete delskill)
        {
            DAL dal = new DAL();
            return dal.DeleteSkill(delskill, conn);
        }
        [HttpPost]
        [Route("UpdateSkill")]
        public Response UpdateSkill([FromBody] Skills updateskill)
        {
            DAL dal = new DAL();
            return dal.UpdateSkill(updateskill, conn);
        }
    }
}
