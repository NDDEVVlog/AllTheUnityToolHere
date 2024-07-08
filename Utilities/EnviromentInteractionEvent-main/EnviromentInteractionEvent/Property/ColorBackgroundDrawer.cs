using UnityEditor;
using UnityEngine;
using System.Collections.Generic;

[CustomPropertyDrawer(typeof(ColorBackgroundAttribute))]
public class ColorBackgroundDrawer : PropertyDrawer
{
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        ColorBackgroundAttribute colorBackground = attribute as ColorBackgroundAttribute;

        EditorGUI.BeginProperty(position, label, property);

        bool isStruct = property.propertyType == SerializedPropertyType.Generic;
        bool isArray = property.isArray;
        bool isList = IsList(property);

        // Set background color for the property if it's a struct, array, or list
        if (isStruct || isArray || isList)
        {
            EditorGUI.DrawRect(position, colorBackground.BackgroundColor);

            // Get the iterator for the elements
            SerializedProperty iterator = property.Copy();
            Rect elementRect = new Rect(position.x, position.y, position.width, EditorGUIUtility.singleLineHeight);

            // If it's an array or list, iterate over each element
            if (isArray || isList)
            {
                for (int i = 0; i < property.arraySize; i++)
                {
                    SerializedProperty elementProperty = property.GetArrayElementAtIndex(i);
                    DrawElement(elementProperty, elementRect, label);
                    elementRect.y += EditorGUI.GetPropertyHeight(elementProperty, GUIContent.none) + EditorGUIUtility.standardVerticalSpacing;
                }
            }
            // If it's a struct, just draw the struct itself
            else if (isStruct)
            {
                EditorGUI.PropertyField(position, property, label, true);
            }
        }
        else
        {
            // Draw the property field for non-struct variables
            EditorGUI.DrawRect(position, colorBackground.BackgroundColor);
            EditorGUI.PropertyField(position, property, label);
        }

        EditorGUI.EndProperty();
    }

    void DrawElement(SerializedProperty elementProperty, Rect elementRect, GUIContent label)
    {
        // Draw the foldout label for the struct
        elementProperty.isExpanded = EditorGUI.Foldout(elementRect, elementProperty.isExpanded, label, true);

        if (elementProperty.isExpanded)
        {
            // Indent the content of the struct
            EditorGUI.indentLevel++;
            Rect contentRect = EditorGUI.IndentedRect(elementRect);

            // Move to the first child property of the struct
            SerializedProperty childProperty = elementProperty.Copy();
            childProperty.Next(true);

            // Draw each child property of the struct
            float height = EditorGUI.GetPropertyHeight(childProperty, GUIContent.none);
            Rect childRect = new Rect(contentRect.x, contentRect.y + EditorGUIUtility.standardVerticalSpacing, contentRect.width, height);
            do
            {
                EditorGUI.PropertyField(childRect, childProperty, true);
                childRect.y += height + EditorGUIUtility.standardVerticalSpacing;
            }
            while (childProperty.NextVisible(false));

            EditorGUI.indentLevel--;
        }
    }

    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        bool isStruct = property.propertyType == SerializedPropertyType.Generic;
        bool isArray = property.isArray;
        bool isList = IsList(property);

        if (isStruct || isArray || isList)
        {
            float totalHeight = 0f;
            if (isArray || isList)
            {
                for (int i = 0; i < property.arraySize; i++)
                {
                    SerializedProperty elementProperty = property.GetArrayElementAtIndex(i);
                    totalHeight += EditorGUI.GetPropertyHeight(elementProperty, GUIContent.none) + EditorGUIUtility.standardVerticalSpacing;
                }
            }
            else if (isStruct)
            {
                return EditorGUI.GetPropertyHeight(property, label);
            }
            return totalHeight;
        }
        else
        {
            return base.GetPropertyHeight(property, label);
        }
    }

    // Check if the property is a list
    private bool IsList(SerializedProperty property)
    {
        return property.isArray && property.type.StartsWith("List");
    }
}
