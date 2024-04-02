using Shared.Interfaces;

namespace Blazor8Test.Services;

public sealed class InMemoryStateService : IStateService
{
    private string? _persistedValue = null;

    public Task PersistSerializedStateAsync(string serializedState)
    {
        _persistedValue = serializedState;
        return Task.CompletedTask;
    }

    public Task<string?> RetrieveSerializedStateAsync()
    {
        return Task.FromResult(_persistedValue);
    }
}