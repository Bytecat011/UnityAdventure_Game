using Game.Gameplay.EntitiesCore;
using Game.Utility.Reactive;
using Game.Utility.StateMachineCore;
using UnityEngine;

namespace Game.Gameplay.Features.AI.States
{
    public class RandomMovementState : State, IUpdatableState
    {
        private ReactiveVariable<Vector3> _movementDirection;
        private ReactiveVariable<Vector3> _rotationDirection;

        private float _cooldownBetweenDirectionGeneration;

        private float _time;

        public RandomMovementState(
            Entity entity,
            float cooldownBetweenDirectionGeneration)
        {
            _movementDirection = entity.MoveDirection;
            _rotationDirection = entity.RotationDirection;
            
            _cooldownBetweenDirectionGeneration = cooldownBetweenDirectionGeneration;
        }

        public override void Enter()
        {
            base.Enter();

            Vector3 randomDirection = new Vector3(Random.Range(-1.0f, 1.0f), 0, Random.Range(-1.0f, 1.0f)).normalized;
            _movementDirection.Value = randomDirection;
            _rotationDirection.Value = randomDirection;
        }

        public override void Exit()
        {
            base.Exit();
            
            _movementDirection.Value = Vector3.zero;
        }

        public void Update(float deltaTime)
        {
            _time += deltaTime;

            if (_time >= _cooldownBetweenDirectionGeneration)
            {
                GenerateNewDirection();
                _time = 0f;
            }
        }

        private void GenerateNewDirection()
        {
            Vector3 inverseDirection = -_movementDirection.Value.normalized;
            Quaternion randomTurn = Quaternion.Euler(0, Random.Range(-30, 30), 0);
            Vector3 newDirection = randomTurn * inverseDirection;
            
            _movementDirection.Value = newDirection;
            _rotationDirection.Value = newDirection;
        }
    }
}