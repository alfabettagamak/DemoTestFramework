using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using DemoTestFramework.models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using NUnit.Framework;

namespace DemoTestFramework
{

    public class Tests : TestBase
    {

        private const string Endpoint = "/planets";
        private HttpClient _client;
        
        [OneTimeSetUp]
        public void BeforeClass()
        {
            // один раз перед всеми тестами этого класса
            _client = new HttpClient();
        }
        
        [SetUp]
        public void Setup()
        {
            // перед каждым тестом
        }

        // приведение ответа от апи к JSON
        [Test]
        public async Task GetPlanetsTesting()
        {
            var result = await _client.GetAsync(Host + Endpoint);
            string content = await result.Content.ReadAsStringAsync();
            JObject json = JObject.Parse(content);
            
            Assert.AreEqual(HttpStatusCode.OK, result.StatusCode, "Код ответа от апи не соответсвует ожидаемому");
            Assert.AreEqual("60", json["count"].ToString());
            Assert.NotNull(json["next"]);
        }
        
        
        // приведение ответа от апи к классу
        [Test]
        public async Task GetPlanetsByObjectTesting()
        {
            var result = await _client.GetAsync(Host + Endpoint);
            string content = await result.Content.ReadAsStringAsync();
            ResponsePlanet response = JsonConvert.DeserializeObject<ResponsePlanet>(content);
  
            Assert.AreEqual(HttpStatusCode.OK, result.StatusCode, "Код ответа от апи не соответсвует ожидаемому");
        }
    }
}