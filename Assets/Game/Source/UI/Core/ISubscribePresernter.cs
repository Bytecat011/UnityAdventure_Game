namespace Game.UI.Core
{
    public interface ISubscribePresernter : IPresenter
    {
        void Subscribe();

        void Unsubscribe();
    }
}