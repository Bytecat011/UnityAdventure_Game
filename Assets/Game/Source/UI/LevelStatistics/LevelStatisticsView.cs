using Game.UI.Core;
using TMPro;
using UnityEngine;

namespace Game.UI.LevelStatistics
{
    public class LevelStatisticsView : MonoBehaviour, IView
    {
        [SerializeField] private TMP_Text _winCountText;
        [SerializeField] private TMP_Text _loseCountText;
        
        public void SetWin(int winCount) => _winCountText.text = winCount.ToString();
        public void SetLose(int loseCount) => _loseCountText.text = loseCount.ToString();
    }
}