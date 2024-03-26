using System;
using System.Text.Json;
using Azure.Messaging.ServiceBus;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using Models = Bored.Models.Messages;

namespace Bored.CloudFunctions;

public class StartFakeStaging
{
    private readonly ILogger<StartFakeStaging> _logger;

    public StartFakeStaging(ILogger<StartFakeStaging> logger)
    {
        _logger = logger;
    }

    [Function(nameof(StartFakeStaging))]
    public void Run([ServiceBusTrigger("start-fake-staging", Connection = "ServiceBus")] ServiceBusReceivedMessage message)
    {
        _logger.LogInformation("Message ID: {id}", message.MessageId);
    
        // convert body to json object StartFakeStaging
        var json = message.Body.ToString();
        var msg = JsonSerializer.Deserialize<Models.Messages.StartFakeStaging>(json);
        _logger.LogInformation("Message ProcessId: {processId}", msg.ProcessId);
        
        _logger.LogInformation("Message Body: {body}", message.Body);
        _logger.LogInformation("Message Content-Type: {contentType}", message.ContentType);
        
    }
}