using Game.Gameplay.Features.StagesFeature;
using Game.Utility.StateMachineCore;
using UnityEngine;

namespace Game.Gameplay.States
{
    public class PreparationState : State, IUpdatableState
    {
        private readonly PreparationTriggerService _preparationTriggerService;

        public PreparationState(PreparationTriggerService preparationTriggerService)
        {
            _preparationTriggerService = preparationTriggerService;
        }

        public override void Enter()
        {
            base.Enter();
            
            Vector3 nextStageTriggerPosition = Vector3.zero + Vector3.forward * 4;
            _preparationTriggerService.Create(nextStageTriggerPosition);
        }

        public void Update(float deltaTime)
        {
            _preparationTriggerService.Update(deltaTime);            
        }

        public override void Exit()
        {
            base.Exit();
            
            _preparationTriggerService.Cleanup();
        }
    }
}