using System;
using System.Collections.Generic;
using Game.Gameplay.EntitiesCore.Systems;

namespace Game.Gameplay.EntitiesCore
{
    public partial class Entity : IDisposable
    {
        private readonly Dictionary<Type, IEntityComponent> _components = new();

        private readonly List<IEntitySystem> _systems = new();
        
        private readonly List<IInitializableSystem> _initializables = new();
        private readonly List<IUpdatableSystem> _updatables = new();
        private readonly List<IDisposableSystem> _disposables = new();

        private bool _isInit;
        
        public void Initialize()
        {
            foreach (var system in _initializables)
                system.OnInit(this);

            _isInit = true;
        }

        public void OnUpdate(float deltaTime)
        {
            if (_isInit == false)
                return;
            
            foreach (var system in _updatables)
                system.OnUpdate(deltaTime);
        }

        public void Dispose()
        {
            foreach (var system in _disposables)
                system.OnDispose();
            
            _isInit = false;
        }
        
        public Entity AddComponent<TComponent>(TComponent component) where TComponent : class, IEntityComponent
        {
            _components.Add(typeof(TComponent), component);

            return this;
        }

        public bool HasComponent<TComponent>() where TComponent : class, IEntityComponent
        {
            return _components.ContainsKey(typeof(TComponent));
        }
        
        public bool TryGetComponent<TComponent>(out TComponent component) where TComponent : class, IEntityComponent
        {
            if (_components.TryGetValue(typeof(TComponent), out IEntityComponent findedObject))
            {
                component = (TComponent)findedObject;
                return true;
            }
            
            component = null;
            return false;
        }

        public TComponent GetComponent<TComponent>() where TComponent : class, IEntityComponent
        {
            if (TryGetComponent(out TComponent component) == false)
                throw new ArgumentException($"Entity not contains component of type {typeof(TComponent)}");

            return component;
        }

        public Entity AddSystem(IEntitySystem system)
        {
            if (_systems.Contains(system))
                throw new ArgumentException(system.GetType().ToString());
            
            _systems.Add(system);

            if (system is IInitializableSystem initializable)
            {
                _initializables.Add(initializable);
                
                if(_isInit)
                    initializable.OnInit(this);
            }

            if(system is IUpdatableSystem updatable)
                _updatables.Add(updatable);
            
            if(system is IDisposableSystem disposable)
                _disposables.Add(disposable);
            
            return this;
        }
    }
}