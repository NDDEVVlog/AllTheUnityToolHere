using UnityEngine;
using UnityEditor;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using UnityEditor.SceneManagement;
using System.Linq;

public class SceneSwitcherTool : EditorWindow
{
    private List<SceneAsset> bookmarkedScenes = new List<SceneAsset>();
    private string selectedScene = null;
    private string searchQuery = "";

    private const string BookmarkedScenesKey = "BookmarkedScenes";

    [MenuItem("Tools/Scene Switcher Tool")]
    public static void ShowWindow()
    {
        GetWindow<SceneSwitcherTool>("Scene Switcher Tool");
    }

    private void OnEnable()
    {
        LoadBookmarkedScenes();
    }

    private void OnGUI()
    {
        GUILayout.Label("Search Scenes", EditorStyles.boldLabel);
        searchQuery = EditorGUILayout.TextField(searchQuery);

        GUILayout.Space(10);

        GUILayout.BeginHorizontal();

        // Bắt đầu vẽ border cho phần Scenes in Build
        GUILayout.BeginVertical("box", GUILayout.MaxWidth(300));

        GUILayout.Label("Scenes in Build", EditorStyles.boldLabel);

        // Hiển thị các scene trong build settings
        for (int i = 0; i < EditorBuildSettings.scenes.Length; i++)
        {
            EditorBuildSettingsScene scene = EditorBuildSettings.scenes[i];
            string sceneName = System.IO.Path.GetFileNameWithoutExtension(scene.path);

            if (!string.IsNullOrEmpty(searchQuery) && !sceneName.ToLower().Contains(searchQuery.ToLower()))
            {
                continue;
            }

            EditorGUILayout.BeginHorizontal();

            GUIContent content = new GUIContent(sceneName, EditorGUIUtility.IconContent("d_UnityEditor.GameView").image);
            bool isSelected = GUILayout.Toggle(selectedScene == sceneName, content, "Button");

            if (isSelected && selectedScene != sceneName)
            {
                selectedScene = sceneName;
            }

            EditorGUILayout.EndHorizontal();
        }

        GUILayout.Space(20);
        GUILayout.Label("Bookmarked Scenes", EditorStyles.boldLabel);

        // Hiển thị các scene đã bookmark
        foreach (SceneAsset sceneAsset in bookmarkedScenes)
        {
            string scenePath = AssetDatabase.GetAssetPath(sceneAsset);
            string sceneName = System.IO.Path.GetFileNameWithoutExtension(scenePath);

            if (!string.IsNullOrEmpty(searchQuery) && !sceneName.ToLower().Contains(searchQuery.ToLower()))
            {
                continue;
            }

            EditorGUILayout.BeginHorizontal();

            GUIContent content = new GUIContent(sceneName, EditorGUIUtility.IconContent("d_UnityEditor.GameView").image);
            bool isSelected = GUILayout.Toggle(selectedScene == sceneName, content, "Button");

            if (isSelected && selectedScene != sceneName)
            {
                selectedScene = sceneName;
            }

            EditorGUILayout.EndHorizontal();
        }

        GUILayout.EndVertical();
        // Kết thúc vẽ border cho phần Scenes in Build

        // Bắt đầu vẽ border cho phần nút cố định
        GUILayout.BeginVertical("box", GUILayout.MaxWidth(150));

        GUILayout.Space(10);

        // Tiêu đề cho nút Play
        GUILayout.Label("Play Scene", EditorStyles.boldLabel);

        // Nút chuyển scene
        if (GUILayout.Button(new GUIContent("Play", EditorGUIUtility.IconContent("d_PlayButton").image), GUILayout.Height(30)))
        {
            if (!string.IsNullOrEmpty(selectedScene))
            {
                if (EditorApplication.isPlaying)
                {
                    SceneManager.LoadScene(selectedScene);
                }
                else
                {
                    string scenePath = GetScenePathByName(selectedScene);
                    EditorSceneManager.OpenScene(scenePath);
                    EditorApplication.isPlaying = true;
                }
            }
        }

        // Tiêu đề cho các nút khác
        GUILayout.Label("Scene Options", EditorStyles.boldLabel);

        // Nút chuyển scene additive
        if (GUILayout.Button(new GUIContent("Additive", EditorGUIUtility.IconContent("d_Toolbar Plus More").image), GUILayout.Height(30)))
        {
            if (!string.IsNullOrEmpty(selectedScene))
            {
                string scenePath = GetScenePathByName(selectedScene);
                EditorSceneManager.OpenScene(scenePath, OpenSceneMode.Additive);
            }
        }

        // Nút load scene
        if (GUILayout.Button(new GUIContent("Load", EditorGUIUtility.IconContent("d_Refresh").image), GUILayout.Height(30)))
        {
            if (!string.IsNullOrEmpty(selectedScene))
            {
                string scenePath = GetScenePathByName(selectedScene);
                EditorSceneManager.OpenScene(scenePath);
            }
        }

        // Nút bookmark
        if (GUILayout.Button(new GUIContent("Bookmark", EditorGUIUtility.IconContent("Favorite").image), GUILayout.Height(30)))
        {
            if (!string.IsNullOrEmpty(selectedScene))
            {
                string scenePath = GetScenePathByName(selectedScene);
                SceneAsset sceneAsset = AssetDatabase.LoadAssetAtPath<SceneAsset>(scenePath);
                if (sceneAsset != null && !bookmarkedScenes.Contains(sceneAsset))
                {
                    bookmarkedScenes.Add(sceneAsset);
                    SaveBookmarkedScenes();
                }
            }
        }

        GUILayout.EndVertical();
        // Kết thúc vẽ border cho phần nút cố định

        GUILayout.EndHorizontal();
    }

    private string GetScenePathByName(string sceneName)
    {
        foreach (EditorBuildSettingsScene scene in EditorBuildSettings.scenes)
        {
            if (System.IO.Path.GetFileNameWithoutExtension(scene.path) == sceneName)
            {
                return scene.path;
            }
        }
        return null;
    }

    private void LoadBookmarkedScenes()
    {
        bookmarkedScenes.Clear();
        string[] sceneGUIDs = EditorPrefs.GetString(BookmarkedScenesKey, "").Split(';');
        foreach (string guid in sceneGUIDs)
        {
            if (!string.IsNullOrEmpty(guid))
            {
                string path = AssetDatabase.GUIDToAssetPath(guid);
                SceneAsset sceneAsset = AssetDatabase.LoadAssetAtPath<SceneAsset>(path);
                if (sceneAsset != null)
                {
                    bookmarkedScenes.Add(sceneAsset);
                }
            }
        }
    }

    private void SaveBookmarkedScenes()
    {
        List<string> sceneGUIDs = new List<string>();
        foreach (SceneAsset sceneAsset in bookmarkedScenes)
        {
            string path = AssetDatabase.GetAssetPath(sceneAsset);
            string guid = AssetDatabase.AssetPathToGUID(path);
            sceneGUIDs.Add(guid);
        }
        EditorPrefs.SetString(BookmarkedScenesKey, string.Join(";", sceneGUIDs));
    }
}
