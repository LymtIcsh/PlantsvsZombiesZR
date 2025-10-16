#if UNITY_EDITOR
using UnityEngine;
using UnityEditor;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;

public class FindStencilMaterialIssues : EditorWindow
{
    [MenuItem("Tools/查找 UI 材质错误")]
    public static void ShowWindow()
    {
        GetWindow<FindStencilMaterialIssues>("查找 UI 材质错误");
    }

    private void OnGUI()
    {
        if (GUILayout.Button("扫描场景中的 UI 材质问题"))
        {
            FindMaterialIssues();
        }
    }

    private static void FindMaterialIssues()
    {
        List<string> problematicObjects = new List<string>();

        // 检查 Unity UI 组件（Image、Text 等）
        foreach (var graphic in GameObject.FindObjectsByType<Graphic>(FindObjectsSortMode.None))
        {
            if (graphic.material != null && graphic.material.name.Contains("Sprites-Default"))
            {
                problematicObjects.Add(graphic.gameObject.name);
            }
        }

        // 检查 TextMeshPro UI 组件
        foreach (var tmp in GameObject.FindObjectsByType<TextMeshProUGUI>(FindObjectsSortMode.None))
        {
            if (tmp.fontMaterial != null && tmp.fontMaterial.name.Contains("Sprites-Default"))
            {
                problematicObjects.Add(tmp.gameObject.name);
            }
        }

        if (problematicObjects.Count > 0)
        {
            Debug.LogWarning("?? 检测到使用错误材质的 UI 组件：");
            foreach (var objName in problematicObjects)
            {
                Debug.Log($"?? {objName}");
            }
        }
        else
        {
            Debug.Log("? 未发现材质使用错误的 UI 组件。");
        }
    }
}
#endif