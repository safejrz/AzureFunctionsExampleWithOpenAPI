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
            return new OkObjectResult(file);
        }
    }
}

