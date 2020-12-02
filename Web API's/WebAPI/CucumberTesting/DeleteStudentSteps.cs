using Newtonsoft.Json;
using System;
using System.Net;
using System.Net.Http;
using System.Text;
using TechTalk.SpecFlow;

namespace CucumberTesting
{
    [Binding]
    public class DeleteStudentSteps
    {
        Client client = new Client();
        [Given(@"The Student Id")]
        public void GivenTheStudentId()
        {
            Console.WriteLine("");
        }
        
        [When(@"The delete button is pressed")]
        public async System.Threading.Tasks.Task WhenTheDeleteButtonIsPressedAsync()
        {
            await client.GeneralRequestAsync("api/student/86", null, "Delete");
        }
        
        [Then(@"The record should be deleted from the database")]
        public void ThenTheRecordShouldBeDeletedFromTheDatabase()
        {
            Console.WriteLine("");
        }
    }
}
