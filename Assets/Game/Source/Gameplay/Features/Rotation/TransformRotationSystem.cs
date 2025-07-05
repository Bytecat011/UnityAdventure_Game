using Game.Gameplay.EntitiesCore;
using Game.Gameplay.EntitiesCore.Systems;
using Game.Utility.Reactive;
using UnityEngine;

namespace Game.Gameplay.Features.Rotation
{
    public class TransformRotationSystem : IInitializableSystem, IUpdatableSystem
    {
        private Transform _transform;
        private ReactiveVariable<Quaternion> _rotation;
        private ReactiveVariable<float> _rotationSpeed;
        
        public void OnInit(Entity entity)
        {
            _rotation = entity.Rotation;
            _rotationSpeed = entity.RotationSpeed;
            _transform = entity.Transform;
        }

        public void OnUpdate(float deltaTime)
        {
            float t = Mathf.Clamp01(_rotationSpeed.Value * deltaTime);
            _transform.rotation = Quaternion.Lerp(_transform.rotation, _rotation.Value, t);
        }
    }
}