using Game.Gameplay.EntitiesCore;
using Game.Utility.Conditions;
using Game.Utility.Reactive;
using UnityEngine;

namespace Game.Gameplay.Features.Movement
{
    public class MoveDirection : IEntityComponent
    {
        public ReactiveVariable<Vector3> Value;
    }

    public class MoveSpeed : IEntityComponent
    {
        public ReactiveVariable<float> Value;
    }

    public class CanMove : IEntityComponent
    {
        public ICompositeCondition Value;
    }
    
    public class RotationDirection : IEntityComponent
    {
        public ReactiveVariable<Vector3> Value;
    }

    public class RotationSpeed : IEntityComponent
    {
        public ReactiveVariable<float> Value;
    }
    
    public class CanRotate : IEntityComponent
    {
        public ICompositeCondition Value;
    }
}