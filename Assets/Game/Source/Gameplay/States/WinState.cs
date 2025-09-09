using Game.Data;
using Game.Gameplay.Core;
using Game.Gameplay.Features.Input;
using Game.Meta.Features.LevelsProgression;
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
        private readonly SceneSwitcherService _sceneSwitcherService;
        private readonly ICoroutineRunner _coroutineRunner;
        
        public WinState(
            IInputService inputService, 
            LevelsProgressionService levelsProgressionService,
            GameplayInputArgs gameplayInputArgs,
            PlayerDataProvider playerDataProvider, 
            SceneSwitcherService sceneSwitcherService,
            ICoroutineRunner coroutineRunner) : base(inputService)
        {
            _levelsProgressionService = levelsProgressionService;
            _gameplayInputArgs = gameplayInputArgs;
            _playerDataProvider = playerDataProvider;
            _sceneSwitcherService = sceneSwitcherService;
            _coroutineRunner = coroutineRunner;
        }

        public override void Enter()
        {
            base.Enter();
            
            Debug.Log("Win!");
            
            _levelsProgressionService.AddLevelToCompleted(_gameplayInputArgs.LevelNumber);

            _coroutineRunner.StartTask(_playerDataProvider.SaveTask());
        }

        public void Update(float deltaTime)
        {
            if (Input.GetKeyDown(KeyCode.Q))
            {
                _coroutineRunner.StartTask(_sceneSwitcherService.SwitchTo(Scenes.MainMenu));
            }
        }
    }
}