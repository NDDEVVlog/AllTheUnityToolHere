using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using Sora_Ults;

[CustomEditor(typeof(GraphVisualize))]
public class GraphInspector : Editor
{
    private AlgorithmSelector_SO algorithmSelector = new AlgorithmSelector_SO();
    private ISecondOrderSystem _selectedAlgorithm = new SO_Calc_None();

    public Graph _graph;
    //private SO_Calc_ZeroPole _soCalcZeroPole = new SO_Calc_ZeroPole();
    private SerializedObject _serializedObject;
    private SerializedProperty _PosToggleProperty;
    private SerializedProperty _PosfrequencyProperty;
    private SerializedProperty _PosdampingProperty;
    private SerializedProperty _PosResponsivenessProperty;
    private SerializedProperty _PosDeltaTimeScaleProperty;
    
    private SerializedProperty _RotToggleProperty;
    private SerializedProperty _RotfrequencyProperty;
    private SerializedProperty _RotdampingProperty;
    private SerializedProperty _RotResponsivenessProperty;
    private SerializedProperty _RotDeltaTimeScaleProperty;
    
    private SerializedProperty _ScaleToggleProperty;
    private SerializedProperty _ScalefrequencyProperty;
    private SerializedProperty _ScaledampingProperty;
    private SerializedProperty _ScaleResponsivenessProperty;
    private SerializedProperty _ScaleDeltaTimeScaleProperty;
    
    bool showPosition = true;
    bool showRotation = true;
    bool showScale = true;
    
    [Range(0, 5000)]
    [SerializeField] int GraphDetail = 1000;
    [Range(0, 50)] [SerializeField] float _graphMaxX = 10;
    [Range(0, 30)] [SerializeField] float _graphMaxY = 1;

    [SerializeField] private Color PosColor = Color.yellow;
    [SerializeField] private Color RotColor = new Color(47/255, 0.6f, 1);
    [SerializeField] private Color ScaleColor = new Color(1, 0.1f, 0.3f);

    private void OnEnable()
    {
        _serializedObject = new SerializedObject(target);
        _PosToggleProperty = _serializedObject.FindProperty("PositionToggle");
        _PosfrequencyProperty = _serializedObject.FindProperty("PosFrequency");
        _PosdampingProperty = _serializedObject.FindProperty("PosDamping");
        _PosResponsivenessProperty = _serializedObject.FindProperty("PosResponsiveness");
        _PosDeltaTimeScaleProperty = _serializedObject.FindProperty("PosDeltaTime");
        
        _RotToggleProperty = _serializedObject.FindProperty("RotationToggle");
        _RotfrequencyProperty = _serializedObject.FindProperty("RotFrequency");
        _RotdampingProperty = _serializedObject.FindProperty("RotDamping");
        _RotResponsivenessProperty = _serializedObject.FindProperty("RotResponsiveness");
        _RotDeltaTimeScaleProperty = _serializedObject.FindProperty("RotDeltaTime");
        
        _ScaleToggleProperty = _serializedObject.FindProperty("ScaleToggle");
        _ScalefrequencyProperty = _serializedObject.FindProperty("ScaleFrequency");
        _ScaledampingProperty = _serializedObject.FindProperty("ScaleDamping");
        _ScaleResponsivenessProperty = _serializedObject.FindProperty("ScaleResponsiveness");
        _ScaleDeltaTimeScaleProperty = _serializedObject.FindProperty("ScaleDeltaTime");
    }
    public override void OnInspectorGUI()
    {
        _serializedObject.Update();

        EditorGUILayout.LabelField("Graphic Settings", EditorStyles.boldLabel);
        EditorGUILayout.Space(5);

        GraphDetail = EditorGUILayout.IntSlider("Graph Detail", GraphDetail, 0, 5000);
        _graphMaxX = EditorGUILayout.Slider("Max X", _graphMaxX, 0, 50);
        _graphMaxY = EditorGUILayout.Slider("Max Y", _graphMaxY, 0, 30);

        PosColor = EditorGUILayout.ColorField("PosLine Color", PosColor);
        RotColor = EditorGUILayout.ColorField("RotLine Color", RotColor);
        ScaleColor = EditorGUILayout.ColorField("ScaleLine Color", ScaleColor);

        _graph = new Graph(0, _graphMaxX, 0, _graphMaxY);

        _graph.HorizontalAxisUnits = "s";
        _graph.LabelStyle = "label";

        EditorGUILayout.Space(5);
        // Draw the sine graph in the Inspector
        UpdateGraphLine();
        DrawGraph();

        GUILayout.Space(15);
        // Access and modify the selected algorithm directly from the target object
        GraphVisualize graphVisualize = (GraphVisualize)target;
        graphVisualize.selectedAlgorithm = (SelectedAlgorithm)EditorGUILayout.EnumPopup("Selected Algorithm", graphVisualize.selectedAlgorithm);
        GUILayout.Space(10);

        showPosition = EditorGUILayout.BeginFoldoutHeaderGroup(showPosition, "Position");
        if (showPosition)
        {
            // Display the properties below the graph
            EditorGUI.BeginDisabledGroup(true);
            EditorGUILayout.PropertyField(_PosfrequencyProperty);
            EditorGUILayout.PropertyField(_PosdampingProperty);
            EditorGUILayout.PropertyField(_PosResponsivenessProperty);
            EditorGUI.EndDisabledGroup();
            EditorGUILayout.PropertyField(_PosDeltaTimeScaleProperty);
        }
        EditorGUILayout.EndFoldoutHeaderGroup();
        
        showRotation = EditorGUILayout.BeginFoldoutHeaderGroup(showRotation, "Rotation");
        if (showRotation)
        {
            // Display the properties below the graph
            EditorGUI.BeginDisabledGroup(true);
            EditorGUILayout.PropertyField(_RotfrequencyProperty);
            EditorGUILayout.PropertyField(_RotdampingProperty);
            EditorGUILayout.PropertyField(_RotResponsivenessProperty);
            EditorGUI.EndDisabledGroup();
            EditorGUILayout.PropertyField(_RotDeltaTimeScaleProperty);
        }
        EditorGUILayout.EndFoldoutHeaderGroup();
        
        showScale = EditorGUILayout.BeginFoldoutHeaderGroup(showScale, "Scale");
        if (showScale)
        {
            // Display the properties below the graph
            EditorGUI.BeginDisabledGroup(true);
            EditorGUILayout.PropertyField(_ScalefrequencyProperty);
            EditorGUILayout.PropertyField(_ScaledampingProperty);
            EditorGUILayout.PropertyField(_ScaleResponsivenessProperty);
            EditorGUI.EndDisabledGroup();
            EditorGUILayout.PropertyField(_ScaleDeltaTimeScaleProperty);
        }
        EditorGUILayout.EndFoldoutHeaderGroup();
        
        
        
        _serializedObject.ApplyModifiedProperties();
    }
    private void DrawGraph()
    {
        GUILayout.Label("Graph", EditorStyles.boldLabel);
        EditorGUILayout.Space(5);
        Rect graphRect = GUILayoutUtility.GetRect(0, 100, GUILayout.ExpandWidth(true));
        _graph.Draw(graphRect);
        if (GUI.changed) Repaint();
    }

