using System;
using System.Configuration;
using System.Net;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace nopanic_API.Data.Repository.AWSAuthRepository
{
    public class AWSAuthRepository: IAWSAuthRepository
    {
        public async Task<object> GenerateAuthorizationHeader(string bucketName, string fileName, HttpMethod method)
        {
            string accessId = ConfigurationManager.AppSettings["AWSAccessId"];
            string secretKey = ConfigurationManager.AppSettings["AWSSecretKey"];
            string httpDate = DateTime.Now.ToString("ddd, dd MMM yyyy HH:mm:ss +0000\n");
            
            string canonicalString = "GET\n"
                                     + "\n"
                                     + "\n"
                                     + "\n"
                                     + "x-amz-date:" + httpDate + "\n"
                                     + "/" + bucketName + "/file_name.png";

            
            // Encode the canonical string
            Encoding ae = new UTF8Encoding();
            // Create a hashing object
            HMACSHA1 signature = new HMACSHA1();
            
            // SecretKey is the hash key
            signature.Key = ae.GetBytes(secretKey);
            byte[] bytes = ae.GetBytes(canonicalString);
            byte[] moreBytes = signature.ComputeHash(bytes);
            
            // convert the hash byte array into a base64 encoding
            string encodedCanonical = Convert.ToBase64String(moreBytes);
            
            HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create("http://" + bucketName +".s3.amazonaws.com/file_name.png");
            request.Headers.Add("x-amz-date", httpDate);
            request.Headers.Add("Authorization", "AWS " + accessId + ":" + encodedCanonical);
            request.Method = "GET";
            
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            return response;
        }
    }
}