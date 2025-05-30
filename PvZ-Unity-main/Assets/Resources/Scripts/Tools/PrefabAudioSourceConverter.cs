#if UNITY_EDITOR
using UnityEngine;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class PrefabAudioSourceConverter : EditorWindow
{
    [MenuItem("Tools/Convert AudioSources to ManagedAudioSource")]
    public static void ShowWindow()
    {
        var window = GetWindow<PrefabAudioSourceConverter>("AudioSource Converter");
        window.minSize = new Vector2(400, 150);
    }

    private void OnGUI()
    {
        GUILayout.Label("Convert AudioSource components to ManagedAudioSource based on playOnAwake", EditorStyles.wordWrappedLabel);
        if (GUILayout.Button("Convert Prefabs"))
        {
            ConvertAllPrefabs();
        }
        GUILayout.Space(10);
        if (GUILayout.Button("Convert Open Scenes"))
        {
            ConvertAllOpenScenes();
        }
        GUILayout.Space(10);
        if (GUILayout.Button("Convert All Build Scenes"))
        {
            ConvertAllBuildScenes();
        }
    }

    // 处理所有预制体
    private static void ConvertAllPrefabs()
    {
        string[] guids = AssetDatabase.FindAssets("t:Prefab");
        int totalProcessed = 0;
        int totalConverted = 0;

        foreach (string guid in guids)
        {
            string path = AssetDatabase.GUIDToAssetPath(guid);
            GameObject root = PrefabUtility.LoadPrefabContents(path);
            bool dirty = ProcessTransformHierarchy(root.transform, ref totalProcessed, ref totalConverted);

            if (dirty)
                PrefabUtility.SaveAsPrefabAsset(root, path);

            PrefabUtility.UnloadPrefabContents(root);
        }

        AssetDatabase.Refresh();
        Debug.Log($"Prefabs processed: {totalProcessed}, converted: {totalConverted}.");
    }

    // 处理所有已打开场景中的音源
    private static void ConvertAllOpenScenes()
    {
        int totalProcessed = 0, totalConverted = 0;
        for (int i = 0; i < EditorSceneManager.sceneCount; i++)
        {
            Scene scene = EditorSceneManager.GetSceneAt(i);
            bool dirty = false;
            foreach (GameObject rootObj in scene.GetRootGameObjects())
            {
                if (ProcessTransformHierarchy(rootObj.transform, ref totalProcessed, ref totalConverted))
                    dirty = true;
            }
            if (dirty)
                EditorSceneManager.MarkSceneDirty(scene);
        }
        EditorSceneManager.SaveOpenScenes();
        Debug.Log($"Open scenes processed: {totalProcessed}, converted: {totalConverted}.");
    }

    // 处理构建设置中的所有场景
    private static void ConvertAllBuildScenes()
    {
        int totalProcessed = 0, totalConverted = 0;
        foreach (EditorBuildSettingsScene bsScene in EditorBuildSettings.scenes)
        {
            if (!bsScene.enabled) continue;
            Scene scene = EditorSceneManager.OpenScene(bsScene.path, OpenSceneMode.Single);
            bool dirty = false;
            foreach (GameObject rootObj in scene.GetRootGameObjects())
            {
                if (ProcessTransformHierarchy(rootObj.transform, ref totalProcessed, ref totalConverted))
                    dirty = true;
            }
            if (dirty)
                EditorSceneManager.MarkSceneDirty(scene);
            EditorSceneManager.SaveScene(scene);
        }
        Debug.Log($"Build scenes processed: {totalProcessed}, converted: {totalConverted}.");
    }

    // 递归处理 Transform 下的 AudioSource
    private static bool ProcessTransformHierarchy(Transform parent, ref int processed, ref int converted)
    {
        bool dirty = false;
        AudioSource src = parent.GetComponent<AudioSource>();
        if (src != null)
        {
            processed++;
            if (!src.playOnAwake)
            {
                Object.DestroyImmediate(src, parent == parent.root ? true : false);
                dirty = true;
            }
            else
            {
                AudioClip clip = src.clip;
                bool loop = src.loop;
                Object.DestroyImmediate(src, true);

                var managed = parent.gameObject.AddComponent<ManagedAudioSource>();
                SerializedObject so = new SerializedObject(managed);
                so.FindProperty("clip").objectReferenceValue = clip;
                so.FindProperty("playOnAwake").boolValue = true;
                so.FindProperty("loop").boolValue = loop;
                so.ApplyModifiedProperties();

                converted++;
                dirty = true;
            }
        }

        // 递归子节点
        foreach (Transform child in parent)
        {
            if (ProcessTransformHierarchy(child, ref processed, ref converted))
                dirty = true;
        }
        return dirty;
    }
}
#endif
