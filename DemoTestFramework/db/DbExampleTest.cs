using System;
using System.Collections.Generic;
using DemoTestFramework.db;
using DemoTestFramework.models;
using Newtonsoft.Json;
using Npgsql;
using NUnit.Framework;
using RestSharp;

namespace DemoTestFramework
{
    public class DbExampleTest
    {
        private string Endpoint;
        
        [SetUp]
        public void BeforeTest()
        {
            Endpoint = "https://swapi.dev/api/planets/";
        }
        
        [Test]
        [TestCase(1)]
        public void GetPlanetsByRestSharpTesting(int page)
        {
            var client = new RestClient();
            RestRequest request = new RestRequest(Endpoint, Method.Get);
            request.AddParameter("page", page);
            RestResponse response = client.Execute(request);
            ResponsePlanet result = JsonConvert.DeserializeObject<ResponsePlanet>(response.Content);

            using (DemoContextDb contextDb = new DemoContextDb())
            {
                for (int i = 0; i < result.Results.Length; i++)
                {
                    result.Results[i].Id = i + 1;
                    contextDb.Planets.AddRange(result.Results[i]);
                }
                contextDb.SaveChanges();
            }

        }

        [Test]
        public void CreateDataInDb()
        {
            Person ignat = new Person() { Id=1, FirstName = "Игнат", SecondName = "Васильевич", MiddleName = "Зуев"};
            Animal cat = new Animal() {Name = "Федор", Age = "8"};
            
            Animal cat2 = new Animal() {Name = "Федор2", Age = "2", Person = ignat, PersonId = 1}; Car car = new Car() {Model = "bmw", Year = 2020, Person = ignat, PersonId = 1};
            
            using (DemoContextDb contextDb = new DemoContextDb())
            {
                contextDb.Persons.AddRange(ignat);
                contextDb.Animals.AddRange(cat);
                contextDb.Animals.AddRange(cat2);
                contextDb.Cars.AddRange(car);
                
                contextDb.SaveChanges();
            }
        }

        // Ado.net
        [Test]
        public void AdoExample()
        {
            string connectionString = "Host=localhost;Port=5432;Database=demo;Username=postgres;Password=admin";
            using (var connect = new NpgsqlConnection(connectionString))
            {
                connect.Open();
                string sql =
                    "SELECT id, name, align, eye, hair, gender, appearances, year, universe FROM public.superheroes";
                var cmd = new NpgsqlCommand(sql, connect);
                var result = cmd.ExecuteReader();

                Dictionary<int, string> superHeroes = new Dictionary<int, string>();

                while (result.Read())
                {
                    superHeroes.Add((int)result["id"], (string)result["name"]);
                }

                Console.WriteLine(superHeroes);
                connect.Close();
            }
        }
    }
}