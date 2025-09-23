using System.Collections.Generic;
using Game.Gameplay.EntitiesCore;
using Game.Gameplay.EntitiesCore.Systems;
using Game.Utility.Reactive;

namespace Game.Gameplay.Features.Stats
{
    public class DamageStatSyncSystem : IInitializableSystem, IUpdatableSystem
    {
        private ReactiveVariable<float> _damage;
        private Dictionary<StatTypes, float> _modifiedStats;
        
        public void OnInit(Entity entity)
        {
            _damage = entity.InstantAttackDamage;
            _modifiedStats = entity.ModifiedStats;
        }

        public void OnUpdate(float deltaTime)
        {
            float tempValue = _modifiedStats[StatTypes.Damage];
            
            if (tempValue < 0)
                tempValue = 0;
            
            _damage.Value = tempValue;
        }
    }
}