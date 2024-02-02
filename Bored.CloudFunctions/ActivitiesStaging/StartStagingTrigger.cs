
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.DurableTask.Client;
using Microsoft.Extensions.Logging;

namespace Bored.CloudFunctions.ActivitiesStaging;

public class StagingStartTrigger
{
    [Function(nameof(StagingStartTrigger))]
    public async Task<HttpResponseData> RunAsync(
        [HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", 
            Route = "startStaging/{count:int?}")] 
        HttpRequestData req,
        int? count,
        [DurableClient] DurableTaskClient client,
        FunctionContext executionContext)
    {
        ILogger logger = executionContext.GetLogger(nameof(StagingStartTrigger));
        count ??= 20;

        logger.LogInformation("Starting new orchestration with count = {count}", count);
        string instanceId = await client.ScheduleNewOrchestrationInstanceAsync(nameof(ActivityStagingDurable), count);
        logger.LogInformation("Created new orchestration with instance ID = {instanceId}", instanceId);

        return client.CreateCheckStatusResponse(req, instanceId);
    }
}