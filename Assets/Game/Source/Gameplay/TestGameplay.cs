using Game.Core.DI;
using Game.Gameplay.EntitiesCore;
using Game.Gameplay.Features.AI;
using Game.Gameplay.Features.AI.States;
using Game.Utility.Reactive;
using UnityEngine;

namespace Game.Gameplay
{
    public class TestGameplay : MonoBehaviour
    {
        private DIContainer _container;
        private EntitiesFactory _entitiesFactory;
        private BrainsFactory _brainsFactory;
        private EntitiesWorld _entitiesWorld;

        private Entity _entity;
        
        private bool _isRunning;

        public void Initialize(DIContainer container)
        {
            _container = container;
            _entitiesFactory = _container.Resolve<EntitiesFactory>();
            _brainsFactory = _container.Resolve<BrainsFactory>();
            _entitiesWorld = _container.Resolve<EntitiesWorld>();
        }

        public void Run()
        {
            var _ghost1 = _entitiesFactory.CreateGhost(Vector3.zero + Vector3.forward * 5);
            _brainsFactory.CreateGhostBrain(_ghost1);
            var _ghost2 = _entitiesFactory.CreateGhost(Vector3.zero + Vector3.left * 5);
            _brainsFactory.CreateGhostBrain(_ghost2);
            var _ghost3 = _entitiesFactory.CreateGhost(Vector3.zero + Vector3.right * 5);
            _brainsFactory.CreateGhostBrain(_ghost3);
            
            _isRunning = true;
        }

        private void Update()
        {
            if (_isRunning == false)
                return;
            
            if (Input.GetKeyDown(KeyCode.R))
                _entity.StartAttackRequest.Notify();

            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                if (_entity != null)
                {
                    _entitiesWorld.Release(_entity);  
                }
                
                _entity = _entitiesFactory.CreateTeleportingCharacter(Vector3.zero);
                _brainsFactory.CreateRandomTeleportBrain(_entity);
            }
             
            if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                if (_entity != null)
                {
                    _entitiesWorld.Release(_entity);  
                }
                
                _entity = _entitiesFactory.CreateTeleportingCharacter(Vector3.zero);
                _entity.AddCurrentTarget();
                _brainsFactory.CreatTeleportToTargetBrain(_entity, new LowestHealthInRangeTargetSelector(_entity), 0.4f);
            }
        }
    }
}