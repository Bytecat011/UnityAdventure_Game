namespace Game.Gameplay.Features.Pause
{
    public interface IPauseService
    {
        bool IsPaused { get; }
        void Pause();
        void Unpause();
    }
}