using Game.Data;
using Game.Gameplay.Core;
using Game.Gameplay.Features.Input;
using Game.Meta.Features.LevelsProgression;
using Game.UI.Gameplay;
using Game.Utility.CoroutineManagement;
using Game.Utility.SceneManagement;
using Game.Utility.StateMachineCore;
using UnityEngine;

namespace Game.Gameplay.States
{
    public class WinState : EndGameState, IUpdatableState
    {
        private readonly LevelsProgressionService _levelsProgressionService;
        private readonly GameplayInputArgs _gameplayInputArgs;
        private readonly PlayerDataProvider _playerDataProvider;
        private readonly ICoroutineRunner _coroutineRunner;
        
        private readonly GameplayPopupService _popupService;
        
        public WinState(
            IInputService inputService, 
            LevelsProgressionService levelsProgressionService,
            GameplayInputArgs gameplayInputArgs,
            PlayerDataProvider playerDataProvider,
            ICoroutineRunner coroutineRunner,
            GameplayPopupService popupService) : base(inputService)
        {
            _levelsProgressionService = levelsProgressionService;
            _gameplayInputArgs = gameplayInputArgs;
            _playerDataProvider = playerDataProvider;
            _coroutineRunner = coroutineRunner;
            _popupService = popupService;
        }

        public override void Enter()
        {
            base.Enter();
            
            _levelsProgressionService.AddLevelToCompleted(_gameplayInputArgs.LevelNumber);
            _coroutineRunner.StartTask(_playerDataProvider.SaveTask());

            _popupService.OpenWinPopup();
        }

        public void Update(float deltaTime)
        {
        }
    }
}