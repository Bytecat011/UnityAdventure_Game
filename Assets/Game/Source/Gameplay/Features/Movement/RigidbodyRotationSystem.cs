using Game.Gameplay.EntitiesCore;
using Game.Gameplay.EntitiesCore.Systems;
using Game.Utility.Conditions;
using Game.Utility.Reactive;
using UnityEngine;

namespace Game.Gameplay.Features.Movement
{
    public class RigidbodyRotationSystem : IInitializableSystem, IUpdatableSystem
    {
        private Rigidbody _rigidbody;
        
        private ReactiveVariable<float> _rotationSpeed;
        private ReactiveVariable<Vector3> _direction;

        private ICompositeCondition _canRotate;

        public void OnInit(Entity entity)
        {
            _rigidbody = entity.Rigidbody;
            _rotationSpeed = entity.RotationSpeed;
            _direction = entity.RotationDirection;
            
            _canRotate = entity.CanRotate;
        }

        public void OnUpdate(float deltaTime)
        {
            if (_canRotate.Evaluate() == false)
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