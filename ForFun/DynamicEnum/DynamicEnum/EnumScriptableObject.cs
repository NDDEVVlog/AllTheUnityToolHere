using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnumScriptableObject : ScriptableObject
{
    public abstract void FillString();
    public abstract void FillEnum();
    public abstract void ToggleStaticEnumField();

}
