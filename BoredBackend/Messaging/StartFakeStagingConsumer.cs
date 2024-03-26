using Bored.Models.Messages;
using MassTransit;
using Microsoft.Extensions.Logging;

namespace BoredBackend.Messaging;

public class StartFakeStagingConsumer : IConsumer<StartFakeStaging>
{
    private readonly ILogger<StartFakeStagingConsumer> _logger;

    public StartFakeStagingConsumer(ILogger<StartFakeStagingConsumer> logger)
    {
        _logger = logger;
    }
    
    public Task Consume(ConsumeContext<StartFakeStaging> context)
    {
        _logger.LogInformation("StartFakeStaging message consumed with ProcessId = {ProcessId}", context.Message.ProcessId);
        return Task.CompletedTask;
    }
}