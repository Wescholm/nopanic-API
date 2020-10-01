namespace nopanic_API.Models.Context
{
    public class JWTPayload
    {
        public string email { get; set; }
        public string unique_name { get; set; }
        public string phone_number { get; set; }
        public string user_guid { get; set; }
    }
}