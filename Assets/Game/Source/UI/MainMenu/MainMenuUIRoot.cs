using UnityEngine;

namespace Game.UI.MainMenu
{
    public class MainMenuUIRoot : MonoBehaviour
    {
        [field: SerializeField] public Transform HUDLayer { get; private set; }
        [field: SerializeField] public Transform PopupsLayer { get; private set; }
        [field: SerializeField] public Transform VFXUnderPopupsLayer { get; private set; }
        [field: SerializeField] public Transform VFXOverPopupsLayer { get; private set; }
    }
}