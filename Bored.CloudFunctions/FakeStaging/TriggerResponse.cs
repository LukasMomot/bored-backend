using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;

namespace Bored.CloudFunctions.FakeStaging;

public class TriggerResponse
{
    public HttpResponseData Response { get; set; }
    
    [ServiceBusOutput("start-fake-staging", Connection = "ServiceBus")]
    public Models.Messages.StartFakeStaging Message { get; set; }
}