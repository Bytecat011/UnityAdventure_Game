using System;
using Game.Configs.Gameplay.Entities;
using Game.Configs.Gameplay.Stages;
using Game.Core.DI;
using Game.Gameplay.EntitiesCore;
using Game.Gameplay.Features.AI;
using Game.Gameplay.Features.AI.States;
using Game.Gameplay.Features.Enemies;
using Game.Gameplay.Features.MainHero;
using Game.Gameplay.Features.Movement;
using Game.Gameplay.Features.StagesFeature;
using UnityEngine;

namespace Game.Gameplay
{
    public class TestGameplay : MonoBehaviour
    {
        private DIContainer _container;
        private EntitiesFactory _entitiesFactory;
        private BrainsFactory _brainsFactory;

        [SerializeField] private HeroConfig _heroConfig;
        
        [SerializeField] private StageConfig _stageConfig;
        
        private StagesFactory _stagesFactory;
        private IStage _stage;
        
        private MainHeroFactory _mainHeroFactory;
        private EnemiesFactory _enemiesFactory;
        
        private Entity _entity;
        private Entity _ghost;
        
        private bool _isRunning;

        public void Initialize(DIContainer container)
        {
            _container = container;
            _entitiesFactory = _container.Resolve<EntitiesFactory>();
            _brainsFactory = _container.Resolve<BrainsFactory>();
            
            _mainHeroFactory = _container.Resolve<MainHeroFactory>();
            _enemiesFactory = _container.Resolve<EnemiesFactory>();
            
            _stagesFactory = _container.Resolve<StagesFactory>();
        }

        public void Run()
        {
            _entity = _mainHeroFactory.Create(Vector3.zero);

            _stage = _stagesFactory.Create(_stageConfig);
            _stage.Completed.Subscribe(OnCompleted);
            _stage.Start();
            
            _isRunning = true;
        }

        private void OnCompleted()
        {
            Debug.Log("Win!");
            _stage.Cleanup();
        }

        private void Update()
        {
            if (_isRunning == false)
                return;
            
            _stage.Update(Time.deltaTime);
        }
    }
}