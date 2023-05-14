using NUnit.Framework;
using System.Net;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;

namespace WebApiTests.DataModelTests
{
    public class DataModelTests : DataModelTestsBase
    {
        [Test]
        public async Task GetAllDataModels()
        {
            List<string> expectedModelsContent = new List<string>() { "item1", "item2", "item3" };

            HttpResponseMessage response = await httpClient.GetAsync("/api/DataModel");
            List<string> responseContent = await response.Content.ReadFromJsonAsync<List<string>>();

            TestingUtilities.IsResponseStatus(HttpStatusCode.OK, response.StatusCode);
            Assert.AreEqual(expectedModelsContent, responseContent);
        }

        [TestCase(0, "item1")]
        [TestCase(1, "item2")]
        [TestCase(2, "item3")]
        public async Task GetDataModelById(int testId, string expectedContent)
        {
            HttpResponseMessage response = await httpClient.GetAsync($"/api/DataModel/{testId}");
            string responseContent = await response.Content.ReadAsStringAsync();

            TestingUtilities.IsResponseStatus(HttpStatusCode.OK, response.StatusCode);
            Assert.AreEqual(expectedContent, responseContent);
        }

        [Test]
        public async Task PostDataModel()
        {
            string testContent = "item0";
            StringContent testStringContent = new StringContent(
                JsonSerializer.Serialize(testContent),
                Encoding.UTF8,
                "application/json"
                );

            HttpResponseMessage response = await httpClient.PostAsync("/api/DataModel", testStringContent);
            string responseContent = await response.Content.ReadAsStringAsync();

            TestingUtilities.IsResponseStatus(HttpStatusCode.Created, response.StatusCode);
            Assert.AreEqual(testContent, responseContent);

            response = await httpClient.GetAsync("/api/DataModel");
            List<string> responseContentList = await response.Content.ReadFromJsonAsync<List<string>>();

            TestingUtilities.IsResponseStatus(HttpStatusCode.OK, response.StatusCode);
            Assert.AreEqual(testContent, responseContentList.Last());
        }

        [Test]
        public async Task PutDataModel()
        {
            string testContent = "test";
            int testId = 0;
            StringContent testStringContent = new StringContent(
                JsonSerializer.Serialize(testContent),
                Encoding.UTF8,
                "application/json"
                );

            HttpResponseMessage response = await httpClient.PutAsync($"/api/DataModel/{testId}", testStringContent);

            TestingUtilities.IsResponseStatus(HttpStatusCode.NoContent, response.StatusCode);

            GetDataModelById(testId, testContent);
        }

        [Test]
        public async Task DeleteDataModel()
        {
            int testId = 0;

            HttpResponseMessage response = await httpClient.GetAsync($"/api/DataModel/{testId}");
            string itemToDelete = await response.Content.ReadAsStringAsync();
            response = await httpClient.DeleteAsync($"/api/DataModel/{testId}");

            TestingUtilities.IsResponseStatus(HttpStatusCode.NoContent, response.StatusCode);

            response = await httpClient.GetAsync($"/api/DataModel/{testId}");
            string actualItem = await response.Content.ReadAsStringAsync();

            Assert.AreNotEqual(itemToDelete, actualItem);
        }
    }
}