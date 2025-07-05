using UnityEngine;

namespace Game.Gameplay.EntitiesCore.Mono
{
    public class MonoEntity : MonoBehaviour
    {
        public void Setup(Entity entity)
        {
            MonoEntityRegistrator[] registrators = GetComponentsInChildren<MonoEntityRegistrator>();
            
            if(registrators != null)
                foreach(MonoEntityRegistrator registrator in registrators)
                    registrator.Register(entity);

            entity.AddTransform(transform);
        }

        public void Cleanup(Entity entity)
        {
            
        }
    }
}