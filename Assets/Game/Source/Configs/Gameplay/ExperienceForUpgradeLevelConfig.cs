using System.Collections.Generic;
using UnityEngine;

namespace Game.Configs.Gameplay
{
    [CreateAssetMenu(fileName = "ExperienceForUpgradeLevelConfig", menuName = "Configs/Gameplay/ExperienceForUpgradeLevelConfig")]
    public class ExperienceForUpgradeLevelConfig : ScriptableObject
    {
        [SerializeField] private List<float> _experienceForLevel;
        
        public int MaxLevel => _experienceForLevel.Count;
        
        public float GetExperienceFor(int level) => _experienceForLevel[level - 1];
    }
}