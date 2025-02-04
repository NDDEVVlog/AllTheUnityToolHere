#if UNITY_EDITOR
using Sirenix.OdinInspector.Editor;
using Sirenix.Utilities.Editor;
using UnityEngine;
using UnityEditor;

public class ColoredFoldoutGroupAttributeDrawer : OdinGroupDrawer<ColoredFoldoutGroupAttribute>
{
    // Persistent foldout state
    private LocalPersistentContext<bool> isExpanded;

    // Persistent color context for the color picker
    private LocalPersistentContext<Color> foldoutColor;

    protected override void Initialize()
    {
        // Initialize the persistent foldout state (expand/collapse)
        this.isExpanded = this.GetPersistentValue<bool>(
            "ColoredFoldoutGroupAttributeDrawer.isExpanded",
            GeneralDrawerConfig.Instance.ExpandFoldoutByDefault);

        // Load the saved color from persistent storage or set the default attribute color
        Color defaultColor = new Color(this.Attribute.R, this.Attribute.G, this.Attribute.B, this.Attribute.A);
        this.foldoutColor = this.GetPersistentValue<Color>("ColoredFoldoutGroupAttributeDrawer.foldoutColor", defaultColor);

        // Initialize the foldout color from the persistent context
        this.Attribute.R = this.foldoutColor.Value.r;
        this.Attribute.G = this.foldoutColor.Value.g;
        this.Attribute.B = this.foldoutColor.Value.b;
        this.Attribute.A = this.foldoutColor.Value.a;
    }

    protected override void DrawPropertyLayout(GUIContent label)
    {
        // Push the custom color for the foldout header
        GUIHelper.PushColor(new Color(this.Attribute.R, this.Attribute.G, this.Attribute.B, this.Attribute.A));
        SirenixEditorGUI.BeginBox(); // Begin drawing the foldout box
        SirenixEditorGUI.BeginBoxHeader(); // Begin header section
        GUIHelper.PopColor(); // Revert the color to default after the header

        // Begin the horizontal layout for foldout label and color box
        EditorGUILayout.BeginHorizontal();

        // Draw foldout control
        this.isExpanded.Value = SirenixEditorGUI.Foldout(this.isExpanded.Value, label);

        // Draw an interactive color box next to the foldout label
        Color currentColor = foldoutColor.Value; // Load from persistent context
        currentColor = EditorGUILayout.ColorField(currentColor, GUILayout.Width(50)); // Interactive color box

        // Update the attribute color values based on the user's input
        this.Attribute.R = currentColor.r;
        this.Attribute.G = currentColor.g;
        this.Attribute.B = currentColor.b;
        this.Attribute.A = currentColor.a;

        // Save the updated color to the persistent context
        foldoutColor.Value = currentColor;

        EditorGUILayout.EndHorizontal(); // End horizontal layout

        SirenixEditorGUI.EndBoxHeader(); // End header section

        // If the foldout is expanded, draw child properties
        if (SirenixEditorGUI.BeginFadeGroup(this, this.isExpanded.Value))
        {
            EditorGUILayout.BeginVertical(EditorStyles.helpBox);

            for (int i = 0; i < this.Property.Children.Count; i++)
            {
                this.Property.Children[i].Draw();
            }

            EditorGUILayout.EndVertical();
        }

        SirenixEditorGUI.EndFadeGroup();
        SirenixEditorGUI.EndBox(); // End the foldout box
    }
}
#endif
