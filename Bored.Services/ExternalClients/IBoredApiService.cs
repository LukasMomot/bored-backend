using BoredBackend.Models;

namespace Bored.Services.ExternalClients;

public interface IBoredApiService
{
    Task<ActivityStaging?> GetActivity();
}