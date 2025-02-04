using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using System;
using System.Globalization;

public class ColoredFoldoutGroupAttribute : PropertyGroupAttribute
{
    public float R, G, B, A;

    // Existing constructor for RGBA values
    public ColoredFoldoutGroupAttribute(string path)
        : base(path)
    {
    }

    public ColoredFoldoutGroupAttribute(string path, float r, float g, float b, float a = 1f)
        : base(path)
    {
        this.R = r;
        this.G = g;
        this.B = b;
        this.A = a;
    }

    // New constructor for hex color codes
    public ColoredFoldoutGroupAttribute(string path, string hexColor)
        : base(path)
    {
        Color color = HexToColor(hexColor);  // Convert hex to Color
        this.R = color.r;
        this.G = color.g;
        this.B = color.b;
        this.A = color.a;
    }

    // Method to convert hex color code to Unity Color
    private Color HexToColor(string hex)
    {
        // Remove the hash if present
        hex = hex.Replace("#", "");

        // Parse depending on the length of the string
        if (hex.Length == 6) // RGB hex code (without alpha)
        {
            hex += "FF"; // Add alpha if it's missing
        }

        if (hex.Length != 8)
        {
            throw new ArgumentException($"Invalid hex color code: {hex}");
        }

        // Convert the hex string to individual color components
        byte r = byte.Parse(hex.Substring(0, 2), NumberStyles.HexNumber);
        byte g = byte.Parse(hex.Substring(2, 2), NumberStyles.HexNumber);  // Fix this part
        byte b = byte.Parse(hex.Substring(4, 2), NumberStyles.HexNumber);
        byte a = byte.Parse(hex.Substring(6, 2), NumberStyles.HexNumber);

        return new Color32(r, g, b, a); // Return as Color32 to include alpha support
    }


    protected override void CombineValuesWith(PropertyGroupAttribute other)
    {
        var otherAttr = (ColoredFoldoutGroupAttribute)other;

        this.R = Math.Max(otherAttr.R, this.R);
        this.G = Math.Max(otherAttr.G, this.G);
        this.B = Math.Max(otherAttr.B, this.B);
        this.A = Math.Max(otherAttr.A, this.A);
    }
}
