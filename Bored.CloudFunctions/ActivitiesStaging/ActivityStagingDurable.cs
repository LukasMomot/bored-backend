using Microsoft.Azure.Functions.Worker;
using Microsoft.DurableTask;
using Microsoft.Extensions.Logging;
using ActivityStagingModel = BoredBackend.Models.ActivityStaging;

namespace Bored.CloudFunctions.ActivitiesStaging;

public class ActivityStagingDurable
{
    [Function(nameof(ActivityStagingDurable))]
    public async Task<string> RunAsync([OrchestrationTrigger] TaskOrchestrationContext context)
    {
        var count = context.GetInput<int>();
        var batches = (int)Math.Ceiling((double)count / 10);
        var logger = context.CreateReplaySafeLogger(typeof(ActivityStagingDurable));

        var tasks = new List<Task<List<ActivityStagingModel>>>();
        var allStagedActivities = new List<ActivityStagingModel>();
        
        for (var i = 0; i < batches; i++)
        {
            var numberOfItems = Math.Min(count - i * 10, 10);
            tasks.Add(context.CallActivityAsync<List<ActivityStagingModel>>(nameof(GetActivities), numberOfItems));
        }
        
        await Task.WhenAll(tasks);
        foreach (var task in tasks)
        {
            allStagedActivities.AddRange(task.Result);
        }
        allStagedActivities = allStagedActivities.DistinctBy(a => a.Key).ToList();
        
        var activityNames = string.Join(", ", allStagedActivities.Select(a => a.Activity));
        
        logger.LogInformation("Stage activitycount: {activityCount}", allStagedActivities.Count);
        logger.LogInformation("Staged activities: {activityNames}", activityNames);
        
        return activityNames;
    }
}