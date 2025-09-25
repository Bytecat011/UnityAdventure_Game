using Game.Data;
using Game.Gameplay.Core;
using Game.Gameplay.Features.Input;
using Game.Gameplay.Features.Pause;
using Game.UI.Gameplay;
using Game.Utility.CoroutineManagement;
using Game.Utility.StateMachineCore;
using UnityEngine;

namespace Game.Gameplay.States
{
    public class WinState : EndGameState, IUpdatableState
    {
        private readonly GameplayInputArgs _gameplayInputArgs;
        private readonly PlayerDataProvider _playerDataProvider;
        private readonly ICoroutineRunner _coroutineRunner;
        
        private readonly GameplayPopupService _popupService;
        
        public WinState(
            IInputService inputService, 
            IPauseService pauseService,
            GameplayInputArgs gameplayInputArgs,
            PlayerDataProvider playerDataProvider,
            ICoroutineRunner coroutineRunner,
            GameplayPopupService popupService) : base(inputService, pauseService)
        {
            _gameplayInputArgs = gameplayInputArgs;
            _playerDataProvider = playerDataProvider;
            _coroutineRunner = coroutineRunner;
            _popupService = popupService;
        }

        public override void Enter()
        {
            base.Enter();
            
            _coroutineRunner.StartTask(_playerDataProvider.SaveTask());

            _popupService.OpenWinPopup();
        }

        public void Update(float deltaTime)
        {
        }
    }
}