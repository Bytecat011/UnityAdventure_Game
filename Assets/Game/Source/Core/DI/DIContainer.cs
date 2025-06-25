using System;
using System.Collections.Generic;

namespace Game.Core.DI
{
    public class DIContainer
    {
        private readonly Dictionary<Type, Registracion> _container = new();

        private readonly List<Type> _requests = new();

        private readonly DIContainer _parent;

        public DIContainer() : this(null) { }

        public DIContainer(DIContainer parent) => _parent = parent;

        public IRegistrationOptions RegisterAsSingle<T>(Func<DIContainer, T> creator)
        {
            if (IsAlreadyRegister<T>())
                throw new InvalidOperationException($"{typeof(T)} already register");

            var registration = new Registracion(container => creator.Invoke(container));
            _container.Add(typeof(T), registration);

            return registration;
        }

        public bool IsAlreadyRegister<T>()
        {
            if (_container.ContainsKey(typeof(T)))
                return true;

            if(_parent != null)
                return _parent.IsAlreadyRegister<T>();

            return false;
        }

        public T Resolve<T>()
        {
            if (_requests.Contains(typeof(T)))
                throw new InvalidOperationException($"Cycle resolve for {typeof(T)}");

            _requests.Add(typeof(T));

            try
            {
                if (_container.TryGetValue(typeof(T), out var registration))
                    return (T)registration.CreateInstanceFrom(this);

                if (_parent != null)
                    return _parent.Resolve<T>();
            }
            finally
            {
                _requests.Remove(typeof(T));
            }

            throw new InvalidOperationException($"Registration for {typeof(T)} not exists");
        }

        public void Inizialize()
        {
            foreach (var regitration in _container.Values)
            {
                if (regitration.IsNonLazy) 
                    regitration.CreateInstanceFrom(this);
            }
        }
    }
}