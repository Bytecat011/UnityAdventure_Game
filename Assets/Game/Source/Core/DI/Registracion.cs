using System;

namespace Game.Core.DI
{
    public class Registracion : IRegistrationOptions
    {
        private Func<DIContainer, object> _creator;
        private object _cachedInstance;

        public bool IsNonLazy { get; private set; }

        public Registracion(Func<DIContainer, object> creator)
        {
            _creator = creator;
        }

        public object CreateInstanceFrom(DIContainer container)
        {
            if (_cachedInstance != null)
                return _cachedInstance;

            if (_creator == null)
                throw new InvalidOperationException("Not has instance or creator");

            _cachedInstance = _creator?.Invoke(container);

            return _cachedInstance;
        }

        public void OnInitialize()
        {
            if (_cachedInstance != null)
                if(_cachedInstance is IInitializable initializable)
                    initializable.Initialize();
        }

        public void OnDispose()
        {
            if (_cachedInstance != null)
                if(_cachedInstance is IDisposable disposable)
                    disposable.Dispose();
        }
        
        public void NonLazy() => IsNonLazy = true;
    }
}