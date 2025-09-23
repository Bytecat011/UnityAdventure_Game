using System.Collections.Generic;
using System.Linq;
using Game.Configs.Gameplay.Abilities;
using Game.Gameplay.EntitiesCore;

namespace Game.Gameplay.Features.AbilityDropping
{
    public class AbilityDropService
    {
        private readonly AbilitiesConfigsContainer _abilitiesConfigsContainer;
        private readonly AbilityDroppingRulesService _abilityDroppingRules;

        public AbilityDropService(
            AbilitiesConfigsContainer abilitiesConfigsContainer, 
            AbilityDroppingRulesService abilityDroppingRules)
        {
            _abilitiesConfigsContainer = abilitiesConfigsContainer;
            _abilityDroppingRules = abilityDroppingRules;
        }

        public List<AbilityConfig> Drop(int count, Entity entity)
        {
            List<AbilityConfig> availableAbilities 
                = new List<AbilityConfig>(_abilitiesConfigsContainer
                    .AbilityConfigs
                    .Where(abilityOption => _abilityDroppingRules.IsAvailable(abilityOption, entity)));
            
            List<AbilityConfig> selectedAbilities = new();

            for (int i = 0; i < count; i++)
            {
                var selectedAbility = availableAbilities[UnityEngine.Random.Range(0, availableAbilities.Count)];
                selectedAbilities.Add(selectedAbility);
                availableAbilities.Remove(selectedAbility);
            }
            
            return selectedAbilities;
        }
    }
}