using Game.Configs.Gameplay.Levels;
using Game.Core.DI;
using Game.Gameplay.EntitiesCore;
using Game.Gameplay.EntitiesCore.Mono;
using Game.Gameplay.Features.AI;
using Game.Gameplay.Features.Enemies;
using Game.Gameplay.Features.Input;
using Game.Gameplay.Features.MainHero;
using Game.Gameplay.Features.StagesFeature;
using Game.Gameplay.States;
using Game.UI;
using Game.UI.Core;
using Game.UI.Gameplay;
using Game.Utility.Assets;
using Game.Utility.Configs;

namespace Game.Gameplay.Core
{
    public static class GameplayContextRegistrations
    {
        private static GameplayInputArgs _inputArgs;
        
        public static void Process(DIContainer container, GameplayInputArgs args)
        {
            _inputArgs = args;
            
            container.RegisterAsSingle(CreateEntitiesFactory);
            container.RegisterAsSingle(CreateEntitiesWorld);
            container.RegisterAsSingle(CreateCollidersRegistryService);
            container.RegisterAsSingle(CreateBrainsFactory);
            container.RegisterAsSingle(CreateAIBrainsContext);
            container.RegisterAsSingle(CreateMainHeroFactory);
            container.RegisterAsSingle(CreateEnemiesFactory);
            container.RegisterAsSingle(CreateStagesFactory);
            container.RegisterAsSingle(CreateStagesProviderService);
            container.RegisterAsSingle(CreatePreparationTriggerService);
            container.RegisterAsSingle(CreateGameplayStatesFactory);
            container.RegisterAsSingle(CreateGameplayStatesContext);
            container.RegisterAsSingle(CreateMainHeroHolderService).NonLazy();
            container.RegisterAsSingle<IInputService>(CreateDesktopInput);
            container.RegisterAsSingle(CreateMonoEntitiesFactory).NonLazy();
            
            container.RegisterAsSingle(CreateGameplayUIRoot).NonLazy();
            container.RegisterAsSingle(createGameplayScreenPresenter).NonLazy();
            container.RegisterAsSingle(CreateGameplayPresentersFactory);
            container.RegisterAsSingle(CreateGameplayPopupService);
        }

        private static GameplayPopupService CreateGameplayPopupService(DIContainer c)
        {
            return new GameplayPopupService(
                c.Resolve<ViewsFactory>(),
                c.Resolve<ProjectPresentersFactory>(),
                c.Resolve<GameplayUIRoot>(),
                c.Resolve<GameplayPresentersFactory>()
            );
        }
        
        private static GameplayUIRoot CreateGameplayUIRoot(DIContainer c)
        {
            ResourcesAssetsLoader resourcesAssetsLoader = c.Resolve<ResourcesAssetsLoader>();
            
            GameplayUIRoot gameplayUIRootPrefab = resourcesAssetsLoader
                .Load<GameplayUIRoot>("UI/Gameplay/GameplayUIRoot");
            
            return UnityEngine.Object.Instantiate(gameplayUIRootPrefab);
        }
        
        private static GameplayScreenPresenter createGameplayScreenPresenter(DIContainer c)
        {
            GameplayUIRoot uiRoot = c.Resolve<GameplayUIRoot>();
            
            GameplayScreenView view = c
                .Resolve<ViewsFactory>()
                .Create<GameplayScreenView>(ViewIDs.GameplayScreen, uiRoot.HUDLayer);
            
            GameplayScreenPresenter presenter = c
                .Resolve<GameplayPresentersFactory>()
                .CreateGameplayScreenPresenter(view);
            
            return presenter;
        }
        
        private static GameplayPresentersFactory CreateGameplayPresentersFactory(DIContainer c)
        {
            return new GameplayPresentersFactory(c, _inputArgs);
        }
        
        private static GameplayStatesContext CreateGameplayStatesContext(DIContainer c)
        {
            return new GameplayStatesContext(c.Resolve<GameplayStatesFactory>().CreateGameStateMachine(_inputArgs));
        }
        
        private static GameplayStatesFactory CreateGameplayStatesFactory(DIContainer c)
        {
            return new GameplayStatesFactory(c);
        }
        
        private static MainHeroHolderService CreateMainHeroHolderService(DIContainer c)
            => new MainHeroHolderService(c.Resolve<EntitiesWorld>());
        
        private static PreparationTriggerService CreatePreparationTriggerService(DIContainer c)
            => new PreparationTriggerService(
                c.Resolve<EntitiesFactory>(),
                c.Resolve<EntitiesWorld>());
        
        private static StageProviderService CreateStagesProviderService(DIContainer c)
        {
            return new StageProviderService(
                c.Resolve<ConfigManager>().GetConfig<LevelsListConfig>().GetBy(_inputArgs.LevelNumber),
                c.Resolve<StagesFactory>());
        }
        
        private static StagesFactory CreateStagesFactory(DIContainer c)
            => new StagesFactory(c);
        
        private static EnemiesFactory CreateEnemiesFactory(DIContainer c)
            => new EnemiesFactory(c);
        
        private static MainHeroFactory CreateMainHeroFactory(DIContainer c)
            => new MainHeroFactory(c);

        private static DesktopInput CreateDesktopInput(DIContainer container)
        {
            return new DesktopInput();
        }

        private static AIBrainsContext CreateAIBrainsContext(DIContainer c) => new AIBrainsContext();

        private static BrainsFactory CreateBrainsFactory(DIContainer c) => new BrainsFactory(c);

        private static CollidersRegistryService CreateCollidersRegistryService(DIContainer c)
        {
            return new CollidersRegistryService();
        }

        private static MonoEntitiesFactory CreateMonoEntitiesFactory(DIContainer c)
        {
            return new MonoEntitiesFactory(
                c.Resolve<ResourcesAssetsLoader>(),
                c.Resolve<EntitiesWorld>(),
                c.Resolve<CollidersRegistryService>());
        }

        private static EntitiesWorld CreateEntitiesWorld(DIContainer c)
        {
            return new EntitiesWorld();
        }

        private static EntitiesFactory CreateEntitiesFactory(DIContainer c)
        {
            return new EntitiesFactory(c);
        }
    }
}