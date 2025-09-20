using Game.UI.CommonViews;
using UnityEngine;

namespace Game.UI.Gameplay.HealthDisplay
{
    public class EntitiesHealthDisplay : ElementsListView<BarWithText>
    {
        private Camera _camera;

        private void Awake()
        {
            _camera = Camera.main;
        }

        public void UpdatePositionsFor(BarWithText bar, Vector3 worldPosition)
        {
            Vector3 position = _camera.WorldToScreenPoint(worldPosition);
            
            bar.transform.position = position;
        }
    }
}