using Game.Utility.Reactive;

namespace Game.Utility.StateMachineCore
{
    public interface IState
    {
        IReadOnlyEvent Entered { get; }
        IReadOnlyEvent Exited { get; }
        
        void Enter();
        void Exit();
    }
}