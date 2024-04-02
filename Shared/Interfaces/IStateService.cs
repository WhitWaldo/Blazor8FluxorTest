namespace Shared.Interfaces;

public interface IStateService
{
    Task PersistSerializedStateAsync(string serializedState);

    Task<string?> RetrieveSerializedStateAsync();
}