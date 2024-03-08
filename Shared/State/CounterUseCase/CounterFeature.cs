using Fluxor;

namespace Shared.State.CounterUseCase;

public class CounterFeature : Feature<CounterState>
{
    public override string GetName() => "CounterState";

    protected override CounterState GetInitialState() => new(0);
}