using Game.Gameplay.Features.Input;
using Game.Gameplay.Features.Pause;
using Game.UI.Gameplay;
using Game.Utility.CoroutineManagement;
using Game.Utility.SceneManagement;
using Game.Utility.StateMachineCore;
using UnityEngine;

namespace Game.Gameplay.States
{
    public class DefeatState : EndGameState, IUpdatableState
    {
        private readonly GameplayPopupService _popupService;
        
        public DefeatState(
            IInputService inputService,
            IPauseService pauseService,
            GameplayPopupService popupService) : base(inputService, pauseService)
        {
            _popupService = popupService;
        }

        public override void Enter()
        {
            base.Enter();
            _popupService.OpenLosePopup();
        }

        public void Update(float deltaTime)
        {
        }
    }
}