using UnityEditor;
using UnityEngine;
using Sirenix.OdinInspector;

[CustomEditor(typeof(GenerateScript))]
public class GenerateScriptEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        GenerateScript generateScript = (GenerateScript)target;

        GUILayout.Space(10);



        GUILayout.Space(10);

        // Display the "Generate" button
        if (GUILayout.Button("Generate"))
        {
            generateScript.Generate();
        }
    }
}
