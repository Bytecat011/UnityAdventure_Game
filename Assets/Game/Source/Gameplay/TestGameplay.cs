using System;
using Game.Configs.Gameplay.Entities;
using Game.Core.DI;
using Game.Gameplay.EntitiesCore;
using Game.Gameplay.Features.AI;
using Game.Gameplay.Features.AI.States;
using Game.Gameplay.Features.Enemies;
using Game.Gameplay.Features.MainHero;
using Game.Gameplay.Features.Movement;
using UnityEngine;

namespace Game.Gameplay
{
    public class TestGameplay : MonoBehaviour
    {
        private DIContainer _container;
        private EntitiesFactory _entitiesFactory;
        private BrainsFactory _brainsFactory;

        [SerializeField] private HeroConfig _heroConfig;
        [SerializeField] private GhostConfig _ghostConfig;
        
        private MainHeroFactory _mainHeroFactory;
        private EnemiesFactory _enemiesFactory;
        
        private Entity _entity;
        private Entity _ghost;
        
        private bool _isRunning;

        public void Initialize(DIContainer container)
        {
            _container = container;
            _entitiesFactory = _container.Resolve<EntitiesFactory>();
            _brainsFactory = _container.Resolve<BrainsFactory>();
            
            _mainHeroFactory = _container.Resolve<MainHeroFactory>();
            _enemiesFactory = _container.Resolve<EnemiesFactory>();
        }

        public void Run()
        {
            _entity = _mainHeroFactory.Create(Vector3.zero);
            
            _ghost = _enemiesFactory.Create(Vector3.zero + Vector3.forward * 5, _ghostConfig);
            
            _isRunning = true;
        }

        private void Update()
        {
            if (_isRunning == false)
                return;
        }
    }
}