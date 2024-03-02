using System.Collections.Generic;
using System.Globalization;
using System.Net;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Logging;

namespace Bored.CloudFunctions.ActivitiesStaging;

public class SaveStagingDate(ILoggerFactory loggerFactory, IDistributedCache cache)
{
    private readonly ILogger _logger = loggerFactory.CreateLogger<SaveStagingDate>();

    [Function("SaveStagingDate")]
    public HttpResponseData Run([HttpTrigger(AuthorizationLevel.Function, "post")] HttpRequestData req,
        FunctionContext executionContext)
    {
        _logger.LogInformation("C# HTTP trigger function processed a request.");

        var response = req.CreateResponse(HttpStatusCode.OK);
        response.Headers.Add("Content-Type", "text/plain; charset=utf-8");
        const string key = "stagingDate";
        var value = DateTime.UtcNow.ToString(CultureInfo.InvariantCulture);
        cache.SetString(key, value);
        var cachedValue = cache.GetString(key);
        
        response.WriteString($"Staging date set to: {cachedValue}");

        return response;
        
    }
}