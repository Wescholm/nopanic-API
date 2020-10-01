using System;
using System.Linq;
using Microsoft.Extensions.Configuration;
using nopanic_API.Models;
using nopanic_API.Models.Context;
using Telesign;

namespace nopanic_API.Data.Repository.UsersRepository
{
    public class UsersRepository: IUsersRepository
    {
        MainDbContext db = new MainDbContext();
        private readonly IConfiguration _config;

        public UsersRepository(IConfiguration config)
        {
            _config = config;
        }

        public bool AddUserPhoneNumber(string phoneNumber, string userGuid)
        {
            var user = db.User.FirstOrDefault(u => u.Guid == userGuid);
            if (user == default) return false;
            try
            {
                user.PhoneNumber = phoneNumber;
                db.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }
        public VerificationData VerifyUserPhoneNumber(string phoneNumber)
        {
            var customerId = _config.GetValue<string>("Telesign:CustomerID");
            string apiKey = _config.GetValue<string>("Telesign:ApiKey");;

            string verifyCode = GenerateVerifyNumber();
            string message = string.Format("Your confirmation code is {0}", verifyCode);
            string messageType = "OTP";
            
            try
            {
                MessagingClient messagingClient = new MessagingClient(customerId, apiKey);
                RestClient.TelesignResponse response = messagingClient.Message(phoneNumber, message, messageType);
                VerificationData verificationData = new VerificationData()
                {
                    phoneNumber = phoneNumber,
                    verificationCode = response.StatusCode == 200 ? verifyCode : null
                };

                return verificationData;
            }
            catch (Exception e)
            {
                return new VerificationData()
                {
                    message = e.ToString()
                };
            }
        }

        private string GenerateVerifyNumber()
        {
            Random random = new Random();
            var randomNumber = random.Next(100000, 999999);
            return randomNumber.ToString();
        }
    }
}