namespace Data.Services;

public interface ISecrets
{
    Task<string?> GetApiKeyAsync();
    Task SetApiKeyAsync(string? apiKey);
}