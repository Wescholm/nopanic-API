using nopanic_API.Models;

namespace nopanic_API.Data.Repository.AccountRepository
{
    public interface IAccountRepository
    {
        bool AddUserPhoneNumber(string phoneNumber, string userGuid);
        VerificationData VerifyUserPhoneNumber(string phoneNumber);
    }
}