using System.Net.Http;
using Amazon.Runtime;

namespace SaltyDog.CognitoForms.iOS
{
    public class IOSClientFactory : IHttpClientFactory
    {
        public HttpClient CreateHttpClient(IClientConfig clientConfig)
        {
            return new HttpClient();
        }

    }
}