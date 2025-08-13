using System;
using System.Collections;
using Game.Utility.CoroutineManagement;
using Game.Utility.Reactive;
using UnityEngine;

namespace Game.Utility.Timer
{
    public class TimerService : IDisposable
    {
        private readonly float _cooldown;

        private readonly ReactiveEvent _cooldownEnded = new();
        private readonly ReactiveVariable<float> _currentTime = new();
        
        private readonly ICoroutineRunner _coroutineRunner;
        private Coroutine _cooldownProcess;

        public TimerService(
            float cooldown,
            ICoroutineRunner coroutineRunner)
        {
            _cooldown = cooldown;
            _coroutineRunner = coroutineRunner;
        }

        public IReadOnlyEvent CooldownEnded => _cooldownEnded;
        public IReactiveVariable<float> CurrentTime => _currentTime;

        public bool IsOver => _currentTime.Value <= 0f;
        
        public void Dispose()
        {
            Stop();
        }
        
        public void Stop()
        {
            if (_cooldownProcess != null)
                _coroutineRunner.StopTask(_cooldownProcess);
        }

        public void Restart()
        {
            Stop();

            _cooldownProcess = _coroutineRunner.StartTask(CooldownProcess());
        }

        private IEnumerator CooldownProcess()
        {
            _currentTime.Value = _cooldown;

            while (IsOver == false)
            {
                _currentTime.Value -= Time.deltaTime;
                yield return null;
            }
            
            _cooldownEnded?.Notify();
        }
    }
}