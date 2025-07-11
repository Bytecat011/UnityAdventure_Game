using System.Collections.Generic;
using Game.UI.Core;
using UnityEngine;

namespace Game.UI.CommonViews
{
    public class ElementsListView<TElement> : MonoBehaviour, IView where TElement : MonoBehaviour, IView
    {
        [SerializeField] private Transform _parent;
        
        private readonly List<TElement> _elements = new();
        
        public IReadOnlyList<TElement> Elements => _elements;

        public void Add(TElement element)
        {
            element.transform.SetParent(_parent);
            _elements.Add(element);
        }

        public void Remove(TElement element)
        {
            element.transform.SetParent(null);
            _elements.Remove(element);
        }
    }
}