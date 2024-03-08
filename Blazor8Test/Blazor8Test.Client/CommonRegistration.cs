using Fluxor;
using Shared.State.CounterUseCase;

namespace Blazor8Test.Client;

public static class CommonRegistration
{
    public static void ConfigureCommonServices(IServiceCollection services)
    {
        services.AddFluxor(x => x.ScanAssemblies(typeof(Program).Assembly, typeof(CounterState).Assembly));
    }
}