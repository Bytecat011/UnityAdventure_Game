using System;

namespace Game.Utility.Reactive
{
    public interface IReadOnlyEvent
    {
        ISubscription Subscribe(Action action);
    }
    
    public interface IReadOnlyEvent<T>
    {
        ISubscription Subscribe(Action<T> action);
    }
}