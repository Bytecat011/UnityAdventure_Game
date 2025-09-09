using Game.Gameplay.Features.StagesFeature;
using Game.Utility.StateMachineCore;

namespace Game.Gameplay.States
{
    public class StageProcessStage : State, IUpdatableState
    {
        private readonly StageProviderService _stageProviderService;

        public StageProcessStage(StageProviderService stageProviderService)
        {
            _stageProviderService = stageProviderService;
        }

        public override void Enter()
        {
            base.Enter();
            
            _stageProviderService.SwitchToNext();
            _stageProviderService.StartCurrent();
        }

        public void Update(float deltaTime)
        {
            _stageProviderService.UpdateCurrent(deltaTime);
        }

        public override void Exit()
        {
            base.Exit();
            
            _stageProviderService.CleanupCurrent();
        }
    }
}