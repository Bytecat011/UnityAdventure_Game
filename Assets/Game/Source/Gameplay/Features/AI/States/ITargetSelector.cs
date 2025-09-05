using System.Collections.Generic;
using System.Linq;
using Game.Gameplay.EntitiesCore;
using Game.Gameplay.Features.ApplyDamage;
using Game.Gameplay.Features.TeamsFeatures;
using Game.Utility.Conditions;
using Game.Utility.Reactive;
using UnityEngine;

namespace Game.Gameplay.Features.AI.States
{
    public interface ITargetSelector
    {
        Entity SelectTargetFrom(IEnumerable<Entity> targets);
    }

    public class NearestDamageableTargetSelector : ITargetSelector
    {
        private Entity _source;
        private Transform _sourceTransform;

        public NearestDamageableTargetSelector(Entity entity)
        {
            _source = entity;
            _sourceTransform = entity.Transform;
        }

        public Entity SelectTargetFrom(IEnumerable<Entity> targets)
        {
            var selectedTargets = targets.Where(target =>
            {
                bool result = target.HasComponent<TakeDamageRequest>();

                if (target.TryGetCanApplyDamage(out ICompositeCondition canApplyDamage))
                {
                    result = result && canApplyDamage.Evaluate();
                }

                if (_source.TryGetTeam(out ReactiveVariable<Teams> sourceTeam)
                    && target.TryGetTeam(out ReactiveVariable<Teams> targetTeam))
                {
                    result = result && (sourceTeam.Value != targetTeam.Value);
                }
                
                result = result && (target != _source);
                
                return result;
            });

            if (selectedTargets.Any() == false)
                return null;
            
            Entity closestTarget = null;
            float minDistance = float.MaxValue;

            foreach (var target in selectedTargets)
            {
                float distance = GetDistanceTo(target);
                if (distance < minDistance)
                {
                    minDistance = distance;
                    closestTarget = target;
                }
            }

            return closestTarget;
        }
        
        private float GetDistanceTo(Entity target) => (_sourceTransform.position - target.Transform.position).magnitude;
    }
}