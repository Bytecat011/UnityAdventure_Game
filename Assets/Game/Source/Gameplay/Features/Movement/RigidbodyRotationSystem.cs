using Game.Gameplay.EntitiesCore;
using Game.Gameplay.EntitiesCore.Systems;
using Game.Utility.Reactive;
using UnityEngine;

namespace Game.Gameplay.Features.Movement
{
    public class RigidbodyRotationSystem : IInitializableSystem, IUpdatableSystem
    {
        private Rigidbody _rigidbody;
        
        private ReactiveVariable<float> _rotationSpeed;
        private ReactiveVariable<Vector3> _direction;

        private ReactiveVariable<bool> _isDead;

        public void OnInit(Entity entity)
        {
            _rigidbody = entity.Rigidbody;
            _rotationSpeed = entity.RotationSpeed;
            _direction = entity.RotationDirection;
            
            _isDead = entity.IsDead;
        }

        public void OnUpdate(float deltaTime)
        {
            if (_isDead.Value)
                return;
            
            if (_direction.Value == Vector3.zero)
                return;

            var lookRotation = Quaternion.LookRotation(_direction.Value.normalized);
            
            float step = _rotationSpeed.Value * deltaTime;
            
            var rotation = Quaternion.RotateTowards(_rigidbody.rotation, lookRotation, step);
            
            _rigidbody.MoveRotation(rotation);
        }
    }
}