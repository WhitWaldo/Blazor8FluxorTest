using Blazored.SessionStorage;
using Fluxor.Persistence;

namespace Shared.Persistence;

public sealed class SessionStorePersistenceManager(ISessionStorageService sessionStorageSvc) : IPersistenceManager
{
    private const string KeyValue = "__flx";

    /// <summary>
    /// Persists the store to a persisted state.
    /// </summary>
    /// <param name="serializedStore">The serialized store data being persisted.</param>
    public async Task PersistStoreToStateAsync(string serializedStore)
    {
        await sessionStorageSvc.SetItemAsStringAsync(KeyValue, serializedStore);
    }

    /// <summary>
    /// Rehydrates the store from a persisted state.
    /// </summary>
    public async Task<string?> RehydrateStoreFromStateAsync()
    {
        return await sessionStorageSvc.GetItemAsStringAsync(KeyValue);
    }

    /// <summary>
    /// Clears the store from the persisted state.
    /// </summary>
    public async Task ClearStoreFromStateAsync()
    {
        await sessionStorageSvc.RemoveItemAsync(KeyValue);
    }
}