using Bored.Models;
using Bored.Services.ExternalClients;
using Microsoft.Azure.Functions.Worker;
using Microsoft.DurableTask;
using Microsoft.Extensions.Logging;

namespace Bored.CloudFunctions.ActivitiesStaging;

public class GetActivities(IBoredApiService boredApiService)
{
    [Function(nameof(GetActivities))]
    public async Task<IList<ActivityStaging>> RunAsync([ActivityTrigger] int count, FunctionContext executionContext)
    {
        var logger = executionContext.GetLogger(nameof(GetActivities));
        logger.LogInformation("Start getting activities. Count: {count}", count);
        var activities = new List<ActivityStaging>();

        for (var i = 0; i < count; i++)
        {
            var activity = await boredApiService.GetActivity();
            activities.Add(activity);
        }

        logger.LogInformation("End getting activities. Count successful: {count}", activities.Count);
        return activities;
    }

}