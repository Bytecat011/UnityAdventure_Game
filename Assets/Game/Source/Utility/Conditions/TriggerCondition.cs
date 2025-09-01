using System;
using Game.Utility.Reactive;

namespace Game.Utility.Conditions
{
    public class TriggerCondition : ICondition, IDisposable
    {
        private IDisposable _subscription;

        private bool isEventTriggered;

        public TriggerCondition(ReactiveEvent triggerEvent)
        {
            _subscription = triggerEvent.Subscribe(OnTrigger);
        }

        private void OnTrigger()
        {
            isEventTriggered = true;
        }

        public bool Evaluate()
        {
            if (isEventTriggered)
            {
                isEventTriggered = false;
                return true;
            }

            return false;
        }

        public void Dispose()
        {
            _subscription?.Dispose();
        }
    }
}