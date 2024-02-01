using Microsoft.Azure.Functions.Worker;
using Microsoft.DurableTask;

using ActivityStagingModel = BoredBackend.Models.ActivityStaging;

namespace Bored.CloudFunctions.Orchestrators;

public class ActivityStaging
{
    [Function(nameof(StageActivities))]
    public async Task<string> StageActivities([OrchestrationTrigger] TaskOrchestrationContext context)
    {
        string result = "";
        var count = context.GetInput<int>();
        var batches = (int)Math.Ceiling((double)count / 10);

        var tasks = new List<Task<List<ActivityStagingModel>>>();
        var allStagedActivities = new List<ActivityStagingModel>();
        
        for (var i = 0; i < batches; i++)
        {
            var numberOfItems = Math.Min(count - i * 10, 10);
            tasks.Add(context.CallActivityAsync<List<ActivityStagingModel>>("StageActivity", numberOfItems));
        }
        
        await Task.WhenAll(tasks);
        foreach (var task in tasks)
        {
            allStagedActivities.AddRange(task.Result);
        }

        allStagedActivities = allStagedActivities.DistinctBy(a => a.Key).ToList();

        return result;
    }
    
    
    
    //
    // [Function(nameof(StageActivities))]
    // public async Task<string> StageActivities([OrchestrationTrigger] TaskOrchestrationContext context)
    // {
    //     string result = "";
    //     var count = context.GetInput<int>();
    //     // calculate number of items pro batch when maximal number of batches is 10
    //     var batches = (int)Math.Ceiling((double)count / 10);
    //     
    //     
    //     
    //     // calculate how many items batches should be created when maximal number of items in batch is 10
    //     // var batches = (int)Math.Ceiling((double)count / 10);
    //     // for (int i = 0; i < batches; i++)
    //     // {
    //     //     var numberOfItems = count - i * 10;
    //     //     var batch = await context.CallActivityAsync<List<ActivityStaging>>(nameof(StageActivity), i);
    //     //     result += batch.Count + " ";
    //     // }
    //    
    //
    //     
    //     
    //     
    //     // result += await context.CallActivityAsync<string>(nameof(SayHello), "Tokyo") + " ";
    //     // result += await context.CallActivityAsync<string>(nameof(SayHello), "London") + " ";
    //     // result += await context.CallActivityAsync<string>(nameof(SayHello), "Seattle");
    //     return result;
    // }
}