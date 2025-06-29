using DG.Tweening;

namespace Game.UI.Core
{
    public interface IShowableView : IView
    {
        Tween Show();
        
        Tween Hide();
    }
}