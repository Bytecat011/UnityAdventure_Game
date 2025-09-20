using System.Collections.Generic;
using Game.UI.Core;
using Game.UI.Gameplay.Stages;

namespace Game.UI.Gameplay
{
    public class GameplayScreenPresenter : IPresenter
    {
        private readonly GameplayScreenView _screen;
        
        private readonly GameplayPresentersFactory _presentersFactory;
        
        private readonly List<IPresenter> _childPresenters = new();

        public GameplayScreenPresenter(GameplayScreenView screen, GameplayPresentersFactory presentersFactory)
        {
            _screen = screen;
            _presentersFactory = presentersFactory;
        }

        public void Initialize()
        {
            CreateStageNumber();
            
            foreach (var presenter in _childPresenters)
                presenter.Initialize();
        }

        public void Dispose()
        {
            foreach (var presenter in _childPresenters)
                presenter.Dispose();
            
            _childPresenters.Clear();
        }
        
        private void CreateStageNumber()
        {
            StagePresenter stagePresenter = _presentersFactory.CreaStagePresenter(_screen.StageNumberView);
            _childPresenters.Add(stagePresenter);
        }
    }
}