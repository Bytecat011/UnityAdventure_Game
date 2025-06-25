using UnityEngine;

namespace Game.Configs
{
    [CreateAssetMenu(menuName = "Configs/Meta/Resources/EconomyConfig", fileName = "EconomyConfig")]
    public class EconomyConfig : ScriptableObject
    {
        [field: SerializeField] public int StartGoldAmount { get; private set; } = 100;
        [field: SerializeField] public int GainGoldAmountForWin { get; private set; } = 10;
        [field: SerializeField] public int LoseGoldAmountForLose { get; private set; } = 5;
        [field: SerializeField] public int ResetStatisticsCost { get; private set; } = 20;
    }
}