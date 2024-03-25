
using Bored.Models;
using BoredBackend.Data;
using BoredBackend.Messages;
using MassTransit;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BoredBackend.Endpoints;

public static class ActivityEndpoints
{
    public static void MapActivityEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("api/activities");
        
        group.MapGet("", GetAllActivities);
        group.MapGet("{id:int}", GetActivityById);
        group.MapPost("fakeStage", FakeStageActivity);
    }

    private static async Task<IResult> FakeStageActivity(ISendEndpointProvider bus)
    {
        var endpoint = await bus.GetSendEndpoint(new Uri("queue:start-fake-staging"));
        await endpoint.Send<StartFakeStaging>(new StartFakeStaging { ProcessId = 123, StartImmediately = true });
        return Results.Ok();
    }

    private static async Task<Ok<List<Activity>>> GetAllActivities(BoredDbContext dbContext)
    {
        var activities = await dbContext.Activities.
            Include(o => o.Offers)
            .ToListAsync();
        
        var res = TypedResults.Ok(activities);

        return res;
    }
    
    private static async Task<Ok<Activity>> GetActivityById(BoredDbContext dbContext, int id)
    {
        var activity = await dbContext.Activities.FindAsync(id);
        var res = TypedResults.Ok(activity);

        return res;
    }
}