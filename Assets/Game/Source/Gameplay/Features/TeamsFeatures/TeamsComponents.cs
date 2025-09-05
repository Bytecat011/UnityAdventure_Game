using Game.Gameplay.EntitiesCore;
using Game.Utility.Reactive;

namespace Game.Gameplay.Features.TeamsFeatures
{
    public class Team : IEntityComponent
    {
        public ReactiveVariable<Teams> Value;
    }
}