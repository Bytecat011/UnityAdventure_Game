using System;
using System.Collections.Generic;
using Game.Utility.Assets;
using UnityEngine;

namespace Game.UI.Core
{
    public class ViewsFactory
    {
        private readonly ResourcesAssetsLoader _resourcesAssetsLoader;

        private readonly Dictionary<string, string> _viewIDToResourcePath = new()
        {
            { ViewIDs.ResourceView, "UI/Resources/ResourceView" },
            { ViewIDs.MainMenuScreen, "UI/MainMenu/MainMenuScreenView" },
            { ViewIDs.TestPopup, "UI/TestPopup" },
            { ViewIDs.LevelTile, "UI/LevelsMenuPopup/LevelTile" },
            { ViewIDs.LevelsMenuPopup, "UI/LevelsMenuPopup/LevelsMenuPopup" },
            { ViewIDs.GameplayScreen, "UI/Gameplay/GameplayScreenView" },
            { ViewIDs.WinPopup, "UI/Gameplay/ResultsPopup/WinPopup" },
            { ViewIDs.LosePopup, "UI/Gameplay/ResultsPopup/LosePopup" },
            { ViewIDs.SimpleHealthBar, "UI/Gameplay/HealthBars/SimpleHealthBar" },
            { ViewIDs.MainHeroHealthBar, "UI/Gameplay/HealthBars/HeroHealthBar" },
        };

        public ViewsFactory(ResourcesAssetsLoader resourcesAssetsLoader)
        {
            _resourcesAssetsLoader = resourcesAssetsLoader;
        }

        public TView Create<TView>(string viewID, Transform parent = null) where TView : MonoBehaviour, IView
        {
            if (_viewIDToResourcePath.TryGetValue(viewID, out var resourcePath) == false)
                throw new ArgumentException($"You didn't set resource path for {typeof(TView)}, searched id: {viewID}");

            GameObject prefab = _resourcesAssetsLoader.Load<GameObject>(resourcePath);
            GameObject instance = UnityEngine.Object.Instantiate(prefab, parent);
            TView view = instance.GetComponent<TView>();

            if (view == null)
                throw new InvalidOperationException($"Not found {typeof(TView)} component on view instance");
            
            return view;
        }

        public void Release<TView>(TView view) where TView : MonoBehaviour, IView
        {
            UnityEngine.Object.Destroy(view.gameObject);
        }
    }
}