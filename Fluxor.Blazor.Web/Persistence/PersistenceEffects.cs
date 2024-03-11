using Fluxor.Persistence;
using Microsoft.Extensions.DependencyInjection;

namespace Fluxor.Blazor.Web.Persistence;

public sealed class PersistenceEffects(IPersistenceManager persistenceManager, IServiceProvider serviceProvider)
{
    /// <summary>
    /// Maintains a reference to IStore - injected this way to avoid a circular dependency during the effect method registration
    /// </summary>
    private readonly Lazy<IStore> _store = new(serviceProvider.GetRequiredService<IStore>);

    [EffectMethod(typeof(StorePersistingAction))]
    public async Task PersistStoreData(IDispatcher dispatcher)
    {
        //Serialize the store
        var json = _store.Value.SerializeToJson();

        //Save to the persistence manager
        await persistenceManager.PersistStoreToStateAsync(json);

        //Completed
        dispatcher.Dispatch(new StorePersistedAction());
    }

    [EffectMethod(typeof(StoreRehydratingAction))]
    public async Task RehydrateStoreData(IDispatcher dispatcher)
    {
        //Read from the persistence manager
        var serializedStore = await persistenceManager.RehydrateStoreFromStateAsync();
        if (serializedStore is null)
        {
            //Nothing to rehydrate - leave as-is
            dispatcher.Dispatch(new StoreRehydratedAction());
            return;
        }

        _store.Value.RehydrateFromJson(serializedStore);

        //Completed
        dispatcher.Dispatch(new StoreRehydratedAction());
    }
}