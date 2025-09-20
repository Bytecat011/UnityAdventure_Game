using Game.UI.CommonViews;
using Game.UI.Core;
using Game.UI.Gameplay.HealthDisplay;
using UnityEngine;

namespace Game.UI.Gameplay
{
    public class GameplayScreenView : MonoBehaviour, IView
    {
        [field: SerializeField] public IconTextView StageNumberView { get; private set; }
        [field: SerializeField] public EntitiesHealthDisplay EntitiesHealthDisplay { get; private set; }
    }
}