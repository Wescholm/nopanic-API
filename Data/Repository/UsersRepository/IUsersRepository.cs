using nopanic_API.Models;

namespace nopanic_API.Data.Repository.UsersRepository
{
    public interface IUsersRepository
    {
        bool AddUserPhoneNumber(string phoneNumber, string userGuid);
        VerificationData VerifyUserPhoneNumber(string phoneNumber);
    }
}