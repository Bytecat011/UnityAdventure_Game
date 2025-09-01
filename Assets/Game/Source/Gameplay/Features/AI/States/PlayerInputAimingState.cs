using Game.Gameplay.EntitiesCore;
using Game.Gameplay.Features.Input;
using Game.Utility.Reactive;
using Game.Utility.StateMachineCore;
using UnityEngine;

namespace Game.Gameplay.Features.AI.States
{
    public class PlayerInputAimingState : State, IUpdatableState
    {
        private IInputService _inputService;
        private float _mouseSensitivity;
        private ReactiveVariable<Vector3> _rotationDirection;

        public PlayerInputAimingState(Entity entity, IInputService inputService, float mouseSensitivity)
        {
            _inputService = inputService;
            _rotationDirection = entity.RotationDirection;
            _mouseSensitivity = mouseSensitivity;
        }

        public void Update(float deltaTime)
        {
            var rotation = Quaternion.AngleAxis(_inputService.MouseDelta.x * _mouseSensitivity, Vector3.up);
            _rotationDirection.Value = rotation * _rotationDirection.Value;
        }
    }
}