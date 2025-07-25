using Game.Core.DI;
using Game.Gameplay.EntitiesCore;
using Game.Gameplay.EntitiesCore.Mono;
using Game.Utility.Assets;

namespace Game.Gameplay.Core
{
    public static class GameplayContextRegistrations
    {
        public static void Process(DIContainer container, GameplayInputArgs args)
        {
            container.RegisterAsSingle(CreateEntitiesFactory);
            container.RegisterAsSingle(CreateEntitiesWorld);
            container.RegisterAsSingle(CreateCollidersRegistryService);
            container.RegisterAsSingle(creaMonoEntitiesFactory).NonLazy();
        }

        private static CollidersRegistryService CreateCollidersRegistryService(DIContainer c)
        {
            return new CollidersRegistryService();
        }
        
        private static MonoEntitiesFactory creaMonoEntitiesFactory(DIContainer c)
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