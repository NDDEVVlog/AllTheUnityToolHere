using UnityEditor;
using UnityEngine;

[CustomPropertyDrawer(typeof(ColorAttribute))]
public class ColorAttributeDrawer : PropertyDrawer
{   
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        ColorAttribute colorAttribute = attribute as ColorAttribute;

        // Set color for the label
        GUI.color = colorAttribute.color;
        EditorGUI.PropertyField(position, property, label);
        GUI.color = Color.white;
    }
}
