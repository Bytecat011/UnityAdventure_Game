using Game.Utility.Reactive;
using UnityEngine;

namespace Game.Gameplay.Features.Input
{
    public interface IInputService
    {
        bool IsEnabled { get; set; }
        
        void Update(float deltaTime);
        
        Vector3 Direction { get; }
        
        Vector2 MouseDelta { get; }
        
        ReactiveEvent Mouse1ClickedEvent { get; }
    }
}