namespace Fluxor.Persistence;

public interface IPersistenceManager
{
    /// <summary>
    /// Persists the store to a persisted state.
    /// </summary>
    /// <param name="id">The identifier of the store being persisted.</param>
    /// <param name="serializedStore">The serialized store data being persisted.</param>
    public Task PersistStoreAsync(Guid id, string serializedStore);

    /// <summary>
    /// Rehydrates the store from a persisted state.
    /// </summary>
    /// <param name="id">The identifier of the store being retrieved.</param>
    public Task<string?> RehydrateStoreAsync(Guid id);
}