using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Services;
public class Secrets : ISecrets
{
    private const string _apiKeyKey = "ApiKey";

    public Task<string?> GetApiKeyAsync()
    {
#pragma warning disable CS8619 // Nullability of reference types in value doesn't match target type.
        return SecureStorage.Default.GetAsync(_apiKeyKey);
#pragma warning restore CS8619 // Nullability of reference types in value doesn't match target type.
    }

    public Task SetApiKeyAsync(string? apiKey)
    {
        if (string.IsNullOrEmpty(apiKey))
        {
            SecureStorage.Default.Remove(_apiKeyKey);
            return Task.CompletedTask;
        }
        return SecureStorage.Default.SetAsync(_apiKeyKey, apiKey);
    }
}
