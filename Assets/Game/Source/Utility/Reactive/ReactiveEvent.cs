using System;

namespace Game.Utility.Reactive
{
    public class ReactiveEvent : IReadOnlyEvent
    {
        private readonly SimpleSubscriptionList _subscriptions = new();

        public void Notify() => _subscriptions.Notify();
        
        public IDisposable Subscribe(Action action)
            => _subscriptions.CreateSubscription(action);
    }
    
    public class ReactiveEvent<T>: IReadOnlyEvent<T>
    {
        private readonly SimpleSubscriptionList<T> _subscriptions = new();

        public void Notify(T value) => _subscriptions.Notify(value);
        
        public IDisposable Subscribe(Action<T> action)
            => _subscriptions.CreateSubscription(action);
    }
}