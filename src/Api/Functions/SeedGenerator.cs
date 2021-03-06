using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Blazoring.PWA.API.Services;
using Blazoring.PWA.Shared.Models;

namespace Blazoring.PWA.API.Functions
{
    public class SeedGenerator
    {
        private readonly ISeedGeneratorService seedGenerator;
        private readonly ILogger<SeedGenerator> logger;

        public SeedGenerator(ISeedGeneratorService seedGenerator, ILogger<SeedGenerator> logger)
        {
            this.seedGenerator = seedGenerator;
            this.logger = logger;
        }

        [FunctionName("SeedUsers")]
        public IActionResult SeedUsers(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = null)] HttpRequest req)
        {
            int count = req.Query.ContainsKey("Count") ? int.Parse(req.Query["Count"]) : 10;
            var users = seedGenerator.GetUsers(count);
            return new OkObjectResult(users);
        }

        [FunctionName("SeedCountries")]
        public IActionResult SeedCountries(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = null)] HttpRequest req)
        {
            var countries = seedGenerator.GetCountries();
            return new OkObjectResult(countries);
        }

        [FunctionName("InsertUser")]
        public async Task<IActionResult> InsertUser(
           [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = null)] HttpRequest req)
        {
            var responseBody = await req.ReadAsStringAsync();
            User user = JsonConvert.DeserializeObject<User>(responseBody);
            seedGenerator.SetUser(user);
            return new OkResult();
        }

        [FunctionName("UpdateUser")]
        public async Task<IActionResult> UpdateUser(
           [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = null)] HttpRequest req)
        {
            var responseBody = await req.ReadAsStringAsync();
            User user = JsonConvert.DeserializeObject<User>(responseBody);
            seedGenerator.UpdateUser(user);
            return new OkResult();
        }
    }
}
