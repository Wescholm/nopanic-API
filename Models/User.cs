using System.Collections;
using System.Collections.Generic;

namespace nopanic_API.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string RefreshToken { get; set; }
        public string ProfilePicture { get; set; }
        
        public int ProfileGradientId { get; set; }
        public ProfileGradient ProfileGradient { get; set; }
    }
}