using Game.Gameplay.EntitiesCore;
using Game.Utility.Reactive;

namespace Game.Gameplay.Features.Energy
{
    public class MaximumEnergy : IEntityComponent
    {
        public ReactiveVariable<float> Value;
    }
    
    public class CurrentEnergy : IEntityComponent
    {
        public ReactiveVariable<float> Value;
    }
    
    public class EnergyRestorationTimer : IEntityComponent
    {
        public ReactiveVariable<float> Value;
    }
    
    public class EnergyRestorationInterval : IEntityComponent
    {
        public ReactiveVariable<float> Value;
    }
    
    public class EnergyRestorationPercentageAmount : IEntityComponent
    {
        public ReactiveVariable<float> Value;
    }
}