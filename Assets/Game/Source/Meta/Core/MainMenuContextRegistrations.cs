using Game.Core.DI;
using Game.Meta.Features.LevelSelection;
using Game.UI;
using Game.UI.Core;
using Game.UI.MainMenu;
using Game.Utility.Assets;
using Game.Utility.Configs;

namespace Game.Meta.Core
{
    public static class MainMenuContextRegistrations
    {
        public static void Process(DIContainer container)
        {
            container.RegisterAsSingle(CreateMainMenuUIRoot).NonLazy();
            container.RegisterAsSingle(CreateMainMenuPresentersFactory);
            container.RegisterAsSingle(CreateMainMenuScreenPresenter).NonLazy();
            container.RegisterAsSingle(CreateMainMenuPopupService);
            container.RegisterAsSingle<ILevelSelector>(CreateRandomLevelSelector);
        }

        private static RandomLevelSelector CreateRandomLevelSelector(DIContainer c)
        {
            return new RandomLevelSelector(c.Resolve<ConfigManager>());
        }
        
        private static MainMenuPopupService CreateMainMenuPopupService(DIContainer c)
        {
            return new MainMenuPopupService(
                c.Resolve<ViewsFactory>(),
                c.Resolve<ProjectPresentersFactory>(),
                c.Resolve<MainMenuUIRoot>());
        }
        
        private static MainMenuUIRoot CreateMainMenuUIRoot(DIContainer c)
        {
            var resourceAssetsLoader = c.Resolve<ResourcesAssetsLoader>();

            var mainMenuUIRootPrefab = resourceAssetsLoader
                .Load<MainMenuUIRoot>("UI/MainMenu/MainMenuUIRoot");

            return UnityEngine.Object.Instantiate(mainMenuUIRootPrefab);
        }

        private static MainMenuPresentersFactory CreateMainMenuPresentersFactory(DIContainer c)
        {
            return new MainMenuPresentersFactory(c);
        }

        private static MainMenuScreenPresenter CreateMainMenuScreenPresenter(DIContainer c)
        {
            MainMenuUIRoot uiRoot = c.Resolve<MainMenuUIRoot>();

            MainMenuScreenView view = c
                .Resolve<ViewsFactory>()
                .Create<MainMenuScreenView>(ViewIDs.MainMenuScreen, uiRoot.HUDLayer);

            MainMenuScreenPresenter presenter = c
                .Resolve<MainMenuPresentersFactory>()
                .CreateMainMenuScreen(view);

            return presenter;
        }
    }
}