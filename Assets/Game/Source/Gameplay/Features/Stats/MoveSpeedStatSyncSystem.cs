using System.Collections.Generic;
using Game.Gameplay.EntitiesCore;
using Game.Gameplay.EntitiesCore.Systems;
using Game.Utility.Reactive;

namespace Game.Gameplay.Features.Stats
{
    public class MoveSpeedStatSyncSystem : IInitializableSystem, IUpdatableSystem
    {
        private ReactiveVariable<float> _speed;
        private Dictionary<StatTypes, float> _modifiedStats;
        
        public void OnInit(Entity entity)
        {
            _speed = entity.MoveSpeed;
            _modifiedStats = entity.ModifiedStats;
        }

        public void OnUpdate(float deltaTime)
        {
            float tempValue = _modifiedStats[StatTypes.MoveSpeed];
            
            if (tempValue < 0)
                tempValue = 0;
            
            _speed.Value = tempValue;
        }
    }
}