using Game.Core.DI;
using Game.Utility.Configs;
using Game.Utility.CoroutineManagment;
using Game.Utility.LoadingScreen;
using Game.Utility.SceneManagment;
using System.Collections;
using UnityEngine;

namespace Game.Core.EntryPoint
{
    public class GameEntryPoint : MonoBehaviour
    {
        private void Awake()
        {
            SetupAppSettings();

            DIContainer gloabalContainer = new DIContainer();

            GlobalContextRegistration.Process(gloabalContainer);

            gloabalContainer.Resolve<ICoroutineRunner>().StartTask(Initialize(gloabalContainer));
        }

        private void SetupAppSettings()
        {
            QualitySettings.vSyncCount = 0;
            Application.targetFrameRate = 60;
        }

        private IEnumerator Initialize(DIContainer container)
        {
            ILoadingScreen loadingScreen = container.Resolve<ILoadingScreen>();
            SceneSwitcherService sceneSwitcherService = container.Resolve<SceneSwitcherService>();

            loadingScreen.Show();

            yield return container.Resolve<ConfigManager>().LoadAsync();

            loadingScreen.Hide();

            yield return sceneSwitcherService.SwitchTo(Scenes.MainMenu);
        }
    }
}