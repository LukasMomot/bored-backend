using System.Collections.Generic;
using System.Net;
using Bored.Models;
using BoredBackend.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;

namespace Bored.CloudFunctions;

public class GetStagedActivities(ILoggerFactory loggerFactory, BoredDbContext dbContext)
{
    private readonly ILogger _logger = loggerFactory.CreateLogger<GetStagedActivities>();

    [Function(nameof(GetStagedActivities))]
    [OpenApiOperation(operationId: "GetStagedActivities", Summary = "Get staged activities", Description = "This endpoint returns staged activities.")]
    [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(List<ActivityStaging>), Description = "This returns the response")]
    [OpenApiParameter(name: "take", In = ParameterLocation.Query, Required = false, Type = typeof(int), Description = "Number of activities to take")]
    public IActionResult Run(
        [HttpTrigger(AuthorizationLevel.Function, "get")] HttpRequest req,
        FunctionContext executionContext)
    {
        _logger.LogInformation("C# HTTP trigger function processed a request");

        // Check if take available
        var activities = dbContext.StagedActivities.AsNoTracking();
        req.Query.TryGetValue("take", out var takeQueryParam);
        if(int.TryParse(takeQueryParam, out var take))
        {
            activities = activities.Take(take);
        }
        
        var activitiesResult = activities.ToList();
        
        _logger.LogInformation("Staged activities count: {count}", activitiesResult.Count);
        
        return new OkObjectResult(activitiesResult);
        
    }
}