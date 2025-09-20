using System.Collections.Generic;
using Game.UI.Core;

namespace Game.UI.Gameplay
{
    public class GameplayScreenPresenter : IPresenter
    {
        private readonly GameplayScreenView _screen;
        
        private readonly List<IPresenter> _childPresenters = new();

        public GameplayScreenPresenter(GameplayScreenView screen)
        {
            _screen = screen;
        }

        public void Initialize()
        {
            foreach (var presenter in _childPresenters)
                presenter.Initialize();
        }

        public void Dispose()
        {
            foreach (var presenter in _childPresenters)
                presenter.Dispose();
            
            _childPresenters.Clear();
        }
    }
}