namespace Bored.CloudFunctions.Services;

public interface IBoredApiService
{
    Task<string> GetActivity();
}