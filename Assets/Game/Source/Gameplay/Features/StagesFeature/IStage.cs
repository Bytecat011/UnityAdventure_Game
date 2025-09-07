using System;
using Game.Utility.Reactive;

namespace Game.Gameplay.Features.StagesFeature
{
    public interface IStage : IDisposable
    {
        IReadOnlyEvent Completed { get; }

        void Start();
        void Update(float deltaTime);
        void Cleanup();
    }
}