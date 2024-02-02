using BoredBackend.Data;
using BoredBackend.Models;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;

namespace Bored.CloudFunctions.ActivitiesStaging;

public class SaveStaged(BoredDbContext dbContext)
{
    [Function(nameof(SaveStaged))]
    public async Task RunAsync([ActivityTrigger] IList<ActivityStaging> activitiesToStage, FunctionContext executionContext)
    {
        var logger = executionContext.GetLogger(nameof(SaveStaged));
        logger.LogInformation("Start saving activities. Count: {count}", activitiesToStage.Count);

        dbContext.StagedActivities.AddRange(activitiesToStage);
        await dbContext.SaveChangesAsync();
        
        logger.LogInformation("End saving activities");
    }
}