using Game.Gameplay.Features.TeamsFeatures;
using Game.Utility.Reactive;

namespace Game.Gameplay.EntitiesCore
{
    public static class EntitiesHelper
    {
        public static bool TryTakeDamageFrom(Entity source, Entity damageable, float damage)
        {
            if (damageable.TryGetTakeDamageRequest(out ReactiveEvent<float> takeDamegeRequest) == false)
                return false;

            if (source.TryGetTeam(out ReactiveVariable<Teams> sourceTeam)
                && damageable.TryGetTeam(out ReactiveVariable<Teams> damageableTeam))
            {
                if (damageableTeam.Value == sourceTeam.Value)
                    return false;
            }
            
            takeDamegeRequest.Notify(damage);
            return true;
        }
    }
}