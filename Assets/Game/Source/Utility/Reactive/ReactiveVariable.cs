using System;

namespace Game.Utility.Reactive
{
    public class ReactiveVariable<T> : IReactiveVariable<T> where T : IEquatable<T>
    {
        private readonly SimpleSubscriptionList<T, T> _subscriptions = new();

        private T _value;

        public ReactiveVariable() => _value = default;

        public ReactiveVariable(T value) => _value = value;

        public T Value
        {
            get => _value;
            set
            {
                T oldValue = _value;

                _value = value;

                if (_value.Equals(oldValue) == false)
                    _subscriptions.Notify(oldValue, _value);
            }
        }

        public IDisposable Subscribe(Action<T, T> action)
            => _subscriptions.CreateSubscription(action);
    }
}