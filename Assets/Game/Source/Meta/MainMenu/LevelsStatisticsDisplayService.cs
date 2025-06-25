using System.Collections;
using Game.Meta.Features.LevelStatistics;
using Game.Meta.Features.Resources;
using Game.Utility.CoroutineManagement;
using UnityEngine;

namespace Game.Meta.MainMenu
{
    public class LevelsStatisticsDisplayService
    {
        private const KeyCode ShowStatisticsKeyCode = KeyCode.S;
        
        private readonly ICoroutineRunner _coroutineRunner;
        private readonly LevelStatisticsService _levelStatistics;

        private Coroutine _handleInputTsk;

        public LevelsStatisticsDisplayService(
            ICoroutineRunner coroutineRunner,
            LevelStatisticsService levelStatistics)
        {
            _coroutineRunner = coroutineRunner;
            _levelStatistics = levelStatistics;
        }

        public void Start()
        {
            _handleInputTsk = _coroutineRunner.StartTask(HandleInputTask());
        }

        private IEnumerator HandleInputTask()
        {
            Debug.Log($"You can see your level statistics by press {ShowStatisticsKeyCode} key");
            
            while (true)
            {
                if (Input.GetKeyDown(ShowStatisticsKeyCode))
                    Debug.Log($"Your statistics: {_levelStatistics.WonCount} win and {_levelStatistics.LostCount} lose");
                yield return null;
            }
        }

        public void Dispose()
        {
            if (_handleInputTsk != null)
                _coroutineRunner.StopTask(_handleInputTsk);
        }
    }
}