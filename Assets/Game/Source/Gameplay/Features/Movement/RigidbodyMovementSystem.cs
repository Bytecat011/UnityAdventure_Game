using Game.Gameplay.Common;
using Game.Gameplay.EntitiesCore;
using Game.Gameplay.EntitiesCore.Systems;
using Game.Utility.Conditions;
using Game.Utility.Reactive;
using UnityEngine;

namespace Game.Gameplay.Features.Movement
{
    public class RigidbodyMovementSystem : IInitializableSystem, IUpdatableSystem
    {
        private ReactiveVariable<Vector3> _moveDirection;
        private ReactiveVariable<float> _moveSpeed;
        private Rigidbody _rigidbody;
        private ReactiveVariable<bool> _isMoving;
        
        private ICompositeCondition _canMove;
        
        public void OnInit(Entity entity)
        {
            _moveDirection = entity.MoveDirection;
            _moveSpeed = entity.MoveSpeed;
            _rigidbody = entity.Rigidbody;
            _isMoving = entity.IsMoving;

            _canMove = entity.CanMove;
        }

        public void OnUpdate(float deltaTime)
        {
            if (_canMove.Evaluate() == false)
            {
                _rigidbody.velocity = Vector3.zero;
                return;
            }

            Vector3 velocity = _moveDirection.Value.normalized * _moveSpeed.Value;
            
            _isMoving.Value = velocity.sqrMagnitude > 0;
            
            _rigidbody.velocity = velocity;
        }
    }
}