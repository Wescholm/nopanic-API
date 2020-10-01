using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using nopanic_API.Data.Repository.UsersRepository;
using nopanic_API.Models;
using nopanic_API.Models.Context;

namespace nopanic_API.Controllers
{
    [Route("API/[controller]")]
    [ApiController]
    public class UsersController : Controller
    {
        private IUsersRepository _repository;
        public UsersController(IConfiguration config)
        {
            _repository = new UsersRepository(config);
        }
        
        [HttpPost("AddUserPhoneNumber")]
        public IActionResult AddUserPhoneNumber([FromBody] JWTPayload payload)
        {
            var isAdded = _repository.AddUserPhoneNumber(payload.phone_number, payload.user_guid);
            if (isAdded) return Accepted();
            return BadRequest("Invalid user guid or incorrect phone number");
        }
        
        [HttpPost("VerifyUserPhoneNumber")]
        public IActionResult CheckUserAvailability([FromBody] VerificationData data)
        {
            VerificationData verificationData = _repository.VerifyUserPhoneNumber(data.phoneNumber);
            if (verificationData.verificationCode != null)
            {
                return Ok(verificationData);
            }
            return BadRequest("Test");
        }
    }
}