using System;
using NaughtyAttributes;
using UnityEngine;
using Sora_Ults;
using Unity.Collections;

public class SO_Transform : MonoBehaviour
{
    public SelectedAlgorithm ASelector;

    [Foldout("Position")] 
    public bool PositionToggle = true;
    [Foldout("Position")]
    [SerializeField][Range(0,10)] public float PosFrequency = 1f;
    [Foldout("Position")]
    [SerializeField][Range(0,1)] public float PosDamping = 1f;
    [Foldout("Position")]
    [SerializeField] public float PosResponsiveness = 0.2f;
    [Foldout("Position")]
    [SerializeField] public float PosDeltaTimeScale = 17f;
    
    [Space(5)]
    [Foldout("Rotation")]
    public bool RotationToggle = true;
    [Foldout("Rotation")]
    [SerializeField][Range(0,10)] public float RotFrequency = 1f;
    [Foldout("Rotation")]
    [SerializeField][Range(0,1)] public float RotDamping = 1f;
    [Foldout("Rotation")]
    [SerializeField] public float RotResponsiveness = 0.2f;
    [Foldout("Rotation")]
    [SerializeField] public float RotDeltaTimeScale = 17f;
    
    [Space(5)]
    [Foldout("Scale")]
    public bool ScaleToggle = true;
    [Foldout("Scale")]
    [SerializeField][Range(0,10)] public float ScaleFrequency = 1f;
    [Foldout("Scale")]
    [SerializeField][Range(0,1)] public float ScaleDamping = 1f;
    [Foldout("Scale")]
    [SerializeField] public float ScaleResponsiveness = 0.2f;
    [Foldout("Scale")]
    [SerializeField] public float ScaleDeltaTimeScale = 17f;
    
    [Space(10)]
    [SerializeField] private Transform target;
    [Space(10)]
    public GraphVisualize GV;
    private SO_Position_Handler positionHandler = new SO_Position_Handler();
    private SO_Quaternion_Handler rotationHandler = new SO_Quaternion_Handler();
    private SO_Scale_Handler scaleHandler = new SO_Scale_Handler();
    
    private Vector3 initialPosition;
    private Quaternion initialRotation;
    private Vector3 initialScale;
    private void Awake()
    {
        GV = this.gameObject.GetComponent<GraphVisualize>();
        
        initialPosition = transform.position;
        initialRotation = transform.rotation;
        initialScale = transform.localScale;
        positionHandler.Initialize(PosFrequency, PosDamping, PosResponsiveness, PosDeltaTimeScale, initialPosition, ASelector,target.transform.position);
        rotationHandler.Initialize(RotFrequency, RotDamping, RotResponsiveness, RotDeltaTimeScale, initialRotation, ASelector, target.transform.rotation);
        scaleHandler.Initialize(ScaleFrequency, ScaleDamping, ScaleResponsiveness, ScaleDeltaTimeScale, initialScale, ASelector,target.transform.localScale);
    }

    private void OnValidate()
    {
        initialPosition = transform.position;
        initialRotation = transform.rotation;
        initialScale = transform.localScale;
        
        positionHandler.Initialize(PosFrequency, PosDamping, PosResponsiveness, PosDeltaTimeScale, initialPosition, ASelector, target.transform.position);
        rotationHandler.Initialize(RotFrequency, RotDamping, RotResponsiveness, RotDeltaTimeScale, initialRotation, ASelector, target.transform.rotation);
        scaleHandler.Initialize(ScaleFrequency, ScaleDamping, ScaleResponsiveness, ScaleDeltaTimeScale, initialScale, ASelector,target.transform.localScale);
        GV.UpdateOnGUI_Pos(PosFrequency, PosDamping, PosResponsiveness, PosDeltaTimeScale * 0.015f, ASelector, PositionToggle);
        GV.UpdateOnGUI_Rot(RotFrequency, RotDamping, RotResponsiveness, RotDeltaTimeScale  * 0.015f, ASelector, RotationToggle);
        GV.UpdateOnGUI_Scale(ScaleFrequency, ScaleDamping, ScaleResponsiveness, ScaleDeltaTimeScale * 0.015f, ASelector, ScaleToggle);

    }

    private void LateUpdate()
    {
        if(PositionToggle) transform.position = positionHandler.UpdatePosition(target.position);
        if(RotationToggle) transform.rotation = rotationHandler.UpdateRotation(target.rotation);
        if(ScaleToggle) transform.localScale = scaleHandler.UpdateScale(target.localScale);
    }
    
    
}