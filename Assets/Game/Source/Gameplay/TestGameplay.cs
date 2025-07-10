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
            _entity = _entitiesFactory.CreateHero(Vector3.zero);
            _entitiesFactory.CreateGhost(Vector3.zero + Vector3.forward * 5);
            
            _isRunning = true;
        }

        private void Update()
        {
            if (_isRunning == false)
                return;
            
            Vector3 input = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));
            
            _entity.MoveDirection.Value = input;
            _entity.RotationDirection.Value = input;
        }
    }
}