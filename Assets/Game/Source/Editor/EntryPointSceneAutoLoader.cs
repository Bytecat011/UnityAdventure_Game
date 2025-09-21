using UnityEditor;
using UnityEditor.SceneManagement;

namespace Game.Editor
{
    [InitializeOnLoad]
    public class EntryPointSceneAutoLoader
    {
        private const string MenuPath = "PlayFromBootstrap/Enabled";
        private const string PlayFromBootstrapKey = "PlayFromBootstrapKey";
        
        static EntryPointSceneAutoLoader()
        {
            EditorApplication.playModeStateChanged += OnPLayModeStateChanged;
        }

        private static void OnPLayModeStateChanged(PlayModeStateChange state)
        {
            if (state == PlayModeStateChange.ExitingPlayMode)
            {
                if (EditorPrefs.GetBool(PlayFromBootstrapKey) == false)
                {
                    EditorSceneManager.playModeStartScene = null;
                    return;
                }
                
                if (EditorBuildSettings.scenes.Length == 0)
                    return;

                EditorSceneManager.playModeStartScene = AssetDatabase
                    .LoadAssetAtPath<SceneAsset>(EditorBuildSettings.scenes[0].path);
            }
        }

        [MenuItem(MenuPath)]
        private static void Toggle()
        {
            bool result = EditorPrefs.GetBool(PlayFromBootstrapKey);
            EditorPrefs.SetBool(PlayFromBootstrapKey, !result);
        }
        
        [MenuItem(MenuPath, true)]
        private static bool ToggleValidate()
        {
            Menu.SetChecked(MenuPath, EditorPrefs.GetBool(PlayFromBootstrapKey));
            return true;
        }
    }
}