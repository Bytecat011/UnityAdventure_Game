using Game.Core.DI;
using Game.Data;
using Game.Utility.Configs;
using Game.Utility.CoroutineManagement;
using Game.Utility.LoadingScreen;
using Game.Utility.SceneManagement;
using System.Collections;
using Game.Gameplay.Core;
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

            gloabalContainer.Inizialize();

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
            PlayerDataProvider playerDataProvider = container.Resolve<PlayerDataProvider>();

            loadingScreen.Show();

            yield return container.Resolve<ConfigManager>().LoadAsync();

            bool isPlayerDataSaveExists = false;

            yield return playerDataProvider.ExistsTask(result => isPlayerDataSaveExists = result);

            if (isPlayerDataSaveExists)
            {
                yield return playerDataProvider.LoadTask();
            } else
            {
                playerDataProvider.Reset();
            }

            loadingScreen.Hide();

            yield return sceneSwitcherService.SwitchTo(Scenes.MainMenu);
        }
    }
}