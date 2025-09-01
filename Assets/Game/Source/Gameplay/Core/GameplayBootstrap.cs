using Game.Core;
using Game.Core.DI;
using Game.Utility.SceneManagement;
using System;
using System.Collections;
using Game.Configs.Gameplay;
using Game.Gameplay.EntitiesCore;
using Game.Gameplay.Features.AI;
using Game.Gameplay.Features.Input;
using UnityEngine;

namespace Game.Gameplay.Core
{
    public class GameplayBootstrap : SceneBootstrap
    {
        private DIContainer _container;
        private GameplayInputArgs _inputArgs;

        [SerializeField] private TestGameplay _testGameplay;

        private EntitiesWorld _entitiesWorld;
        private AIBrainsContext _brainsContext;
        private IInputService _inputService;
        
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
            
            _brainsContext = _container.Resolve<AIBrainsContext>();
            
            _inputService = _container.Resolve<IInputService>();
            
            _testGameplay.Initialize(_container);
            yield break;
        }

        public override void Run()
        {
            _testGameplay.Run();
        }

        private void Update()
        {
            _brainsContext?.Update(Time.deltaTime);
            _entitiesWorld?.Update(Time.deltaTime);
            _inputService?.Update(Time.deltaTime);
        }
    }
}