using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using System.IO;

[CreateAssetMenu(menuName = "NDTool/DynamicEnum/EnumGenerater")]
public class GenerateScript : ScriptableObject
{
    public string scriptName = "ScriptName";
    public string enumName = "EnumName";

    public void Generate()
    {
        GeneratorScript(scriptName, enumName);
    }

    public void GeneratorScript(string scriptName, string enumName)
    {
        string scriptContent = $@"
using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CreateAssetMenu(menuName = ""NDTool/DynamicEnum/EnumList/{scriptName}"")]
public class {scriptName} : EnumScriptableObject
{{
    public List<string> {scriptName.ToLower()}String;
    public bool isStatic = false;
    public enum {enumName}
    {{
        // Add your enum values here
    }}

    public {enumName} {scriptName.ToLower()};
    






    public override void FillString()
    {{
        GenerateEnum.FillStringEnumList({scriptName.ToLower()}String, typeof({enumName}));
    }}

    public override void FillEnum()
    {{
        GenerateEnum.FillEnum(this, typeof({enumName}), {scriptName.ToLower()}String);
    }}

    public override void ToggleStaticEnumField()
    {{
        GenerateEnum.ToggleStaticEnumField(this, typeof({enumName}), nameof({scriptName.ToLower()}), ref isStatic);
    }}
}}
";

        string filePath = $"Assets/Enums/{scriptName}.cs";

        // Write the script content to a new file
        File.WriteAllText(filePath, scriptContent);

        // Refresh the AssetDatabase to reflect the changes in the Unity Editor
        AssetDatabase.Refresh();
        Debug.Log($"Script '{scriptName}.cs' created at: {filePath}");
        // Create the asset file
        CreateAsset(scriptName, filePath);

       
    }

    private void CreateAsset(string scriptName, string scriptPath)
    {
        // Instantiate the scriptable object
        ScriptableObject scriptableObject = ScriptableObject.CreateInstance(scriptName);

        // Create the asset file
        string assetPath = $"Assets/Enums/{scriptName}.asset";
        AssetDatabase.CreateAsset(scriptableObject, assetPath);

        // Refresh the AssetDatabase
        AssetDatabase.Refresh();

        Debug.Log($"Asset '{scriptName}.asset' created at: {assetPath}");
    }
}
