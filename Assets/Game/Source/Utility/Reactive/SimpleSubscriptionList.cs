using System;
using System.Collections.Generic;

namespace Game.Utility.Reactive
{
    public class SimpleSubscriptionList
    {
        private class Subscription : IDisposable
        {
            private readonly Action _action;
            private readonly Action<Subscription> _onRemove;

            public Subscription(Action action, Action<Subscription> onRemove)
            {
                _action = action;
                _onRemove = onRemove;
            }

            public void Notify() => _action?.Invoke();

            public void Dispose()
            {
                _onRemove?.Invoke(this);
            }
        }

        private readonly List<Subscription> _items = new();
        private readonly List<Subscription> _toAdd = new();
        private readonly List<Subscription> _toRemove = new();

        public IDisposable CreateSubscription(Action action)
        {
            var subscription = new Subscription(action, Remove);
            _toAdd.Add(subscription);
            return subscription;
        }

        private void Remove(Subscription subscription)
        {
            _toRemove.Add(subscription);
        }

        public void Notify()
        {
            if (_toAdd.Count > 0)
            {
                _items.AddRange(_toAdd);
                _toAdd.Clear();
            }

            if (_toRemove.Count > 0)
            {
                _items.AddRange(_toRemove);
                _toRemove.Clear();
            }

            foreach (var item in _items)
                item.Notify();
        }
    }
    
    public class SimpleSubscriptionList<T>
    {
        private class Subscription : IDisposable
        {
            private readonly Action<T> _action;
            private readonly Action<Subscription> _onRemove;

            public Subscription(Action<T> action, Action<Subscription> onRemove)
            {
                _action = action;
                _onRemove = onRemove;
            }

            public void Notify(T arg) => _action?.Invoke(arg);

            public void Dispose()
            {
                _onRemove?.Invoke(this);
            }
        }

        private readonly List<Subscription> _items = new();
        private readonly List<Subscription> _toAdd = new();
        private readonly List<Subscription> _toRemove = new();

        public IDisposable CreateSubscription(Action<T> action)
        {
            var subscription = new Subscription(action, Remove);
            _toAdd.Add(subscription);
            return subscription;
        }

        private void Remove(Subscription subscription)
        {
            _toRemove.Add(subscription);
        }

        public void Notify(T value)
        {
            if (_toAdd.Count > 0)
            {
                _items.AddRange(_toAdd);
                _toAdd.Clear();
            }

            if (_toRemove.Count > 0)
            {
                _items.AddRange(_toRemove);
                _toRemove.Clear();
            }

            foreach (var item in _items)
                item.Notify(value);
        }
    }
    
    public class SimpleSubscriptionList<T, K>
    {
        private class Subscription : IDisposable
        {
            private readonly Action<T, K> _action;
            private readonly Action<Subscription> _onRemove;

            public Subscription(Action<T, K> action, Action<Subscription> onRemove)
            {
                _action = action;
                _onRemove = onRemove;
            }

            public void Notify(T arg1, K arg2) => _action?.Invoke(arg1, arg2);

            public void Dispose()
            {
                _onRemove?.Invoke(this);
            }
        }

        private readonly List<Subscription> _items = new();
        private readonly List<Subscription> _toAdd = new();
        private readonly List<Subscription> _toRemove = new();

        public IDisposable CreateSubscription(Action<T, K> action)
        {
            var subscription = new Subscription(action, Remove);
            _toAdd.Add(subscription);
            return subscription;
        }

        private void Remove(Subscription subscription)
        {
            _toRemove.Add(subscription);
        }

        public void Notify(T oldValue, K newValue)
        {
            if (_toAdd.Count > 0)
            {
                _items.AddRange(_toAdd);
                _toAdd.Clear();
            }

            if (_toRemove.Count > 0)
            {
                _items.AddRange(_toRemove);
                _toRemove.Clear();
            }

            foreach (var item in _items)
                item.Notify(oldValue, newValue);
        }
    }
}