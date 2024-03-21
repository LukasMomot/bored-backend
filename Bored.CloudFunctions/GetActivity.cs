using System.Collections.Generic;
using System.Net;
using Bored.Models;
using Bored.Services.ExternalClients;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;
using Microsoft.Extensions.Logging;

namespace Bored.CloudFunctions;

public class GetActivity
{
    private readonly IBoredApiService _boredApiService;
    private readonly ILogger _logger;

    public GetActivity(ILoggerFactory loggerFactory, IBoredApiService boredApiSservice)
    {
        _boredApiService = boredApiSservice;
        _logger = loggerFactory.CreateLogger<GetActivity>();
    }

    [Function(nameof(GetActivity))]
    [OpenApiOperation(operationId: "GetActivity", Summary = "Get a random activity", Description = "This endpoint returns a random activity.")]
    [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(ActivityStaging), Description = "This returns the response")]
    [OpenApiResponseWithBody(statusCode: HttpStatusCode.BadRequest, contentType: "application/json", bodyType: typeof(string), Description = "This return error message")]
    public async Task<HttpResponseData> Run(
        [HttpTrigger(AuthorizationLevel.Function, "get")] 
        HttpRequestData req,
        FunctionContext executionContext)
    {
        _logger.LogInformation("C# HTTP trigger function processed a request");

        // var maxCount = 2;
        // var batchSize = 5;
        // var batchCount = (int)Math.Ceiling((double)maxCount / batchSize);
        
        var response = req.CreateResponse(HttpStatusCode.OK);
        
        var activity = await _boredApiService.GetActivity();
        await response.WriteAsJsonAsync(activity);
        //response.Headers.Add("Content-Type", "text/plain; charset=utf-8");
        //response.WriteString("Welcome to Azure Functions!");

        return response;
        
    }
}