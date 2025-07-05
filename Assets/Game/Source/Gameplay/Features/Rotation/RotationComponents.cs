using Game.Gameplay.EntitiesCore;
using Game.Utility.Reactive;
using UnityEngine;

namespace Game.Gameplay.Features.Rotation
{
    public class RotationComponent : IEntityComponent
    {
        public ReactiveVariable<Quaternion> Value;
    }
    
    public class RotationSpeed : IEntityComponent
    {
        public ReactiveVariable<float> Value;
    }
}