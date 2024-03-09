using Blazored.LocalStorage;
using Fluxor.Persistence;

namespace Fluxor.Blazor.Web.Persistence;

public sealed class PersistenceEffects(IPersistenceManager persistenceManager, IStore store, ILocalStorageService localStorage)
{
    [EffectMethod(typeof(StorePersistingAction))]
    public async Task PersistStoreData(IDispatcher dispatcher)
    {
        //Serialize the store
        var json = store.SerializeToJson();

        //Get the identifier for the store from local storage (whether this is running on server or web assembly)
        var storeIdentifier = await GetStoreIdentifierAsync();

        //Save to the persistence manager
        await persistenceManager.PersistStoreAsync(storeIdentifier, json);

        //Completed
        dispatcher.Dispatch(new StorePersistedAction());
    }

    [EffectMethod(typeof(StoreRehydratingAction))]
    public async Task RehydrateStoreData(IDispatcher dispatcher)
    {
        //Get the identifier for the store from local storage (whether this is running on server or web assembly)
        var storeIdentifier = await GetStoreIdentifierAsync();

        //Read from the persistence manager
        var serializedStore = await persistenceManager.RehydrateStoreAsync(storeIdentifier);
        if (serializedStore is null)
        {
            //Nothing to rehydrate - leave as-is
            dispatcher.Dispatch(new StoreRehydratedAction());
            return;
        }

        store.RehydrateFromJson(serializedStore);

        //Completed
        dispatcher.Dispatch(new StoreRehydratedAction());
    }

    /// <summary>
    /// Pulls the store identifier from the local storage, if found. Otherwise, creates a new value and persists
    /// to local storage.
    /// </summary>
    /// <returns></returns>
    private async Task<Guid> GetStoreIdentifierAsync()
    {
        const string localStorageIdentifier = "__flxid";

        //Attempt to pull the identifier from the local storage, if it exists
        var storeIdentifier = await localStorage.GetItemAsStringAsync(localStorageIdentifier);
        if (storeIdentifier != null && Guid.TryParse(storeIdentifier, out var storeId))
        {
            return storeId;
        }

        //Otherwise, create a new identifier and persist it
        var newIdentifier = Guid.NewGuid();
        await localStorage.SetItemAsStringAsync(localStorageIdentifier, newIdentifier.ToString());
        return newIdentifier;
    }
}