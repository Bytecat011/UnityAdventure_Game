using System;
using System.Collections.Generic;
using Game.Gameplay.EntitiesCore;
using Game.Gameplay.Features.MainHero;
using Game.UI.CommonViews;
using Game.UI.Core;
using UnityEngine;

namespace Game.UI.Gameplay.HealthDisplay
{
    public class EntitiesHealthDisplayPresenter : IPresenter
    {
        private readonly EntitiesWorld _entitiesWorld;
        private readonly EntitiesHealthDisplay _view;
        
        private readonly GameplayPresentersFactory _presentersFactory;
        private readonly ViewsFactory _viewsFactory;
        
        private Dictionary<Entity, EntityHealthBarInfo> _entityToHealthBarInfo = new();

        public EntitiesHealthDisplayPresenter(
            EntitiesWorld entitiesWorld, 
            EntitiesHealthDisplay view, 
            ViewsFactory viewsFactory,
            GameplayPresentersFactory presentersFactory)
        {
            _entitiesWorld = entitiesWorld;
            _view = view;
            _viewsFactory = viewsFactory;
            _presentersFactory = presentersFactory;
        }

        public void Initialize()
        {
            _entitiesWorld.Added += OnEntityAdded;
            _entitiesWorld.Released += OnEntityReleased;
        }
        
        public void Dispose()
        {
            _entitiesWorld.Added -= OnEntityAdded;
            _entitiesWorld.Released -= OnEntityReleased;

            foreach (var info in _entityToHealthBarInfo.Values)
                DisposeFor(info);
            
            _entityToHealthBarInfo.Clear();
        }

        public void LateUpdate()
        {
            foreach (var info in _entityToHealthBarInfo.Values)
                _view.UpdatePositionsFor(info.HealthPresenter.Bar, info.HealthBarPoint.position);
        }
        
        private void OnEntityAdded(Entity entity)
        {
            if (!entity.TryGetHealthBarPoint(out Transform healthBarPoint))
                return;

            BarWithText healthBarView = null;

            if (entity.HasComponent<IsMainHero>())
                healthBarView = _viewsFactory.Create<BarWithText>(ViewIDs.MainHeroHealthBar);
            else
                healthBarView = _viewsFactory.Create<BarWithText>(ViewIDs.SimpleHealthBar);
            
            _view.Add(healthBarView);

            EntityHealthPresenter entityHealthPresenter =
                _presentersFactory.CreateEntityHealthPresenter(entity, healthBarView);
            entityHealthPresenter.Initialize();

            IDisposable removeReason = entity.IsDead.Subscribe((_, isdead) =>
            {
                if (isdead)
                    RemoveHealthBarFor(entity);
            });
            
            _entityToHealthBarInfo.Add(
                entity, 
                new EntityHealthBarInfo(healthBarPoint, removeReason, entityHealthPresenter)
            );
        }

        private void RemoveHealthBarFor(Entity entity)
        {
            EntityHealthBarInfo info = _entityToHealthBarInfo[entity];
            DisposeFor(info);
            _entityToHealthBarInfo.Remove(entity);
        }

        private void DisposeFor(EntityHealthBarInfo info)
        {
            info.RemoveReason.Dispose();
            
            _view.Remove(info.HealthPresenter.Bar);
            _viewsFactory.Release(info.HealthPresenter.Bar);
            
            info.HealthPresenter.Dispose();
        }

        private void OnEntityReleased(Entity entity)
        {
            if (_entityToHealthBarInfo.ContainsKey(entity))
                RemoveHealthBarFor(entity);
        }

        private class EntityHealthBarInfo
        {
            public Transform HealthBarPoint { get; }
            public IDisposable RemoveReason { get; }
            public EntityHealthPresenter HealthPresenter { get; }

            public EntityHealthBarInfo(
                Transform healthBarPoint,
                IDisposable removeReason,
                EntityHealthPresenter healthPresenter)
            {
                HealthBarPoint = healthBarPoint;
                RemoveReason = removeReason;
                HealthPresenter = healthPresenter;
            }
        }
    }
}