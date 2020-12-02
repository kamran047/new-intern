using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace CucumberTesting
{
    class Client
    {
        HttpResponseMessage response;
        private static readonly HttpClient client = new HttpClient();

        public async Task GeneralRequestAsync(string controllerUrl, HttpContent data, string methodType) {
            var baseUrl= "http://localhost:65276/";

            if (methodType == "Post")
            {
                response = await client.PostAsync(baseUrl + controllerUrl, data);
            }
            else if (methodType == "Put")
            {
                response = await client.PutAsync(baseUrl + controllerUrl, data);
            }
            else if (methodType == "Delete")
            {
                response = await client.DeleteAsync(baseUrl + controllerUrl);
            }

            else if (methodType == "Get")
            {
                response = await client.GetAsync(baseUrl + controllerUrl);
            }

            if (response.StatusCode != HttpStatusCode.OK)
                {
                    var message = string.Format("Faile: Received HTTP {0}", response.StatusCode);
                    throw new ApplicationException(message);
                }

            var responseString = await response.Content.ReadAsStringAsync();
            }          
    }
}
