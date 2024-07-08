using UnityEngine;
[System.AttributeUsage(System.AttributeTargets.Field, Inherited = true)]
public class ColorBackgroundAttribute : PropertyAttribute
{
    public Color BackgroundColor;

    public ColorBackgroundAttribute(float r, float g, float b, float a = 1f)
    {
        BackgroundColor = new Color(r, g, b, a);
    }
}
