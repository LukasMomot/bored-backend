using System.Reflection;
using Bored.Models;
using Microsoft.EntityFrameworkCore;

namespace BoredBackend.Data;

public class BoredDbContext(DbContextOptions<BoredDbContext> options) :DbContext(options), IBoredDbContext
{
    public DbSet<Activity> Activities { get; set; } = null!;
    public DbSet<Offer> Offers { get; set; } = null!;
    public DbSet<ActivityStaging> StagedActivities { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        base.OnModelCreating(modelBuilder);
    }   
}