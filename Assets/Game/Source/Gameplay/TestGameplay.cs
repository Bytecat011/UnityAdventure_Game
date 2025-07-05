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
        private EntitiesWorld _entitiesWorld;

        private Entity _entity;
        
        private bool _isRunning;

        public void Initialize(DIContainer container)
        {
            _container = container;
            _entitiesFactory = _container.Resolve<EntitiesFactory>();
            _entitiesWorld = _container.Resolve<EntitiesWorld>();
        }

        public void Run()
        {
            _isRunning = true;
        }

        private void Update()
        {
            if (_isRunning == false)
                return;

            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                if (_entity != null)
                    _entitiesWorld.Release(_entity);

                _entity = _entitiesFactory.CreateRigidbodyCharacter(Vector3.zero);
                Debug.Log("Rigidbody character created");
            }
            
            if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                if (_entity != null)
                    _entitiesWorld.Release(_entity);

                _entity = _entitiesFactory.CreateCharacterControllerCharacter(Vector3.zero);
                Debug.Log("CharacterController character created");
            }
            
            if (_entity != null)
            {
                Vector3 input = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));

                _entity.MoveDirection.Value = input;
            }
        }
    }
}