using System.ComponentModel.DataAnnotations;

namespace nopanic_API.Models
{
    public class User
    {
        public int Id { get; set; }
        public string ProfilePicture { get; set; }
        [StringLength(255)]
        public string Email { get; set; }
        [StringLength(255)]
        public string UserName { get; set; }
        [StringLength(255)]
        public string Password { get; set; }
        [StringLength(36, MinimumLength = 36)]
        public string Guid { get; set; }
        [StringLength(36, MinimumLength = 36)]
        public string RefreshToken { get; set; }
        #nullable enable
        [StringLength(12, MinimumLength = 12)]
        public string? PhoneNumber { get; set; }
        #nullable disable
        public int ProfileGradientId { get; set; }
        public ProfileGradient ProfileGradient { get; set; }
    }
}