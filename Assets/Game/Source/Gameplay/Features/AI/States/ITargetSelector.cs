using System.Collections.Generic;
using Game.Gameplay.EntitiesCore;

namespace Game.Gameplay.Features.AI.States
{
    public interface ITargetSelector
    {
        Entity SelectTargetFrom(IEnumerable<Entity> targets);
    }
}