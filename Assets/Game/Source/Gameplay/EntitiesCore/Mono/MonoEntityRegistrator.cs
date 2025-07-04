using UnityEngine;

namespace Game.Gameplay.EntitiesCore.Mono
{
    public abstract class MonoEntityRegistrator : MonoBehaviour
    {
        public abstract void Register(Entity entity);
    }
}