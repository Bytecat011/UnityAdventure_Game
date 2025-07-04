using Game.Gameplay.Common;
using Game.Gameplay.EntitiesCore;
using Game.Gameplay.EntitiesCore.Systems;
using Game.Utility.Reactive;
using UnityEngine;

namespace Game.Gameplay.Features.Movement
{
    public class RigidbodyMovementSystem : IInitializableSystem, IUpdatableSystem
    {
        private ReactiveVariable<Vector3> _moveDirection;
        private ReactiveVariable<float> _moveSpeed;
        private Rigidbody _rigidbody;
        
        public void OnInit(Entity entity)
        {
            _moveDirection = entity.GetComponent<MoveDirection>().Value;
            _moveSpeed = entity.GetComponent<MoveSpeed>().Value;
            _rigidbody = entity.GetComponent<RigidbodyComponent>().Value;
        }

        public void OnUpdate(float deltaTime)
        {
            Vector3 velocity = _moveDirection.Value.normalized * _moveSpeed.Value;
            
            _rigidbody.velocity = velocity;
        }
    }
}