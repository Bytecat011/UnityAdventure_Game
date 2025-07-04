using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using Game.Gameplay.EntitiesCore;
using UnityEditor;
using UnityEngine;

namespace Game.Editor
{
    public class EntityAPIGenerator
    {
        private const string AssemblyName = "Assembly-CSharp";

        private static string OutputPath
            => Path.Combine(Application.dataPath, "Game/Source/Gameplay/EntitiesCore/Generated/EntityAPI.cs");

        [InitializeOnLoadMethod]
        [MenuItem("Tools/GenerateEntityAPI")]
        private static void Generate()
        {
            
            string outputDir = Path.Combine(Application.dataPath, "Game/Source/Gameplay/EntitiesCore/Generated");

            if (!Directory.Exists(outputDir))
                Directory.CreateDirectory(outputDir);

            foreach (var file in Directory.GetFiles(outputDir, "Entity.*.cs"))
            {
                File.Delete(file);
            }
            
            Assembly assembly = Assembly.Load(AssemblyName);
            var componentTypes = getComponentTypesFrom(assembly);

            foreach (var componentType in componentTypes)
            {
                string typeName = componentType.Name;
                string fullTypeName = componentType.FullName;

                string componentName = RemoveSuffixIfExists(typeName, "Component");
                string modifiedComponentName = componentName + "C";

                var sb = new StringBuilder();

                // namespace
                sb.AppendLine($"namespace {typeof(Entity).Namespace}");
                sb.AppendLine("{");

                // partial class Entity
                sb.AppendLine($"\tpublic partial class {nameof(Entity)}");
                sb.AppendLine("\t{");

                // C-свойство
                sb.AppendLine($"\t\tpublic {fullTypeName} {modifiedComponentName} => GetComponent<{fullTypeName}>();");
                sb.AppendLine();

                // Value shortcut, если есть
                if (HasSingleField(componentType, out var field) && field.Name == "Value")
                {
                    sb.AppendLine(
                        $"\t\tpublic {GetValidTypeName(field.FieldType)} {componentName} => {modifiedComponentName}.{field.Name};");
                    sb.AppendLine();

                    if (HasEmptyConstructor(field.FieldType))
                    {
                        string initializer = "{" + field.Name + " = new " + GetValidTypeName(field.FieldType) + "() }";

                        sb.AppendLine($"\t\tpublic {typeof(Entity).FullName} Add{componentName}()");
                        sb.AppendLine("\t\t{");
                        sb.AppendLine($"\t\t\treturn AddComponent(new {fullTypeName}() {initializer});");
                        sb.AppendLine("\t\t}");
                        sb.AppendLine();
                    }
                }

                // AddX(params)
                string componentParameters = GetParameters(componentType);
                sb.AppendLine($"\t\tpublic {typeof(Entity).FullName} Add{componentName}({componentParameters})");
                sb.AppendLine("\t\t{");
                sb.AppendLine($"\t\t\treturn AddComponent(new {fullTypeName}() {GetInitializer(componentType)});");
                sb.AppendLine("\t\t}");
                sb.AppendLine();

                // Закрытие
                sb.AppendLine("\t}");
                sb.AppendLine("}");

                // Путь для файла
                string outputPath = Path.Combine(outputDir, $"Entity.{componentName}.cs");
                File.WriteAllText(outputPath, sb.ToString());
            }

            AssetDatabase.Refresh();
            AssetDatabase.SaveAssets();
        }

        private static bool HasEmptyConstructor(Type type)
        {
            return type.GetConstructor(Type.EmptyTypes) != null
                   && type.IsSubclassOf(typeof(UnityEngine.Object)) == false;
        }

        private static string GetParameters(Type type)
        {
            var fields = type.GetFields(BindingFlags.Public | BindingFlags.Instance);

            if (fields.Any() == false)
                return "";

            var parameters = fields
                .Select(f => $"{GetValidTypeName(f.FieldType)} {GetVariableNameFrom(f.Name)}");

            return string.Join(",", parameters);
        }

        private static string GetInitializer(Type type)
        {
            FieldInfo[] fields = type.GetFields(BindingFlags.Public | BindingFlags.Instance);


            if (fields.Any() == false)
                return "";

            var initializers = fields
                .Select(f => $"{f.Name} = {GetVariableNameFrom(f.Name)}");

            return "{" + string.Join(",", initializers) + "}";
        }

        private static string GetVariableNameFrom(string name) => char.ToLowerInvariant(name[0]) + name.Substring(1);

        private static string GetValidTypeName(Type type)
        {
            if (type == null)
                throw new ArgumentNullException(nameof(type));

            if (type.IsGenericType)
            {
                string typeName = type.GetGenericTypeDefinition().FullName;
                int backtickIndex = typeName.IndexOf('`');
                if (backtickIndex > 0)
                {
                    typeName = typeName.Substring(0, backtickIndex);
                }

                var genericArgs = type.GetGenericArguments();
                string genericArgsStr = string.Join(", ", genericArgs.Select(GetValidTypeName));

                return $"{typeName}<{genericArgsStr}>";
            }

            return type.FullName;
        }

        private static bool HasSingleField(Type type, out FieldInfo field)
        {
            var fields = type.GetFields(BindingFlags.Public | BindingFlags.Instance);

            if (fields.Length != 1)
            {
                field = null;
                return false;
            }

            field = fields[0];
            return true;
        }

        private static string RemoveSuffixIfExists(string str, string suffix)
        {
            if (str.EndsWith(suffix))
            {
                return str.Substring(0, str.Length - suffix.Length);
            }

            return str;
        }

        private static IEnumerable<Type> getComponentTypesFrom(Assembly assembly)
        {
            return assembly
                .GetTypes()
                .Where(type => type.IsInterface == false
                               && type.IsAbstract == false
                               && typeof(IEntityComponent).IsAssignableFrom(type));
        }
    }
}