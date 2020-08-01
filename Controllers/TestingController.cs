using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using nopanic_API.Models;
using nopanic_API.Models.Context;

namespace nopanic_API.Controllers
{
    [Route("API/[controller]")]
    [ApiController]
    public class TestingController: Controller
    {
        MainDbContext db = new MainDbContext();

        [HttpGet("Test")]
        public object Get()
        {
            var token = Guid.NewGuid().ToString();
            return token;
        }
        
        [HttpPost("Test")]
        public User Post([FromBody] User userData)
        {
            return userData;
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