    private float[] ySamples;
    private int currentIndex = 0;

    public void UpdateGraphLine()
    {
        //Example 
        //ySamples = CalculateLine_Sin();
        //_graph.UpdateLine("line1", ySamples, Color.yellow);

        if (_PosToggleProperty.boolValue)
        {
            ySamples = CalculateLine_SelectedAlgorithm(_PosfrequencyProperty, _PosdampingProperty,
                                                                    _PosResponsivenessProperty, _PosDeltaTimeScaleProperty);
            _graph.UpdateLine("PosLine", ySamples, PosColor);
        }


        if (_RotToggleProperty.boolValue)
        {
            ySamples = CalculateLine_SelectedAlgorithm(_RotfrequencyProperty, _RotdampingProperty,
                _RotResponsivenessProperty, _RotDeltaTimeScaleProperty);
            _graph.UpdateLine("RotLine", ySamples, RotColor);
        }

        if (_ScaleToggleProperty.boolValue)
        {
            ySamples = CalculateLine_SelectedAlgorithm(_ScalefrequencyProperty, _ScaledampingProperty,
                                                                     _ScaleResponsivenessProperty, _ScaleDeltaTimeScaleProperty);
            _graph.UpdateLine("ScaleLine", ySamples, ScaleColor);
        }
    }

    //example, to print anything owo
    private float[] CalculateLine_Sin()
    {
        if (ySamples == null || ySamples.Length != GraphDetail)
        {
            ySamples = new float[GraphDetail];
        }

        float frequency = _PosfrequencyProperty.floatValue;
        // Calculate new y samples for the graph
        for (int i = 0; i < GraphDetail; i++)
        {
            float x = i / 10f * frequency;
            ySamples[(currentIndex + i) % GraphDetail] = Mathf.Sin(x);
        }
        currentIndex = (currentIndex + GraphDetail) % GraphDetail;

        return ySamples;
    }

    ISecondOrderSystem selectedAlgorithm;
    private GraphVisualize graphVisualize;
    void SelectAlgorithm()
    {
        graphVisualize = (GraphVisualize)target;
        switch (graphVisualize.selectedAlgorithm)
        {
            case SelectedAlgorithm.ZeroPole:
                selectedAlgorithm = new SO_Calc_ZeroPole();
                break;
            case SelectedAlgorithm.StableForcedIterations:
                selectedAlgorithm = new SO_Calc_StableForcedIterations();
                break;
            case SelectedAlgorithm.Euler:
                selectedAlgorithm = new SO_Calc_Euler();
                break;
            case SelectedAlgorithm.None:
                selectedAlgorithm = new SO_Calc_None();
                break;
            default:
                Debug.Log("pick plz");
                selectedAlgorithm = new SO_Calc_None();
                break;
        }
    }

    private float[] CalculateLine_SelectedAlgorithm(SerializedProperty _frequencyProperty, 
                                                    SerializedProperty _dampingProperty, 
                                                    SerializedProperty _ResponsivenessProperty,
                                                    SerializedProperty _DeltaTimeScaleProperty)
    {
        SelectAlgorithm();

        float[] LineArray = new float[GraphDetail];
        SecondOrderState state = new SecondOrderState
        {
            F = _frequencyProperty.floatValue,
            Z = _dampingProperty.floatValue,
            R = _ResponsivenessProperty.floatValue,
            InitialValue = 0.0f,
            DeltaTime = _DeltaTimeScaleProperty.floatValue,
            TargetValue = 1.0f,
        };

        selectedAlgorithm.RecalculateConstants(ref state, state.InitialValue);

        for (int i = 0; i < GraphDetail; i++)
        {
            LineArray[i] = selectedAlgorithm.UpdateStrategy(ref state, state.DeltaTime, state.TargetValue, null);
        }

        return LineArray;
    }
}
