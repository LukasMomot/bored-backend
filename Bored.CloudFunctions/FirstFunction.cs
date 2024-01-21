using System.Collections.Generic;
using System.Net;
using Bored.CloudFunctions.Services;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Logging;

namespace Bored.CloudFunctions;

public class FirstFunction
{
    private readonly IBoredApiService _boredApiService;
    private readonly ILogger _logger;

    public FirstFunction(ILoggerFactory loggerFactory, IBoredApiService boredApiSservice)
    {
        _boredApiService = boredApiSservice;
        _logger = loggerFactory.CreateLogger<FirstFunction>();
    }

    [Function("FirstFunction")]
    public async Task<HttpResponseData> Run([HttpTrigger(AuthorizationLevel.Function, "get", "post")] HttpRequestData req,
        FunctionContext executionContext)
    {
        _logger.LogInformation("C# HTTP trigger function processed a request.");

        var response = req.CreateResponse(HttpStatusCode.OK);
        response.Headers.Add("Content-Type", "text/plain; charset=utf-8");

        var activity = await _boredApiService.GetActivity();
        await response.WriteStringAsync(activity);
        //response.WriteString("Welcome to Azure Functions!");

        return response;
        
    }
}