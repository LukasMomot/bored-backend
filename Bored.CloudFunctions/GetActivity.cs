using System.Collections.Generic;
using System.Net;
using Bored.Services.ExternalClients;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
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