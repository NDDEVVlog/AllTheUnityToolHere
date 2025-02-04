# Colored Foldout Group for Odin Inspector

**Author: NDDEVGAME**
**Origin link :https://github.com/NDDEVVlog/ColoredFoldGroupAttribute**

A customizable foldout group for Odin Inspector that allows you to set custom colors for foldout headers, including hex color support and a persistent color picker.

## Features
- 🎨 Custom Colors: Set RGBA values or use a hex color code.
- 🔄 Persistent State: The foldout remembers its expansion state.
- 🖌️ Color Picker: Easily adjust colors in the Unity Inspector.
- ⚡ Seamless Integration: Works flawlessly with Odin Inspector.

## Installation

1. Ensure you have **Odin Inspector** installed in your Unity project.
2. Copy the `ColoredFoldoutGroupAttribute.cs` and `ColoredFoldoutGroupAttributeDrawer.cs` scripts into your Unity project (inside an `Editor` folder for the drawer script).
3. Use `[ColoredFoldoutGroup("Group Name", "#FF5733")]` in your scripts.

## Usage

```csharp
using UnityEngine;
using Sirenix.OdinInspector;

public class ExampleScript : MonoBehaviour
{
    [ColoredFoldoutGroup("Settings", "#3498db")]
    public float speed;

    [ColoredFoldoutGroup("Settings", "#3498db")]
    public int power;

    [ColoredFoldoutGroup("Advanced", "#2ecc71")]
    public bool isEnabled;
}
```
| Parameter | Type | Description |
| :---         |     :---:      |          ---: |
| **path**   | **string**  | Name of the foldout group    |
| **r, g, b, a**     | **float**  | Color values (0-1) for red, green, blue, alpha    |
| **hexColor**     | **string** |  Hex color string (e.g., #FF5733)   |

## Example
![image](https://github.com/user-attachments/assets/2e9be7ef-b8b2-48fd-845b-0d911da4ad1e)


## Contributions
Pull requests and improvements are welcome! 🚀
## Contact
For issues or suggestions, open a GitHub Issue or reach out via email.

⭐ If you find this useful, please star this repo! ⭐
