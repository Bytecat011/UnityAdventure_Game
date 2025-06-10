using System;

namespace Game.Utility.Reactive
{
    public interface IReactiveVariable<T>
    {
        T Value { get; }

        ISubscription Subscribe(Action<T, T> action);
    }
}