using System;
using System.Net;
using System.Net.Http;
using TechTalk.SpecFlow;

namespace CucumberTesting
{
    [Binding]
    public class GetStudentsSteps
    {
        Client client = new Client();

        [Given(@"A username and password to login to the application")]
        public void GivenAUsernameAndPasswordToLoginToTheApplication()
        {
            Console.WriteLine("");
        }
        
        [When(@"A user login using right credentials")]
        public async System.Threading.Tasks.Task WhenAUserLoginUsingRightCredentialsAsync()
        {
            await client.GeneralRequestAsync("api/student", null, "Get");
        
        }
        
        [Then(@"a list of students will be displayed")]
        public void ThenAListOfStudentsWillBeDisplayedAsync()
        {
            Console.WriteLine("");
        }
    }
}
