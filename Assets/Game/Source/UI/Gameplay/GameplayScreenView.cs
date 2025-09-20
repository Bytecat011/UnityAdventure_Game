using Game.UI.CommonViews;
using Game.UI.Core;
using UnityEngine;

namespace Game.UI.Gameplay
{
    public class GameplayScreenView : MonoBehaviour, IView
    {
        [field: SerializeField] public IconTextView StageNumberView { get; private set; }
    }
}