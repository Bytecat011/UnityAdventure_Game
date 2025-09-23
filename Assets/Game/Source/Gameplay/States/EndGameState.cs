using Game.Gameplay.Features.Input;
using Game.Gameplay.Features.Pause;
using Game.Utility.StateMachineCore;

namespace Game.Gameplay.States
{
    public abstract class EndGameState : State
    {
        private readonly IInputService _inputService;
        private readonly IPauseService _pauseService;

        protected EndGameState(IInputService inputService, IPauseService pauseService)
        {
            _inputService = inputService;
            _pauseService = pauseService;
        }

        public override void Enter()
        {
            base.Enter();

            _inputService.IsEnabled = false;
            _pauseService.Pause();
        }

        public override void Exit()
        {
            base.Exit();
            
            _inputService.IsEnabled = true;
            _pauseService.Unpause();
        }
    }
}