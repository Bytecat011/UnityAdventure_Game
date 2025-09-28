using System;
using System.Collections.Generic;
using Game.Configs.Gameplay.Stages;
using Game.Gameplay.EntitiesCore;
using Game.Gameplay.Features.Enemies;
using Game.Gameplay.Features.MainHero;
using Game.Utility;
using Game.Utility.Reactive;
using UnityEngine;

namespace Game.Gameplay.Features.StagesFeature
{
    public class ClearEnemiesWaveStage: IStage
    {
        private ClearEnemiesWaveStageConfig _config;

        private ReactiveEvent _completed = new();

        private EnemiesFactory _enemiesFactory;
        private EntitiesWorld _entitiesWorld;
        private MainHeroHolderService _mainHeroHolderService;

        private bool _inProcess;

        private int spawnedEnemyCount = 0;
        private float timeToSpawnNextEnemy;
        
        private Dictionary<Entity, IDisposable> _spawnedEnemiesToRemoveReason = new();

        public ClearEnemiesWaveStage(
            ClearEnemiesWaveStageConfig config, 
            EnemiesFactory enemiesFactory,
            EntitiesWorld entitiesWorld,
            MainHeroHolderService mainHeroHolderService)
        {
            _config = config;
            _enemiesFactory = enemiesFactory;
            _entitiesWorld = entitiesWorld;
            _mainHeroHolderService = mainHeroHolderService;
        }

        public IReadOnlyEvent Completed => _completed;

        public void Start()
        {
            if (_inProcess)
            {
                throw new InvalidOperationException("Game mode already started");
            }
            
            _inProcess = true;
        }
        
        public void Update(float deltaTime)
        {
            if (_inProcess == false)
                return;

            HandlePlayerAttack();
            
            if (spawnedEnemyCount < _config.TotalEnemyCount)
            {
                UpdateEnemySpawn(deltaTime);
                return;
            }
            if (_spawnedEnemiesToRemoveReason.Count == 0)
                ProcessEnd();
        }

        private void HandlePlayerAttack()
        {
            if (UnityEngine.Input.GetMouseButtonDown(0))
            {
                _mainHeroHolderService.MainHero.TowerAttackTargetPoint.Value = GetMousePositionInWorld();
                _mainHeroHolderService.MainHero.StartAttackRequest.Notify();
            }
        }

        private Vector3 GetMousePositionInWorld()
        {
            Ray ray = Camera.main.ScreenPointToRay(UnityEngine.Input.mousePosition);

            Vector3 worldPos = Vector3.zero;
            
            float distance = -ray.origin.y / ray.direction.y;
            if (distance >= 0)
            {
                worldPos = ray.origin + ray.direction * distance;
            }

            return worldPos;
        }

        private void UpdateEnemySpawn(float deltaTime)
        {
            if (timeToSpawnNextEnemy >= 0)
            {
                timeToSpawnNextEnemy -= deltaTime;
                if (timeToSpawnNextEnemy < 0)
                {
                    SpawnEnemy(_config.EnemyItems.GetRandomElement());
                    spawnedEnemyCount++;
                    timeToSpawnNextEnemy = _config.EnemySpawnCooldown;
                }
            }
        }
        
        private void SpawnEnemy(EnemyItemConfig enemyItemConfig)
        {
            var spawnPosition = RandomUtils.RandomPointInAnnulus(
                _mainHeroHolderService.MainHero.Transform.position, 
                _config.MinSpawnDistance,
                _config.MaxSpawnDistance);
            var spawnedEnemy = _enemiesFactory.Create(
                spawnPosition,
                enemyItemConfig.EnemyConfig);

            IDisposable removeReason = spawnedEnemy.IsDead.Subscribe((_, isDead) =>
            {
                IDisposable disposable = _spawnedEnemiesToRemoveReason[spawnedEnemy];
                disposable.Dispose();
                _spawnedEnemiesToRemoveReason.Remove(spawnedEnemy);
            });
            
            _spawnedEnemiesToRemoveReason.Add(spawnedEnemy, removeReason);
        }

        private void ProcessEnd()
        {
            _inProcess = false;
            _completed.Notify();
        }

        public void Cleanup()
        {
            foreach (var item in _spawnedEnemiesToRemoveReason)
            {
                item.Value.Dispose();
                _entitiesWorld.Release(item.Key);
            }
            
            _spawnedEnemiesToRemoveReason.Clear();
            
            _inProcess = false;
        }
        
        public void Dispose()
        {
            foreach (var disposable in _spawnedEnemiesToRemoveReason.Values)
            {
                disposable.Dispose();
            }
            
            _spawnedEnemiesToRemoveReason.Clear();
            
            _inProcess = false;
        }
    }
}