using UnityEngine;

namespace Game.Gameplay.Features.Pause
{
    public class TimeScalePauseService : IPauseService
    {
        public bool IsPaused { get; private set; }
        
        public void Pause()
        {
           Time.timeScale = 0;
           IsPaused = true;
        }

        public void Unpause()
        {
            Time.timeScale = 1;
            IsPaused = false;
        }
    }
}