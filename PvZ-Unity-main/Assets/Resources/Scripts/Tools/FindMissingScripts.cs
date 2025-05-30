#if UNITY_EDITOR
using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

public class FindMissingScripts : EditorWindow
{
    [MenuItem("Tools/Find Missing Scripts")]
    public static void ShowWindow()
    {
        EditorWindow.GetWindow<FindMissingScripts>("Find Missing Scripts");
    }

    void OnGUI()
    {
        if (GUILayout.Button("查找丢失的脚本"))
        {
            FindMissingScriptInScene();
        }
    }

    // 查找场景中的丢失脚本
    private static void FindMissingScriptInScene()
    {
        // 获取场景中的所有物体
        GameObject[] allObjects = GameObject.FindObjectsByType<GameObject>(FindObjectsSortMode.None);
        List<GameObject> objectsWithMissingScripts = new List<GameObject>();

        foreach (GameObject obj in allObjects)
        {
            // 获取该物体上的所有组件
            Component[] components = obj.GetComponents<Component>();

            foreach (Component component in components)
            {
                if (component == null)
                {
                    // 如果组件为 null，说明这个脚本丢失
                    objectsWithMissingScripts.Add(obj);
                    break;
                }
            }
        }

        // 输出丢失脚本的物体
        if (objectsWithMissingScripts.Count > 0)
        {
            Debug.Log("找到丢失脚本的物体:");
            foreach (GameObject obj in objectsWithMissingScripts)
            {
                Debug.Log("丢失脚本的物体: " + obj.name);
            }
        }
        else
        {
            Debug.Log("没有发现丢失脚本的物体");
        }
    }
}
#endif