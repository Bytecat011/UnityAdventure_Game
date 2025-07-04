using Game.Core;
using Game.Core.DI;
using Game.Utility.SceneManagement;
using System;
using System.Collections;
using Game.Configs.Gameplay;
using Game.Gameplay.EntitiesCore;
using UnityEngine;

namespace Game.Gameplay.Core
{
    public class GameplayBootstrap : SceneBootstrap
    {
        private DIContainer _container;
        private GameplayInputArgs _inputArgs;

        [SerializeField] private TestGameplay _testGameplay;

        private EntitiesWorld _entitiesWorld;
        
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
            _entitiesWorld = _container.Resolve<EntitiesWorld>();
            
            _testGameplay.Initialize(_container);
            yield break;
        }

        public override void Run()
        {
            _testGameplay.Run();
        }

        private void Update()
        {
            _entitiesWorld?.Update(Time.deltaTime);
        }
    }
}