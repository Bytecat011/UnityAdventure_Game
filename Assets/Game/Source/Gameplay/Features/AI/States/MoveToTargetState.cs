using Game.Gameplay.EntitiesCore;
using Game.Utility.Reactive;
using Game.Utility.StateMachineCore;
using UnityEngine;

namespace Game.Gameplay.Features.AI.States
{
    public class MoveToTargetState : State, IUpdatableState
    {
        private ReactiveVariable<Vector3> _moveDirection;
        private ReactiveVariable<Entity> _currentTarget;
        private Transform _transform;

        public MoveToTargetState(Entity entity)
        {
            _moveDirection = entity.MoveDirection;
            _currentTarget = entity.CurrentTarget;
            _transform = entity.Transform;
        }

        public override void Exit()
        {
            base.Exit();
            
            _moveDirection.Value = Vector3.zero;
        }

        public void Update(float deltaTime)
        {
            if (_currentTarget.Value != null)
                _moveDirection.Value= (_currentTarget.Value.Transform.position - _transform.position).normalized;
        }
    }
}