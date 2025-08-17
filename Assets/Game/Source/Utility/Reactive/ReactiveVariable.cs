using System;
using System.Collections.Generic;

namespace Game.Utility.Reactive
{
    public class ReactiveVariable<T> : IReactiveVariable<T>
    {
        private readonly SimpleSubscriptionList<T, T> _subscriptions = new();

        private T _value;
        private IEqualityComparer<T> _comparer;

        public ReactiveVariable() :this(default)
        {
        }

        public ReactiveVariable(T value) : this(value, EqualityComparer<T>.Default)
        {
        }

        public ReactiveVariable(T value, IEqualityComparer<T> comparer)
        {
            _value = value;
            _comparer = comparer;
        }

        public T Value
        {
            get => _value;
            set
            {
                T oldValue = _value;

                _value = value;

                if (_comparer.Equals(oldValue, value) == false)
                    _subscriptions.Notify(oldValue, _value);
            }
        }

        public IDisposable Subscribe(Action<T, T> action)
            => _subscriptions.CreateSubscription(action);
    }
}