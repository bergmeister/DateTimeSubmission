using Dto;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using System;

namespace DateTimeSubmission
{
    public static class DateTimeSubmission
    {
        [FunctionName("DateTimeSubmission")]
        public static IActionResult Run(
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = null)] HttpRequest req,
            [CosmosDB("DateTimeList", "Items", ConnectionStringSetting = "CosmosDBConnectionString", CreateIfNotExists = true)] out DateTimeDto document,
            ILogger log)
        {
            var utcNow = DateTime.UtcNow;
            document = new DateTimeDto
            {
                DateTime = utcNow
            };
            log.LogInformation($"DateTime {utcNow} submitted");

            return new OkObjectResult($"DateTime {utcNow} submitted");
        }

    }
}
