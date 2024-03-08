using Fluxor;

namespace Shared.State.CounterUseCase;

[FeatureState]
public sealed record CounterState
{
    public int ClickCount { get; init; }

    public CounterState(int clickCount)
    {
        ClickCount = clickCount;
    }

    public CounterState()
    {
    }
}