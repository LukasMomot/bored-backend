using BoredBackend.Models;

namespace Bored.CloudFunctions.Services;

public interface IBoredApiService
{
    Task<ActivityStaging?> GetActivity();
}