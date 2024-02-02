using System.Collections.Generic;
using System.Net;
using BoredBackend.Data;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Bored.CloudFunctions;

public class GetStagedActivities(ILoggerFactory loggerFactory, BoredDbContext dbContext)
{
    private readonly ILogger _logger = loggerFactory.CreateLogger<GetStagedActivities>();

    [Function(nameof(GetStagedActivities))]
    public async Task<HttpResponseData> Run(
        [HttpTrigger(AuthorizationLevel.Function, "get")] HttpRequestData req,
        FunctionContext executionContext)
    {
        _logger.LogInformation("C# HTTP trigger function processed a request");

        // Check if take available
        var activities = dbContext.StagedActivities.AsNoTracking();
        
        var takeQueryParam = req.Query.Get("take");
        if(int.TryParse(takeQueryParam, out var take))
        {
            activities = activities.Take(take);
        }
        
        var activitiesResult = activities.ToList();
        
        _logger.LogInformation("Staged activities count: {count}", activitiesResult.Count);
        
        var response = req.CreateResponse(HttpStatusCode.OK);
        await response.WriteAsJsonAsync(activities);
        
        return response;
        
    }
}