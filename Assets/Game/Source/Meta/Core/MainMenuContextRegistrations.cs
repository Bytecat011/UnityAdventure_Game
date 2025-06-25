using Game.Core.DI;
using Game.Meta.MainMenu;
using Game.Utility.CoroutineManagement;
using Game.Utility.SceneManagement;
using Unity.VisualScripting;

namespace Game.Meta.Core
{
    public static class MainMenuContextRegistrations
    {
        public static void Process(DIContainer container)
        {
            container.RegisterAsSingle(CreateLevelSelector);
        }

        private static LevelSelector CreateLevelSelector(DIContainer c)
        {
            return new LevelSelector(c.Resolve<ICoroutineRunner>(), c.Resolve<SceneSwitcherService>());
        }
    }
}