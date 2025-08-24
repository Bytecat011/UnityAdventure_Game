using System.Collections.Generic;
using System.Linq;
using Game.Gameplay.EntitiesCore;
using Game.Gameplay.Features.ApplyDamage;
using Game.Gameplay.Features.LifeCycle;
using Game.Utility.Conditions;
using UnityEngine;

namespace Game.Gameplay.Features.AI.States
{
    public class LowestHealthInRangeTargetSelector : ITargetSelector
    {
        private Entity _source;
        private Transform _sourceTransform;

        public LowestHealthInRangeTargetSelector(Entity entity)
        {
            _source = entity;
            _sourceTransform = entity.Transform;
        }

        public Entity SelectTargetFrom(IEnumerable<Entity> targets)
        {
            var selectedTargets = targets.Where(target =>
            {
                bool result = target.HasComponent<TakeDamageRequest>() && target.HasComponent<CurrentHealth>();

                if (target.TryGetCanApplyDamage(out ICompositeCondition canApplyDamage))
                {
                    result = result && canApplyDamage.Evaluate();
                }

                result = result && (target != _source);
                
                return result;
            });

            if (selectedTargets.Any() == false)
                return null;
            
            Entity lowestHealthTarget = null;
            float minHealth = float.MaxValue;

            foreach (var target in selectedTargets)
            {
                float health = target.CurrentHealth.Value;
                if (health < minHealth)
                {
                    minHealth = health;
                    lowestHealthTarget = target;
                }
            }

            return lowestHealthTarget;
        }
        
        private float GetDistanceTo(Entity target) => (_sourceTransform.position - target.Transform.position).magnitude;
    }
}