using UnityEditor;
using UnityEditor.SceneManagement;

namespace Game.Editor
{
    [InitializeOnLoad]
    public class EntryPointSceneAutoLoader
    {
        static EntryPointSceneAutoLoader()
        {
            if (EditorBuildSettings.scenes.Length == 0)
                return;
            
            EditorSceneManager.playModeStartScene = AssetDatabase
                .LoadAssetAtPath<SceneAsset>(EditorBuildSettings.scenes[0].path);
        }
    }
}