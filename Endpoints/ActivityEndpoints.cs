using BoredBackend.Data;
using BoredBackend.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;

namespace BoredBackend.Endpoints;

public static class ActivityEndpoints
{
    public static void MapActivityEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("api/activities");
        
        group.MapGet("", GetAllActivities);
    }

    private static async Task<Ok<List<Activity>>> GetAllActivities(BoredDbContext dbContext)
    {
        var activities = await dbContext.Activities.ToListAsync();
        var res = TypedResults.Ok(activities);

        return res;
    }
}