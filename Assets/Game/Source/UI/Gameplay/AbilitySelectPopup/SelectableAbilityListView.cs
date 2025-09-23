using DG.Tweening;
using Game.UI.CommonViews;
using Game.UI.Core;

namespace Game.UI.Gameplay.AbilitySelectPopup
{
    public class SelectableAbilityListView : ElementsListView<SelectableAbilityView>, IShowableView
    {
        private Sequence _currentAnimation;

        public void Select(SelectableAbilityView abilityView)
        {
            foreach (var view in Elements)
                view.Unselect();
            
            abilityView.Select();
        }
        
        public Tween Show()
        {
            _currentAnimation?.Kill();
            _currentAnimation = DOTween.Sequence();
            
            foreach (var view in Elements)
            {
                _currentAnimation.Append(view.Show());
                _currentAnimation.AppendInterval(0.2f);
            }

            return _currentAnimation.SetUpdate(true).Play();
        }

        public Tween Hide()
        {
            _currentAnimation?.Kill();
            _currentAnimation = DOTween.Sequence();

            foreach (var view in Elements)
            {
                _currentAnimation.Append(view.Hide());
                _currentAnimation.AppendInterval(0.2f);
            }

            return _currentAnimation.SetUpdate(true).Play();
        }
    }
}