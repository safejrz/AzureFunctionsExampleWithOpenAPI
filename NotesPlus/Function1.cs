using System;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Enums;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;

namespace NotesPlus
{
    public static class GetNoteById
    {
        [FunctionName("GetNoteById")]
        [OpenApiOperation(operationId: "Run", tags: new[] { "name" })]
        [OpenApiSecurity("function_key", SecuritySchemeType.ApiKey, Name = "code", In = OpenApiSecurityLocationType.Query)]        
        [OpenApiParameter(name: "id", In = ParameterLocation.Path, Required = true, Type = typeof(Guid), Description = "The **ID** parameter")]
        [OpenApiParameter(name: "category", In = ParameterLocation.Path, Required = true, Type = typeof(string), Description = "The **Category** parameter")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(Models.File), Description = "The OK response")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", Route = "GetNoteById/{category}/{id}")] HttpRequest req, 
            [CosmosDB(databaseName: "%Database:Name%", collectionName: "%Database:Collection%", ConnectionStringSetting = "ConnectionStrings:CosmosDb", Id = "{id}", PartitionKey = "{category}")]
            Models.File file,
            ILogger log)        
        {
            //log.LogInformation("C# HTTP trigger function processed a request.");

            //string idUnparsed = req.Query["id"];

            //string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            //dynamic data = JsonConvert.DeserializeObject(requestBody);
            //var id = Guid.Parse(idUnparsed);
            ////name = name ?? data?.name;

            //string responseMessage = string.IsNullOrEmpty(name)
            //string responseMessage = string.IsNullOrEmpty(id.ToString())
            //    ? "This HTTP triggered function executed successfully. Pass a name in the query string or in the request body for a personalized response."
            //    //: $"Hello, {id}. This HTTP triggered function executed successfully.";
            //    : $"{id}.";

            return new OkObjectResult(file);
        }
    }
}

