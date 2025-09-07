using System;
using System.Collections.Generic;
using Game.Configs.Gameplay.Stages;
using Game.Gameplay.EntitiesCore;
using Game.Gameplay.Features.Enemies;
using Game.Utility.Reactive;

namespace Game.Gameplay.Features.StagesFeature
{
    public class ClearAllEnemyStage : IStage
    {
        private ClearAllEnemiesStageConfig _config;

        private ReactiveEvent _completed = new();

        private EnemiesFactory _enemiesFactory;
        private EntitiesWorld _entitiesWorld;

        private bool _inProcess;
        
        private Dictionary<Entity, IDisposable> _spawnedEnemiesToRemoveReason = new();

        public ClearAllEnemyStage(
            ClearAllEnemiesStageConfig config, 
            EnemiesFactory enemiesFactory,
            EntitiesWorld entitiesWorld)
        {
            _config = config;
            _enemiesFactory = enemiesFactory;
            _entitiesWorld = entitiesWorld;
        }

        public IReadOnlyEvent Completed => _completed;

        public void Start()
        {
            if (_inProcess)
            {
                throw new InvalidOperationException("Game mode already started");
            }

            SpawnEnemies();
            
            _inProcess = true;
        }

        private void SpawnEnemies()
        {
            foreach (var enemyItemConfig in _config.EnemyItems)
                SpawnEnemy(enemyItemConfig);
        }

        private void SpawnEnemy(EnemyItemConfig enemyItemConfig)
        {
            var spawnedEnemy = _enemiesFactory.Create(enemyItemConfig.SpawnPosition, enemyItemConfig.EnemyConfig);

            IDisposable removeReason = spawnedEnemy.IsDead.Subscribe((_, isDead) =>
            {
                IDisposable disposable = _spawnedEnemiesToRemoveReason[spawnedEnemy];
                disposable.Dispose();
                _spawnedEnemiesToRemoveReason.Remove(spawnedEnemy);
            });
            
            _spawnedEnemiesToRemoveReason.Add(spawnedEnemy, removeReason);
        }

        public void Update(float deltaTime)
        {
            if (_inProcess == false)
                return;

            if (_spawnedEnemiesToRemoveReason.Count == 0)
                ProcessEnd();
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