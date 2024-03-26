
using System.Text.Json;
using Azure.Messaging.ServiceBus;
using Bored.Models;
using Bored.Models.Messages;
using BoredBackend.Data;
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

    private static async Task FakeStageActivity([FromServices] IConfiguration configuration)
    {
        var connectionString = configuration["ServiceBus"];
        await using var client = new ServiceBusClient(connectionString);

        await using ServiceBusSender sender = client.CreateSender("start-fake-staging");
        
        var msg = new StartFakeStaging{ ProcessId = 1, StartImmediately = true };
        var json = JsonSerializer.Serialize(msg);
        var sbMsg = new ServiceBusMessage(json);

        await sender.SendMessageAsync(sbMsg);
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