using System;
using System.Collections.Generic;

namespace Game.Gameplay.Features.Abilities
{
    public class AbilitiesList
    {
        public event Action<Ability> Added;
        
        private List<Ability> _elements = new();
        
        public IReadOnlyList<Ability> Elements => _elements;

        public virtual void Add(Ability ability)
        {
            _elements.Add(ability);
            Added?.Invoke(ability);
        }
    }
}