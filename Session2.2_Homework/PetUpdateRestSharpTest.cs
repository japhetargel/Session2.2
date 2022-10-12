using Microsoft.VisualStudio.TestTools.UnitTesting;
using RestSharp;
using Session2._2_Homework;
using System.Net;

namespace Session2._2_homework
{
    [TestClass]
    public class PetUpdateRestSharpTest
    {
        private static RestClient restClient;

        private static readonly string BaseURL = "https://petstore.swagger.io/v2/";

        private static readonly string PetsEndpoint = "pet";

        private static string GetURL(string endpoint) => $"{BaseURL}{endpoint}";

        private static Uri GetURI(string endpoint) => new Uri(GetURL(endpoint));

        private readonly List<PetModel> cleanUpList = new List<PetModel>();

        [TestInitialize]
        public void TestInitialize()
        {
            restClient = new RestClient();
        }

        [TestMethod]
        public async Task AddPet()
        {

            #region Add new pet to the store
            // Create New Pet

            PetModel petData = new PetModel()
            {
                Id = 1123,
                Category = new Category()
                {
                    Id = 0,
                    Name = "Zkye"
                },
                Name = "CutePet",
                PhotoUrls = new string[]
                {
                    "pic.photo"
                },
                Status = "available",
                Tags = new Tag[]
                {
                    new Tag()
                    {
                       Id = 0,
                       Name = "Hypoallergenic, soft fur"
                    }
                }
            };

            // Send POST Request
            var postRestRequest = new RestRequest(GetURI(PetsEndpoint)).AddJsonBody(petData);
            var postRestResponse = await restClient.ExecutePostAsync(postRestRequest);

            //Verify POST request status code
            Assert.AreEqual(HttpStatusCode.OK, postRestResponse.StatusCode, "Status code is not equal to 200");
            #endregion

            #region GetUser
            var restRequest = new RestRequest(GetURI($"{PetsEndpoint}/{petData.Id}"), Method.Get);
            var restResponse = await restClient.ExecuteAsync<PetModel>(restRequest);
            #endregion

            #region Assertions
            Assert.AreEqual(HttpStatusCode.OK, restResponse.StatusCode, "Status code is not equal to 200.");
            Assert.AreEqual(petData.Name, restResponse.Data.Name, "Pet Name did not match.");
            Assert.AreEqual(petData.Category.Id, restResponse.Data.Category.Id, "Category did not match.");
            Assert.AreEqual(petData.PhotoUrls[0], restResponse.Data.PhotoUrls[0], "PhotoUrls did not match.");
            Assert.AreEqual(petData.Tags[0].Name, restResponse.Data.Tags[0].Name, "Tags did not match.");
            Assert.AreEqual(petData.Status, restResponse.Data.Status, "Status did not match.");
            #endregion
        }

        [TestCleanup]
        public async Task TestCleanUp()
        {
            foreach (var data in cleanUpList)
            {
                var restRequest = new RestRequest(GetURI($"{PetsEndpoint}/{data.Id}"));
                var restResponse = await restClient.DeleteAsync(restRequest);
            }
        }
    }
}
