using BoredBackend.Models;
using Microsoft.EntityFrameworkCore;

namespace BoredBackend.Data;

public class BoredDbContext(DbContextOptions<BoredDbContext> options) : DbContext(options)
{
    public DbSet<Activity> Activities { get; set; } = null!;
}