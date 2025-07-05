using System.IO;
using System.Text;
using UnityEditor;
using UnityEngine;

namespace Game.Editor
{
    public class UnityLayersGenerator
    {
        private static string OutputPath
            => Path.Combine(Application.dataPath, "Game/Source/Utility/Generated/UnityLayers.cs");
        
        [InitializeOnLoadMethod]
        [MenuItem("Tools/Generate UnityLayers")]
        public static void Generate()
        {
            var sb = new StringBuilder();

            sb.AppendLine($"using UnityEngine;");
            sb.AppendLine();
            
            sb.AppendLine($"namespace Game.Utility");
            sb.AppendLine("{");
            
            sb.AppendLine("\tpublic static class UnityLayers");
            sb.AppendLine("\t{");

            for (int i = 0; i <= 31; i++)
            {
                string layerName = LayerMask.LayerToName(i);
                if (string.IsNullOrEmpty(layerName))
                    continue;

                string safeName = MakeSafeName(layerName);
                
                sb.AppendLine($"\t\tpublic static readonly int Layer{safeName} = LayerMask.NameToLayer(\"{layerName}\");");
            }

            sb.AppendLine();

            for (int i = 0; i <= 31; i++)
            {
                string layerName = LayerMask.LayerToName(i);
                if (string.IsNullOrEmpty(layerName))
                    continue;

                string safeName = MakeSafeName(layerName);
                
                sb.AppendLine($"\t\tpublic static readonly int LayerMask{safeName} = 1 << Layer{safeName};");
            }

            sb.AppendLine("\t}");
            sb.AppendLine("}");

            if (!Directory.Exists(OutputPath))
                Directory.CreateDirectory(Path.GetDirectoryName(OutputPath));
            File.WriteAllText(OutputPath, sb.ToString());

            AssetDatabase.Refresh();
            AssetDatabase.SaveAssets();
        }

        private static string MakeSafeName(string layerName)
        {
            var sb = new StringBuilder();
            foreach (char c in layerName)
            {
                if (char.IsLetterOrDigit(c))
                    sb.Append(c);
            }

            return sb.ToString();
        }
    }
}