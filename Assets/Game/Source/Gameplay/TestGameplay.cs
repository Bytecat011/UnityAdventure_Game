using System;
using Game.Core.DI;
using UnityEngine;

namespace Game.Gameplay
{
    public class TestGameplay : MonoBehaviour
    {
        private DIContainer _container;
        private bool _isRunning;

        public void Initialize(DIContainer container)
        {
            _container = container;
        }

        public void Run()
        {
            _isRunning = true;
        }

        private void Update()
        {
            if (_isRunning == false)
                return;
        }
    }
}