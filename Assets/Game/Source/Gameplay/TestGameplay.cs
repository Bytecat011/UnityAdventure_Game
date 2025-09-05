using System;
using Game.Configs.Gameplay.Entities;
using Game.Core.DI;
using Game.Gameplay.EntitiesCore;
using Game.Gameplay.Features.AI;
using Game.Gameplay.Features.AI.States;
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
        
        private Entity _entity;
        private Entity _ghost;
        
        private bool _isRunning;

        public void Initialize(DIContainer container)
        {
            _container = container;
            _entitiesFactory = _container.Resolve<EntitiesFactory>();
            _brainsFactory = _container.Resolve<BrainsFactory>();
        }

        public void Run()
        {
            _entity = _entitiesFactory.CreateHero(Vector3.zero, _heroConfig);
            _entity.AddCurrentTarget();
            _brainsFactory.CreateMainHeroBrain(_entity, new NearestDamageableTargetSelector(_entity));
            
            _ghost = _entitiesFactory.CreateGhost(Vector3.zero + Vector3.forward * 5, _ghostConfig);
            
            _isRunning = true;
        }

        private void Update()
        {
            if (_isRunning == false)
                return;
            
            if (Input.GetKeyDown(KeyCode.R))
                _entity.StartAttackRequest.Notify();

            if (Input.GetKeyDown(KeyCode.I))
                _brainsFactory.CreateGhostBrain(_ghost);
        }
    }
}