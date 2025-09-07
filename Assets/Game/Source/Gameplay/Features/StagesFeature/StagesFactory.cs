using System;
using Game.Configs.Gameplay.Stages;
using Game.Core.DI;
using Game.Gameplay.EntitiesCore;
using Game.Gameplay.Features.Enemies;

namespace Game.Gameplay.Features.StagesFeature
{
    public class StagesFactory
    {
        private readonly DIContainer _container;

        public StagesFactory(DIContainer container)
        {
            _container = container;
        }

        public IStage Create(StageConfig stageConfig)
        {
            switch (stageConfig)
            {
                case ClearAllEnemiesStageConfig clearAllEnemiesStageConfig:
                    return new ClearAllEnemyStage(
                        clearAllEnemiesStageConfig,
                        _container.Resolve<EnemiesFactory>(),
                        _container.Resolve<EntitiesWorld>());
                
                default:
                    throw new ArgumentException($"Not supported {stageConfig.GetType()} type config");
            }
        }
    }
}