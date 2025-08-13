using System;

namespace Game.Utility.Reactive
{
    public interface IReactiveVariable<T>
    {
        T Value { get; }

        IDisposable Subscribe(Action<T, T> action);
    }
}