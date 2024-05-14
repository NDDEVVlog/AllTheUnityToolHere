using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(EnumScriptableObject), true)]
public class EnumScriptableObjectEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        EnumScriptableObject enumScriptableObject = (EnumScriptableObject)target;

        GUILayout.Space(10);

        if (GUILayout.Button("Load Enum"))
        {
            enumScriptableObject.FillString();
            EditorUtility.SetDirty(enumScriptableObject);
        }

        if (GUILayout.Button("Generate Enum"))
        {
            enumScriptableObject.FillEnum();
            EditorUtility.SetDirty(enumScriptableObject);
        }
        if (GUILayout.Button("Toggle Static Enum Field"))
        {
            enumScriptableObject.ToggleStaticEnumField();
            EditorUtility.SetDirty(enumScriptableObject);
        }
    }
}
