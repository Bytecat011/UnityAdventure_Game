using Game.Gameplay.EntitiesCore;
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
}