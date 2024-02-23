using System.Net;
using Bored.CloudFunctions.Commands;
using BoredBackend.Data;
using BoredBackend.Models;
using BoredBackend.Utils;
using MediatR;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Bored.CloudFunctions.ActivitiesStaging;

public class TransferStageActivities(ILoggerFactory loggerFactory, IMediator mediator)
{
    private readonly ILogger _logger = loggerFactory.CreateLogger<TransferStageActivities>();

    [Function(nameof(TransferStageActivities))]
    public async Task<HttpResponseData> RunAsync([HttpTrigger(AuthorizationLevel.Function, "post")] HttpRequestData req,
        FunctionContext executionContext)
    {
        _logger.LogInformation("Transfer Stage Activities HTTP trigger function processed a request");
        
        var rowsAffected = await mediator.Send(new TransferStageActivitiesCommand());
        
        _logger.LogInformation("All activities staged successfully: Rows affected: {RowsAffected}", rowsAffected);

        var response = req.CreateResponse(HttpStatusCode.OK);
        return response;
    }
}