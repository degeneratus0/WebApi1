using NUnit.Framework;
using System.Net;
using System.Text;
using System.Text.Json;

namespace WebApiTests.DataModelTests
{
    public class DataModelNegativeTests : DataModelTestsBase
    {
        [Test]
        public async Task GetDataModelByIncorrectId()
        {
            int testId = -1;

            HttpResponseMessage response = await httpClient.GetAsync($"/api/DataModel/{testId}");

            TestingUtilities.IsResponseStatus(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Test]
        public async Task PostEmptyDataModel()
        {
            StringContent emptyStringContent = TestingUtilities.CreateDefaultStringContent("");

            HttpResponseMessage response = await httpClient.PostAsync("/api/DataModel", emptyStringContent);

            TestingUtilities.IsResponseStatus(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Test]
        public async Task PutDataModelByIncorrectId()
        {
            string testContent = "test";
            int testId = -1;
            StringContent testStringContent = new StringContent(
                JsonSerializer.Serialize(testContent),
                Encoding.UTF8,
                "application/json"
                );

            HttpResponseMessage response = await httpClient.PutAsync($"/api/DataModel/{testId}", testStringContent);

            TestingUtilities.IsResponseStatus(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Test]
        public async Task PutEmptyDataModel()
        {
            int testId = 0;
            StringContent testStringContent = new StringContent("", Encoding.UTF8, "application/json");

            HttpResponseMessage response = await httpClient.PutAsync($"/api/DataModel/{testId}", testStringContent);

            TestingUtilities.IsResponseStatus(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Test]
        public async Task DeleteDataModelByIncorrectId()
        {
            int testId = -1;

            HttpResponseMessage response = await httpClient.DeleteAsync($"/api/DataModel/{testId}");

            TestingUtilities.IsResponseStatus(HttpStatusCode.BadRequest, response.StatusCode);
        }
    }
}