using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using nopanic_API.Models.Context;

namespace nopanic_API.Controllers
{
    [Authorize(Policy = "MainPolicy")]
    [Route("API/[controller]")]
    [ApiController]
    public class TestingController: Controller
    {
        MainDbContext db = new MainDbContext();

        [HttpGet("Test")]
        public int Get()
        {

            return 1 + 1;
        }
        
        [HttpPost("Test")]
        public object Post([FromBody] object user)
        {
            return 1;
        }
        
        [HttpPut("Test")]
        public void Put()
        {
            var el = db.Test.Where(v => v.Id == 5).FirstOrDefault();
            if (el != null) el.OperationTime = 3308;
            db.SaveChanges();
        }
        
        [HttpDelete("Test")]
        public void Delete()
        {
            var el = db.Test.Find(5);
            if (el != null) db.Remove(el);
            db.SaveChanges();
        }
    }
}