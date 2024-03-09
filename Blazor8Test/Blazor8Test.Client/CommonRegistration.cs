using Blazored.LocalStorage;
using Fluxor;
using Fluxor.Blazor.Web.Persistence;
using Fluxor.Persistence;
using Shared.Persistence;
using Shared.State.CounterUseCase;

namespace Blazor8Test.Client;

public static class CommonRegistration
{
    public static void ConfigureCommonServices(IServiceCollection services)
    {
        services.AddFluxor(x => x.ScanAssemblies(typeof(Program).Assembly, typeof(CounterState).Assembly, typeof(PersistenceEffects).Assembly));
        services.AddScoped<IPersistenceManager, LocalStorePersistenceManager>();
        
        services.AddBlazoredLocalStorage();
    }
}