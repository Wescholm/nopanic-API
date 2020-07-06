using System.Collections;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
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
        public List<Test> Get()
        {
            var result = from v in db.Test 
                where v.Action != "Test2"
                select v;
            return result.ToList();
        }
        
        [HttpPost("Test")]
        public void Post()
        {
            var data = new Test
            {
               Action = "Added",
               OperationTime = 999000
            };

            db.Add(data);
            db.SaveChanges();
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