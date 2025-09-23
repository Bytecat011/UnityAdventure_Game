using System;
using System.Collections.Generic;
using Game.Configs.Gameplay;
using Game.Gameplay.EntitiesCore;
using Game.Gameplay.Features.MainHero;
using Game.UI.CommonViews;
using Game.UI.Core;
using Game.Utility.Reactive;

namespace Game.UI.Gameplay.Experience
{
    public class MainHeroExperiencePresenter : IPresenter
    {
        private BarWithText _view;

        private MainHeroHolderService _mainHeroHolderService;
        private ExperienceForUpgradeLevelConfig _levelUpConfig;
        private ReactiveVariable<float> _experience;
        private ReactiveVariable<int> _currentLevel;

        private List<IDisposable> _disposables = new();

        public MainHeroExperiencePresenter(
            MainHeroHolderService mainHeroHolderService,
            ExperienceForUpgradeLevelConfig levelUpConfig,
            BarWithText view)
        {
            _mainHeroHolderService = mainHeroHolderService;
            _levelUpConfig = levelUpConfig;
            _view = view;
        }

        public void Initialize()
        {
            _disposables.Add(_mainHeroHolderService.HeroRegistered.Subscribe(OnMainHeroRegistered));
        }

        private void OnMainHeroRegistered(Entity hero)
        {
            _experience = hero.Experience;
            _currentLevel = hero.Level;
            
            _disposables.Add(_experience.Subscribe(OnCurrentExpChanged));
            _disposables.Add(_currentLevel.Subscribe(OnLevelChanged));
            
            UpdateBarText(_currentLevel.Value);
            UpdateCurrentExp(_experience.Value);
        }

        private void UpdateCurrentExp(float value)
            => _view.UpdateSlider(value / _levelUpConfig.GetExperienceFor(_currentLevel.Value));

        private void UpdateBarText(int level) => _view.UpdateText($"Lv.{level}");

        private void OnLevelChanged(int _, int level)
            => UpdateBarText(_currentLevel.Value);

        private void OnCurrentExpChanged(float _, float exp)
            => UpdateCurrentExp(_experience.Value);

        public void Dispose()
        {
            foreach (var disposable in _disposables)
                disposable.Dispose();
        }
    }
}