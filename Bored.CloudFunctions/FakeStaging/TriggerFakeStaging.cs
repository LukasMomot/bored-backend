using System.Collections.Generic;
using System.Net;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Logging;

namespace Bored.CloudFunctions.FakeStaging;

public class TriggerFakeStaging
{
    private readonly ILogger _logger;

    public TriggerFakeStaging(ILoggerFactory loggerFactory)
    {
        _logger = loggerFactory.CreateLogger<TriggerFakeStaging>();
    }

    [Function("TriggerFakeStaging")]
    public async Task<TriggerResponse> Run([HttpTrigger(AuthorizationLevel.Function, "get", "post")] HttpRequestData req,
        FunctionContext executionContext)
    {
        _logger.LogInformation("C# HTTP trigger function processed a request.");

        var response = req.CreateResponse(HttpStatusCode.OK);
        response.Headers.Add("Content-Type", "text/plain; charset=utf-8");
        await response.WriteStringAsync("Start Fake Staging triggered.");

        return new TriggerResponse
        {
            Response = response,
            Message = new Models.Messages.StartFakeStaging { ProcessId = 567, StartImmediately = true }
        };
    }
}