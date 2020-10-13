using System.Net.Http;
using System.Threading.Tasks;

namespace nopanic_API.Data.Repository.AWSAuthRepository
{
    public interface IAWSAuthRepository
    {
        public Task<object>  GenerateAuthorizationHeader(string bucketName, string fileName, HttpMethod method);
    }
}