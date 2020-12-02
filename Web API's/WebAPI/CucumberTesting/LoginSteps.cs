using System;
using System.Collections.Generic;
using System.Net.Http;
using TechTalk.SpecFlow;

namespace CucumberTesting
{
    [Binding]
    public class LoginSteps
    {
        Client client = new Client();

        [Given(@"The credentials of user")]
        public void GivenTheCredentialsOfUser()
        {
            Console.WriteLine("");
        }
        
        [When(@"the login button is pressed")]
        public async System.Threading.Tasks.Task WhenTheLoginButtonIsPressedAsync()
        {
            var values = new Dictionary<string, string>
        {
         { "grant_type", "password" },
         { "username", "Kamran" },
         { "password", "Kamran" }
        };
            var content = new FormUrlEncodedContent(values);
            await client.GeneralRequestAsync("Login", content, "Post");
        }
        
        [Then(@"the user should be logged in")]
        public void ThenTheUserShouldBeLoggedIn()
        {
            Console.WriteLine("");
        }
    }
}
