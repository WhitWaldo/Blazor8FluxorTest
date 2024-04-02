using Fluxor.Persistence;
using Shared.Interfaces;

namespace Shared.Persistence;

public sealed class ServerSideStoreManager(IStateService stateSvc) : IPersistenceManager
{
    /// <summary>
    /// Persists the store to a persisted state.
    /// </summary>
    /// <param name="serializedStore">The serialized store data being persisted.</param>
    public async Task PersistStoreToStateAsync(string serializedStore)
    {
        await stateSvc.PersistSerializedStateAsync(serializedStore);
    }

    /// <summary>
    /// Rehydrates the store from a persisted state.
    /// </summary>
    public async Task<string?> RehydrateStoreFromStateAsync()
    {
        return await stateSvc.RetrieveSerializedStateAsync();
    }

    /// <summary>
    /// Clears the store from the persisted state.
    /// </summary>
    public Task ClearStoreFromStateAsync()
    {
        // no-op
        return Task.CompletedTask;
    }
}