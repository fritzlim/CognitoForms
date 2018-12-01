using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using Amazon.Runtime;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace SaltyDog.CognitoForms.Droid
{
   public class AndroidClientFactory : IHttpClientFactory
    {
        public HttpClient CreateHttpClient(IClientConfig clientConfig)
        {
            return new HttpClient();
        }

    }
}