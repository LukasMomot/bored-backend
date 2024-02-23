using System.Net;
using BoredBackend.Data;
using BoredBackend.Models;
using BoredBackend.Utils;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Bored.CloudFunctions.ActivitiesStaging;

public class TransferStageActivities(ILoggerFactory loggerFactory, BoredDbContext boredDbContext)
{
    private readonly ILogger _logger = loggerFactory.CreateLogger<TransferStageActivities>();

    [Function(nameof(TransferStageActivities))]
    public async Task<HttpResponseData> RunAsync([HttpTrigger(AuthorizationLevel.Function, "post")] HttpRequestData req,
        FunctionContext executionContext)
    {
        _logger.LogInformation("Transfer Stage Activities HTTP trigger function processed a request");
        var stagingActivities = await boredDbContext.StagedActivities.ToListAsync();
        var activities = stagingActivities.Select(a => new Activity
        {
            Type = a.Type,
            Participants = a.Participants,
            Price = a.Price,
            Link = a.Link,
            Name = a.Activity,
            Accessibility = a.Accessibility,
            ExternalKey = a.Key,
        }).ToList();
        
        foreach (var activity in activities)  
        {
            boredDbContext.Activities.AddIfNotExists(activity, a => a.ExternalKey == activity.ExternalKey);
        }
        
        var rowsAffected = await boredDbContext.SaveChangesAsync();
        
        _logger.LogInformation("All activities staged successfully: Rows affected: {RowsAffected}", rowsAffected);

        var response = req.CreateResponse(HttpStatusCode.OK);
        return response;
        
    }
}