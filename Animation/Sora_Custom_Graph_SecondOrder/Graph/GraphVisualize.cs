using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Sora_Ults;

public class GraphVisualize : MonoBehaviour
{
    public SelectedAlgorithm selectedAlgorithm;
    
    public bool PositionToggle = true;
    [Range(0f, 20f)] public float PosFrequency = 1f;
    [Range(0f, 2f)] public float PosDamping = 0.5f;
    [Range(-200f, 100f)] 
    public float PosResponsiveness = 0f;
    [Range(0, 1f)] public float PosDeltaTime = 0.3f;
    
    public bool RotationToggle = true;
    [Range(0f, 20f)] public float RotFrequency = 1f;
    [Range(0f, 2f)] public float RotDamping = 0.5f;
    [Range(-200f, 100f)] 
    public float RotResponsiveness = 0f;
    [Range(0, 1f)] public float RotDeltaTime = 0.3f;
    
    public bool ScaleToggle = true;
    [Range(0f, 20f)] public float ScaleFrequency = 1f;
    [Range(0f, 2f)] public float ScaleDamping = 0.5f;
    [Range(-200f, 100f)] 
    public float ScaleResponsiveness = 0f;
    [Range(0, 1f)] public float ScaleDeltaTime = 0.3f;

    public void UpdateOnGUI_Pos(float? frequency , float? damping , float? Responsiveness,float? DeltaTime,SelectedAlgorithm selectedAlgorithm, bool Toggle)
    {
        this.PositionToggle = Toggle;
        this.PosFrequency = frequency.GetValueOrDefault(this.PosFrequency);
        this.PosDamping = damping.GetValueOrDefault(this.PosDamping);
        this.PosResponsiveness = Responsiveness.GetValueOrDefault(this.PosResponsiveness);
        this.selectedAlgorithm = selectedAlgorithm;
        this.PosDeltaTime = DeltaTime.GetValueOrDefault(this.PosDeltaTime);
    }
    
    public void UpdateOnGUI_Rot(float? frequency , float? damping , float? Responsiveness,float? DeltaTime,SelectedAlgorithm selectedAlgorithm, bool Toggle)
    {
        this.RotationToggle = Toggle;
        this.RotFrequency = frequency.GetValueOrDefault(this.RotFrequency);
        this.RotDamping = damping.GetValueOrDefault(this.RotDamping);
        this.RotResponsiveness = Responsiveness.GetValueOrDefault(this.RotResponsiveness);
        this.selectedAlgorithm = selectedAlgorithm;
        this.RotDeltaTime = DeltaTime.GetValueOrDefault(this.RotDeltaTime);
    }
    
    public void UpdateOnGUI_Scale(float? frequency , float? damping , float? Responsiveness,float? DeltaTime,SelectedAlgorithm selectedAlgorithm, bool Toggle)
    {
        this.ScaleToggle = Toggle;
        this.ScaleFrequency = frequency.GetValueOrDefault(this.ScaleFrequency);
        this.ScaleDamping = damping.GetValueOrDefault(this.ScaleDamping);
        this.ScaleResponsiveness = Responsiveness.GetValueOrDefault(this.ScaleResponsiveness);
        this.selectedAlgorithm = selectedAlgorithm; 
        this.ScaleDeltaTime = DeltaTime.GetValueOrDefault(this.ScaleDeltaTime);
    }
}
