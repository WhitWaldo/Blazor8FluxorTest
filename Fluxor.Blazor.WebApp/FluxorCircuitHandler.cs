using Microsoft.AspNetCore.Components.Server.Circuits;

namespace Fluxor.Blazor.WebApp;

public class FluxorHandler : CircuitHandler
{
    public IFluxorCircuitHandler Handler;

    public FluxorHandler(IFluxorCircuitHandler handler)
    {
        Handler = handler;
    }

    public override Task OnCircuitOpenedAsync(Circuit circuit, CancellationToken cancellationToken)
    {
        Handler.CurrentCircuit = circuit;
        return base.OnCircuitOpenedAsync(circuit, cancellationToken);
    }
}

public interface IFluxorCircuitHandler
{
    public Circuit? CurrentCircuit { get; set; }
}

public class FluxorCircuitHandler : IFluxorCircuitHandler
{
    public Circuit? CurrentCircuit { get; set; }
}