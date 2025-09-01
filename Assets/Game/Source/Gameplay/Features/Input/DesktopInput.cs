using Game.Utility.Reactive;
using UnityEngine;

namespace Game.Gameplay.Features.Input
{
    public class DesktopInput : IInputService
    {
        private const string HorizontalAxisName = "Horizontal";
        private const string VerticalAxisName = "Vertical";

        private Vector2 _mousePosition;
        
        public bool IsEnabled { get; set; } = true;

        public void Update(float deltaTime)
        {
            UpdateMouseDelta();

            if (UnityEngine.Input.GetMouseButtonDown(0))
            {
                Mouse1ClickedEvent.Notify();
            }
        }

        private void UpdateMouseDelta()
        {
            var mousePosition = new Vector2(UnityEngine.Input.mousePosition.x, UnityEngine.Input.mousePosition.y);
            MouseDelta = mousePosition - _mousePosition;
            _mousePosition = mousePosition;
        }

        public Vector3 Direction {
            get
            {
                if (IsEnabled == false)
                    return Vector3.zero;
                
                return new Vector3(UnityEngine.Input.GetAxisRaw(HorizontalAxisName), 0, UnityEngine.Input.GetAxisRaw(VerticalAxisName));
            }
        }

        public Vector2 MouseDelta { get; private set; }
        public float MouseSensitivity { get; set; }

        public ReactiveEvent Mouse1ClickedEvent { get; } = new ReactiveEvent();
    }
}