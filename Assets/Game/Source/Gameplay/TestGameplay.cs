using System;
using Game.Core.DI;
using Game.Gameplay.EntitiesCore;
using Game.Gameplay.Features.Movement;
using UnityEngine;

namespace Game.Gameplay
{
    public class TestGameplay : MonoBehaviour
    {
        private DIContainer _container;
        private EntitiesFactory _entitiesFactory;

        private Entity _entity;
        
        private bool _isRunning;

        public void Initialize(DIContainer container)
        {
            _container = container;
            _entitiesFactory = _container.Resolve<EntitiesFactory>();
        }

        public void Run()
        {
            _entity = _entitiesFactory.CreateTeleportingCharacter(Vector3.zero);
            _entitiesFactory.CreateGhost(Vector3.zero + Vector3.forward * 5);
            _entitiesFactory.CreateGhost(Vector3.zero + Vector3.left * 3);
            _entitiesFactory.CreateGhost(Vector3.zero + Vector3.right * 3);
            _entitiesFactory.CreateGhost(Vector3.zero + Vector3.forward * 5 + Vector3.left * 1.5f);
            _entitiesFactory.CreateGhost(Vector3.zero + Vector3.forward * 5 + Vector3.right * 1.5f);
            
            _isRunning = true;
        }

        private void Update()
        {
            if (_isRunning == false)
                return;
            
            if (Input.GetKeyDown(KeyCode.Space))
                _entity.TeleportAbilityStartRequest.Notify();
        }
    }
}