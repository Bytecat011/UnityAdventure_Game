using Game.Gameplay.Features.Input;
using Game.Utility.CoroutineManagement;
using Game.Utility.SceneManagement;
using Game.Utility.StateMachineCore;
using UnityEngine;

namespace Game.Gameplay.States
{
    public class DefeatState : EndGameState, IUpdatableState
    {
        private readonly SceneSwitcherService _sceneSwitcherService;
        private readonly ICoroutineRunner _coroutineRunner;
        
        public DefeatState(
            IInputService inputService,
            SceneSwitcherService sceneSwitcherService,
            ICoroutineRunner coroutineRunner) : base(inputService)
        {
            _sceneSwitcherService = sceneSwitcherService;
            _coroutineRunner = coroutineRunner;
        }

        public override void Enter()
        {
            base.Enter();
            
            Debug.Log("Lose!");
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