using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Game.Configs.Gameplay.Abilities
{
    [CreateAssetMenu(menuName = "Configs/Gameplay/Abilities/AbilitiesConfigsContainer", fileName = "AbilitiesConfigsContainer")]
    public class AbilitiesConfigsContainer : ScriptableObject
    {
        [SerializeField] private List<AbilityConfig> _abilityConfigs;
        
        public IReadOnlyList<AbilityConfig> AbilityConfigs => _abilityConfigs;
        
        public AbilityConfig GetConfigBy(string id) => _abilityConfigs.First(x => x.ID == id);
    }
}