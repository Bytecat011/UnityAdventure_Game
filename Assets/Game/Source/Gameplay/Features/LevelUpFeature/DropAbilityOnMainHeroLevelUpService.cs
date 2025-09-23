using System;
using System.Collections;
using System.Collections.Generic;
using Game.Core.DI;
using Game.Gameplay.EntitiesCore;
using Game.Gameplay.Features.MainHero;
using Game.UI.Gameplay;
using Game.UI.Gameplay.AbilitySelectPopup;
using Game.Utility.CoroutineManagement;
using UnityEngine;

namespace Game.Gameplay.Features.LevelUpFeature
{
    public class DropAbilityOnMainHeroLevelUpService : IInitializable, IDisposable
    {
        private MainHeroHolderService _mainHeroHolderService;
        private GameplayPopupService _popupService;
        private ICoroutineRunner _coroutineRunner;

        private Queue<int> _levelUpRequests = new();

        private AbilitySelectPopupPresenter _popup;
        private Coroutine _selectAbilityTask;
        
        private IDisposable _heroRegistredSubscription;
        private IDisposable _heroLevelChangedSubscription;

        public DropAbilityOnMainHeroLevelUpService(
            MainHeroHolderService mainHeroHolderService, 
            GameplayPopupService popupService,
            ICoroutineRunner coroutineRunner)
        {
            _mainHeroHolderService = mainHeroHolderService;
            _popupService = popupService;
            _coroutineRunner = coroutineRunner;
        }

        private bool PopupIsOpened => _popup != null;
        
        public void Initialize()
        {
            _heroRegistredSubscription = _mainHeroHolderService.HeroRegistered.Subscribe(OnMainHeroRegistred);
        }

        public void Dispose()
        {
            _heroRegistredSubscription?.Dispose();
            _heroLevelChangedSubscription?.Dispose();
        }
        
        private void OnMainHeroRegistred(Entity hero)
        {
            _heroLevelChangedSubscription = hero.Level.Subscribe(OnHeroLevelChanged);
        }

        private void OnHeroLevelChanged(int _, int currentLevel)
        {
            _levelUpRequests.Enqueue(currentLevel);
            
            if (_selectAbilityTask != null)
                return;

            _selectAbilityTask = _coroutineRunner.StartTask(SelectAbilityTask());
        }

        private IEnumerator SelectAbilityTask()
        {
            while (_levelUpRequests.Count > 0)
            {
                int level = _levelUpRequests.Dequeue();

                _popup = _popupService.OpenAbilitySelectPopup(_mainHeroHolderService.MainHero, level, () =>
                {
                    _popup = null;
                });
                
                yield return new WaitUntil(() => PopupIsOpened == false);
            }
            
            _selectAbilityTask = null;
        }
    }
}