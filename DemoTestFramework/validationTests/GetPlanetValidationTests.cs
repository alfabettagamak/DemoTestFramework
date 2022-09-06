using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Schema;
using NUnit.Framework;

namespace DemoTestFramework.validationTests
{
    public class GetPlanetValidationTests 
    {
        private const string Host = "https://swapi.dev/api";
        private const string Endpoint = "/planets";
        
        [Test]
        public async Task GetPlanetsValidTesting()
        {
            HttpClient client = new HttpClient();
            var response = await client.GetAsync(Host + Endpoint);
            string content = await response.Content.ReadAsStringAsync();
            JObject json = JObject.Parse(content);
            
            JSchema jSchema = JSchema.Parse(File.ReadAllText("D://testing/Maxima_Aqa/UnitExample/1/DemoTestFramework/DemoTestFramework/validationTests/GetPlanets.Positive.json"));
            bool result = json.IsValid(jSchema, out IList<string> messages);
            Assert.IsTrue(result, string.Join(" ,", messages.ToArray()));
        }
        
    }
}