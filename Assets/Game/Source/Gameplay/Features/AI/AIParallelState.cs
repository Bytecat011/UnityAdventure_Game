using Game.Utility.StateMachineCore;

namespace Game.Gameplay.Features.AI
{
    public class AIParallelState : ParallelState<IUpdatableState>, IUpdatableState
    {
        public AIParallelState(params IUpdatableState[] states) : base(states)
        {
        }

        public void Update(float deltaTime)
        {
            foreach (var state in States)
                state.Update(deltaTime);
        }
    }
}