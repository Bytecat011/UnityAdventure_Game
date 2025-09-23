using Game.Gameplay.EntitiesCore;
using Game.Gameplay.EntitiesCore.Systems;

namespace Game.Gameplay.Features.Abilities
{
    public class AbilityOnAddActivatorSystem : IInitializableSystem, IDisposableSystem
    {
        private AbilitiesList _abilitiesList;

        public void OnInit(Entity entity)
        {
            _abilitiesList = entity.Abilities;

            _abilitiesList.Added += OnAbilityAdded;

            foreach (var ability in _abilitiesList.Elements)
            {
                ability.Activate();
            }
        }

        private void OnAbilityAdded(Ability ability)
        {
            ability.Activate();
        }

        public void OnDispose()
        {
            _abilitiesList.Added -= OnAbilityAdded;
        }
    }
}