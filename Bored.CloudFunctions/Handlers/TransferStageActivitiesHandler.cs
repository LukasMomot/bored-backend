using Bored.CloudFunctions.Commands;
using BoredBackend.Data;
using BoredBackend.Models;
using BoredBackend.Utils;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Bored.CloudFunctions.Handlers;

public class TransferStageActivitiesHandler(BoredDbContext boredDbContext) : IRequestHandler<TransferStageActivitiesCommand, int>
{
    public async Task<int> Handle(TransferStageActivitiesCommand request, CancellationToken cancellationToken)
    {
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
        return rowsAffected;
    }
}