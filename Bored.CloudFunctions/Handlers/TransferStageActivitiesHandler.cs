using System.Globalization;
using Bored.CloudFunctions.Commands;
using Bored.Models;
using BoredBackend.Data;
using BoredBackend.Utils;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;

namespace Bored.CloudFunctions.Handlers;

public class TransferStageActivitiesHandler(BoredDbContext boredDbContext, IDistributedCache? cache) : IRequestHandler<TransferStageActivitiesCommand, int>
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

        if (cache != null)
        {
            await cache.SetStringAsync("stagingDate", DateTime.UtcNow.ToString(CultureInfo.InvariantCulture), cancellationToken);
        }
        
        return rowsAffected;
    }
}