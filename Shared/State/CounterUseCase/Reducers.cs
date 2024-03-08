using Fluxor;

namespace Shared.State.CounterUseCase;

public static class Reducers
{
    //[ReducerMethod(typeof(IncrementCounterAction))]
    [ReducerMethod]
    public static CounterState ReduceIncrementCounterAction(CounterState state, IncrementCounterAction action)
    {
        return new CounterState(state.ClickCount + 1);
        //return state with
        //{
        //    ClickCount = state.ClickCount + 1
        //};
    }
}