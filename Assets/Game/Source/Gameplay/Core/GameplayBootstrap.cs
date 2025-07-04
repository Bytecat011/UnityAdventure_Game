using Game.Core;
using Game.Core.DI;
using Game.Utility.SceneManagement;
using System;
using System.Collections;
using Game.Configs.Gameplay;
using UnityEngine;

namespace Game.Gameplay.Core
{
    public class GameplayBootstrap : SceneBootstrap
    {
        private DIContainer _container;
        private GameplayInputArgs _inputArgs;

        [SerializeField] private TestGameplay _testGameplay;
        
        public override void ProcessRegistrations(DIContainer container, IInputSceneArgs sceneArgs)
        {
            _container = container;

            if (sceneArgs is not GameplayInputArgs gameplayInputArgs)
            {
                throw new ArgumentException($"{nameof(sceneArgs)} is not match with {typeof(GameplayInputArgs)} type");
            }

            _inputArgs = gameplayInputArgs;

            GameplayContextRegistrations.Process(container, gameplayInputArgs);
        }

        public override IEnumerator Initialize()
        {
            _testGameplay.Initialize(_container);
            yield break;
        }

        public override void Run()
        {
            _testGameplay.Run();
        }
    }
}