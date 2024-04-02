using System.Diagnostics;
using Shared.Interfaces;

namespace Blazor8Test.Client.Services;

public class StateService(IHttpClientFactory httpClientFactory) : IStateService
{
    public async Task PersistSerializedStateAsync(string serializedState)
    {
        Debug.WriteLine("Persisting serialized state - wasm");
        var client = httpClientFactory.CreateClient(HttpClientNames.Backend);
        await client.GetAsync($"fluxor/set?state={serializedState}");
    }

    public async Task<string?> RetrieveSerializedStateAsync()
    {
        Debug.WriteLine("Getting serialized state - wasm");
        var client = httpClientFactory.CreateClient(HttpClientNames.Backend);
        var result = await client.GetAsync("fluxor/get");
        return result.IsSuccessStatusCode ? await result.Content.ReadAsStringAsync() : null;
    }
}