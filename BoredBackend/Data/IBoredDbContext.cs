using BoredBackend.Models;
using Microsoft.EntityFrameworkCore;

namespace BoredBackend.Data;

public interface IBoredDbContext
{
    DbSet<Activity> Activities { get; }
    DbSet<Offer> Offers { get; }
}