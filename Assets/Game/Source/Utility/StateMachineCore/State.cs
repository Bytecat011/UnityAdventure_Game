using Game.Utility.Reactive;

namespace Game.Utility.StateMachineCore
{
    public abstract class State : IState
    {
        private ReactiveEvent _entered = new();
        private ReactiveEvent _exited = new();
        
        public IReadOnlyEvent Entered => _entered;
        public IReadOnlyEvent Exited  => _exited;

        public virtual void Enter() => _entered.Notify();

        public virtual void Exit() => _exited.Notify();
    }
}