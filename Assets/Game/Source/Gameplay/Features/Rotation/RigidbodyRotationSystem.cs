using Game.Gameplay.EntitiesCore;
using Game.Gameplay.EntitiesCore.Systems;
using Game.Utility.Reactive;
using UnityEngine;

namespace Game.Gameplay.Features.Rotation
{
    public class RigidbodyRotationSystem : IInitializableSystem, IUpdatableSystem
    {
        private Rigidbody _rigidbody;
        private ReactiveVariable<Quaternion> _rotation;
        private ReactiveVariable<float> _rotationSpeed;
        
        public void OnInit(Entity entity)
        {
            _rotation = entity.Rotation;
            _rotationSpeed = entity.RotationSpeed;
            _rigidbody = entity.Rigidbody;
        }

        public void OnUpdate(float deltaTime)
        {
            float t = Mathf.Clamp01(_rotationSpeed.Value * deltaTime);
            _rigidbody.MoveRotation(Quaternion.Lerp(_rigidbody.rotation, _rotation.Value, t));
        }
    }
}