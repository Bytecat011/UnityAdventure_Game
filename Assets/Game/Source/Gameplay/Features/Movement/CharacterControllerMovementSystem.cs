using Game.Gameplay.EntitiesCore;
using Game.Gameplay.EntitiesCore.Systems;
using Game.Utility.Reactive;
using UnityEngine;

namespace Game.Gameplay.Features.Movement
{
    public class CharacterControllerMovementSystem : IInitializableSystem, IUpdatableSystem
    {
        private ReactiveVariable<Vector3> _moveDirection;
        private ReactiveVariable<float> _moveSpeed;
        private CharacterController _characterController;
        
        public void OnInit(Entity entity)
        {
            _moveDirection = entity.MoveDirection;
            _moveSpeed = entity.MoveSpeed;
            _characterController = entity.CharacterController;
        }

        public void OnUpdate(float deltaTime)
        {
            Vector3 move = _moveDirection.Value.normalized * (_moveSpeed.Value * deltaTime);
            
            _characterController.Move(move);
        }
    }
}