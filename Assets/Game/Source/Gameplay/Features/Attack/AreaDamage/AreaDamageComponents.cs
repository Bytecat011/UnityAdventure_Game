using Game.Gameplay.EntitiesCore;
using Game.Utility.Reactive;
using UnityEngine;

namespace Game.Gameplay.Features.Attack.AreaDamage
{
    public class AreaDamageRequest : IEntityComponent
    {
        public ReactiveEvent<Vector3> Value;
    }
    
    public class AreaDamage : IEntityComponent
    {
        public ReactiveVariable<float> Value;
    }
    
    public class AreaDamageRange : IEntityComponent
    {
        public ReactiveVariable<float> Value;
    }
    
    public class AreaDamageLayerMask : IEntityComponent
    {
        public LayerMask Value;
    }
}