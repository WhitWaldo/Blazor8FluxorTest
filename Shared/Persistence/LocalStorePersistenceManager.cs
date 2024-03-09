using Blazored.LocalStorage;
using Fluxor.Persistence;

namespace Shared.Persistence;

public sealed class LocalStorePersistenceManager(ILocalStorageService localStorageSvc) : IPersistenceManager
{
    /// <summary>
    /// Persists the store to a persisted state.
    /// </summary>
    /// <param name="id">The identifier of the store being persisted.</param>
    /// <param name="serializedStore">The serialized store data being persisted.</param>
    public async Task PersistStoreAsync(Guid id, string serializedStore)
    {
        await localStorageSvc.SetItemAsStringAsync(id.ToString(), serializedStore);
    }

    /// <summary>
    /// Rehydrates the store from a persisted state.
    /// </summary>
    /// <param name="id">The identifier of the store being retrieved.</param>
    public async Task<string?> RehydrateStoreAsync(Guid id)
    {
        return await localStorageSvc.GetItemAsStringAsync(id.ToString());
    }
}