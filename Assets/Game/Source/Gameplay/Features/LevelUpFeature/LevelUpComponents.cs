using Game.Gameplay.EntitiesCore;
using Game.Utility.Reactive;

namespace Game.Gameplay.Features.LevelUpFeature
{
    public class Experience : IEntityComponent
    {
        public ReactiveVariable<float> Value;
    }

    public class Level : IEntityComponent
    {
        public ReactiveVariable<int> Value;
    }
}