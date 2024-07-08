using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class BillUtilsWindow : EditorWindow
{
    GameObject prefab;
    GameObject container;
    GameObject saveContainer;
    List<string> savedPrefabPaths = new List<string>();
    Vector2 mainScrollPosition;
    Vector2 savedPrefabsScrollPosition;
    int selectedPrefabIndex = -1;
    GameObject previewObject;
    Editor previewEditor;
    bool showPrefabPreview = true;
    bool showSavedPrefabPreview = true;

    [MenuItem("Tools/Bill Utils/Prefab Management Tools")]
    public static void ShowWindow()
    {
        GetWindow<BillUtilsWindow>("Prefab Management Tools");
    }

    void OnEnable()
    {
        LoadSavedPrefabs();
    }

    void OnGUI()
    {
        mainScrollPosition = GUILayout.BeginScrollView(mainScrollPosition, false, true);

        GUILayout.Label("Prefab Management Tools", EditorStyles.boldLabel);
        EditorGUILayout.Space();

        EditorGUILayout.BeginVertical("box");
        GUILayout.Label("Prefab Settings", EditorStyles.boldLabel);
        prefab = (GameObject)EditorGUILayout.ObjectField("Prefab", prefab, typeof(GameObject), false);
        container = (GameObject)EditorGUILayout.ObjectField("Container", container, typeof(GameObject), true);

        if (GUILayout.Button(new GUIContent(" Instantiate Prefab", EditorGUIUtility.IconContent("d_PreMatCube").image)))
        {
            InstantiatePrefab();
        }
        EditorGUILayout.EndVertical();

        EditorGUILayout.Space();

        showPrefabPreview = EditorGUILayout.Foldout(showPrefabPreview, "Preview Asset");
        if (showPrefabPreview)
        {
            EditorGUILayout.BeginVertical("box");
            GUILayout.Label("Preview Asset", EditorStyles.boldLabel);
            if (prefab != null)
            {
                if (previewObject != prefab)
                {
                    previewObject = prefab;
                    CreatePreview();
                }
                previewEditor.OnInteractivePreviewGUI(GUILayoutUtility.GetRect(256, 128), EditorStyles.helpBox);
            }
            else
            {
                GUILayout.Label("No prefab selected for preview.", EditorStyles.miniLabel);
            }
            EditorGUILayout.EndVertical();
        }

        EditorGUILayout.Space();

        if (GUILayout.Button("Save Prefab"))
        {
            SavePrefab();
        }

        EditorGUILayout.Space();

        EditorGUILayout.BeginVertical("box");
        GUILayout.Label("Saved Prefabs", EditorStyles.boldLabel);
        saveContainer = (GameObject)EditorGUILayout.ObjectField("Save Prefab Container", saveContainer, typeof(GameObject), true);

        savedPrefabsScrollPosition = GUILayout.BeginScrollView(savedPrefabsScrollPosition, GUILayout.Height(200));
        if (savedPrefabPaths.Count == 0)
        {
            GUILayout.Label(
                "No saved prefabs. Saved prefabs will appear here. \n\n" +
                "      d888888b                         d888888b\n" +
                "   d888    8888b                    d888888   888b\n" +
                " d88    88  898888b               d8888  888     88b\n" +
                "d8P        88888888b             d88888888888     b8b\n" +
                "88        8888888888             88888888888       88\n" +
                "88       88888888888             8888888888        88\n" +
                "98b     88888888888P             988888888        d8P\n" +
                " 988          888  8888P      _=_      9888898  88    88P\n" +
                "   9888   888888P      q(-_-)p       98888    888P\n" +
                "      9888888P         '_) (_`         9888888P\n" +
                "      88            /__/  \\            88\n" +
                "      88          _(<_   / )_          88\n" +
                "     d88b        (__\\_\\_|_/__)        d88b\n" +
                "                   NO BUG PLS!!!                ",
                EditorStyles.centeredGreyMiniLabel);
        }
        else
        {
            for (int i = 0; i < savedPrefabPaths.Count; i++)
            {
                EditorGUILayout.BeginHorizontal("box");

                bool isSelected = selectedPrefabIndex == i;
                GUIStyle style = isSelected ? new GUIStyle(GUI.skin.button) { normal = { textColor = Color.green } } : GUI.skin.button;

                if (GUILayout.Button(savedPrefabPaths[i], style, GUILayout.Width(250)))
                {
                    selectedPrefabIndex = isSelected ? -1 : i;
                    if (selectedPrefabIndex != -1)
                    {
                        previewObject = AssetDatabase.LoadAssetAtPath<GameObject>(savedPrefabPaths[selectedPrefabIndex]);
                        CreatePreview();
                    }
                    else
                    {
                        previewObject = null;
                        if (previewEditor != null)
                        {
                            DestroyImmediate(previewEditor);
                        }
                    }
                }

                EditorGUILayout.EndHorizontal();
            }
        }
        GUILayout.EndScrollView();

        EditorGUILayout.BeginHorizontal();
        if (selectedPrefabIndex != -1)
        {
            if (GUILayout.Button("Instantiate", GUILayout.Width(100)))
            {
                InstantiateSavedPrefab(selectedPrefabIndex);
            }

            if (GUILayout.Button("Remove", GUILayout.Width(100)))
            {
                RemoveSavedPrefab(selectedPrefabIndex);
            }
        }
        EditorGUILayout.EndHorizontal();
        EditorGUILayout.EndVertical();

        if (GUILayout.Button("Remove All Prefab Save", GUILayout.Width(300)))
        {
            RemoveAllSavedPrefabs();
        }
        showSavedPrefabPreview = EditorGUILayout.Foldout(showSavedPrefabPreview, "Preview Selected Asset");
        if (showSavedPrefabPreview && previewObject != null)
        {
            EditorGUILayout.BeginVertical("box");
            GUILayout.Label("Preview Selected Asset", EditorStyles.boldLabel);
            previewEditor.OnInteractivePreviewGUI(GUILayoutUtility.GetRect(256, 128), EditorStyles.helpBox);
            EditorGUILayout.EndVertical();
        }

        GUILayout.EndScrollView();
    }

    void InstantiatePrefab()
    {
        if (prefab != null)
        {
            GameObject instance = Instantiate(prefab);
            if (container != null)
            {
                instance.transform.SetParent(container.transform);
            }
            Undo.RegisterCreatedObjectUndo(instance, "Instantiate Prefab");
        }
    }

    void SavePrefab()
    {
        if (prefab != null)
        {
            string path = AssetDatabase.GetAssetPath(prefab);
            if (!savedPrefabPaths.Contains(path))
            {
                savedPrefabPaths.Add(path);
                SavePrefabsToEditorPrefs();
            }
        }
    }

    void InstantiateSavedPrefab(int index)
    {
        string path = savedPrefabPaths[index];
        GameObject prefabToInstantiate = AssetDatabase.LoadAssetAtPath<GameObject>(path);
        if (prefabToInstantiate != null)
        {
            GameObject instance = Instantiate(prefabToInstantiate);
            if (saveContainer != null)
            {
                instance.transform.SetParent(saveContainer.transform);
            }
            Undo.RegisterCreatedObjectUndo(instance, "Instantiate Saved Prefab");
        }
    }

    void RemoveSavedPrefab(int index)
    {
        savedPrefabPaths.RemoveAt(index);
        SavePrefabsToEditorPrefs();
        selectedPrefabIndex = -1;
        previewObject = null;
        if (previewEditor != null)
        {
            DestroyImmediate(previewEditor);
        }
    }

    void LoadSavedPrefabs()
    {
        string savedPrefabsString = EditorPrefs.GetString("BillUtils_SavedPrefabs", "");
        if (!string.IsNullOrEmpty(savedPrefabsString))
        {
            savedPrefabPaths = new List<string>(savedPrefabsString.Split(';'));
        }
    }

    void SavePrefabsToEditorPrefs()
    {
        string savedPrefabsString = string.Join(";", savedPrefabPaths);
        EditorPrefs.SetString("BillUtils_SavedPrefabs", savedPrefabsString);
    }

    void CreatePreview()
    {
        if (previewObject != null)
        {
            if (previewEditor != null)
            {
                DestroyImmediate(previewEditor);
            }
            previewEditor = Editor.CreateEditor(previewObject);
        }
    }
    void RemoveAllSavedPrefabs()
    {
        savedPrefabPaths.Clear();
        SavePrefabsToEditorPrefs();
    }

}
