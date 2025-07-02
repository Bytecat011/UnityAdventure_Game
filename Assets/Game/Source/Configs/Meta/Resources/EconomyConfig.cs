using UnityEngine;

namespace Game.Configs.Meta.Resources
{
    [CreateAssetMenu(menuName = "Configs/Meta/Resources/EconomyConfig", fileName = "EconomyConfig")]
    public class EconomyConfig : ScriptableObject
    {
        [field: SerializeField] public int GainGoldAmountForWin { get; private set; } = 10;
        [field: SerializeField] public int LoseGoldAmountForLose { get; private set; } = 5;
        [field: SerializeField] public int ResetStatisticsGoldCost { get; private set; } = 20;
    }
}