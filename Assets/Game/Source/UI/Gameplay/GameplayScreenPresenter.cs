using System.Collections.Generic;
using Game.UI.Core;
using Game.UI.Gameplay.HealthDisplay;
using Game.UI.Gameplay.Stages;
using Unity.VisualScripting;

namespace Game.UI.Gameplay
{
    public class GameplayScreenPresenter : IPresenter
    {
        private readonly GameplayScreenView _screen;
        
        private readonly GameplayPresentersFactory _presentersFactory;
        
        private readonly List<IPresenter> _childPresenters = new();
        
        private EntitiesHealthDisplayPresenter _entitiesHealthDisplayPresenter;

        public GameplayScreenPresenter(GameplayScreenView screen, GameplayPresentersFactory presentersFactory)
        {
            _screen = screen;
            _presentersFactory = presentersFactory;
        }

        public void Initialize()
        {
            CreateStageNumber();
            CreateEntitiesHealthDisplay();
            CreateMainHeroExpView();
            
            foreach (var presenter in _childPresenters)
                presenter.Initialize();
        }

        public void Dispose()
        {
            foreach (var presenter in _childPresenters)
                presenter.Dispose();
            
            _childPresenters.Clear();
        }

        public void LateUpdate()
        {
            _entitiesHealthDisplayPresenter.LateUpdate();
        }
        
        private void CreateStageNumber()
        {
            StagePresenter stagePresenter = _presentersFactory.CreateStagePresenter(_screen.StageNumberView);
            _childPresenters.Add(stagePresenter);
        }

        private void CreateEntitiesHealthDisplay()
        {
            _entitiesHealthDisplayPresenter =
                _presentersFactory.CreateEntitiesHealthDisplayPresenter(_screen.EntitiesHealthDisplay);
            
            _childPresenters.Add(_entitiesHealthDisplayPresenter);
        }

        private void CreateMainHeroExpView()
        {
            var view = _presentersFactory.CreateMainHeroExperiencePresenter(_screen.ExperienceBarView);
            
            _childPresenters.Add(view);
        }
    }
}