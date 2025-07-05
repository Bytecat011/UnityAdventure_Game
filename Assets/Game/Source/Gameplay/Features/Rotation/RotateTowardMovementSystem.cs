using Game.Gameplay.EntitiesCore;
using Game.Gameplay.EntitiesCore.Systems;
using Game.Utility.Reactive;
using UnityEngine;

namespace Game.Gameplay.Features.Rotation
{
    public class RotateTowardMovementSystem: IInitializableSystem, IUpdatableSystem
    {
        private ReactiveVariable<Quaternion> _rotation;
        private ReactiveVariable<Vector3> _move;
        
        public void OnInit(Entity entity)
        {
            _rotation = entity.Rotation;
            _move = entity.MoveDirection;
        }

        public void OnUpdate(float deltaTime)
        {
            if (_move.Value.sqrMagnitude > 0.01f)
            {
                _rotation.Value = Quaternion.LookRotation(_move.Value.normalized);
            }
        }
    }
}