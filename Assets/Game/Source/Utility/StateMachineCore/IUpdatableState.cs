namespace Game.Utility.StateMachineCore
{
    public interface IUpdatableState : IState
    {
        void Update(float deltaTime);
    }
}