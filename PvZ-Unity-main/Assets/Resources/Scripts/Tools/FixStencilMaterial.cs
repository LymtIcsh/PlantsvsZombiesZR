#if UNITY_EDITOR
using UnityEngine;
using UnityEditor;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;

public class FindStencilMaterialIssues : EditorWindow
{
    [MenuItem("Tools/���� UI ���ʴ���")]
    public static void ShowWindow()
    {
        GetWindow<FindStencilMaterialIssues>("���� UI ���ʴ���");
    }

    private void OnGUI()
    {
        if (GUILayout.Button("ɨ�賡���е� UI ��������"))
        {
            FindMaterialIssues();
        }
    }

    private static void FindMaterialIssues()
    {
        List<string> problematicObjects = new List<string>();

        // ��� Unity UI �����Image��Text �ȣ�
        foreach (var graphic in GameObject.FindObjectsByType<Graphic>(FindObjectsSortMode.None))
        {
            if (graphic.material != null && graphic.material.name.Contains("Sprites-Default"))
            {
                problematicObjects.Add(graphic.gameObject.name);
            }
        }

        // ��� TextMeshPro UI ���
        foreach (var tmp in GameObject.FindObjectsByType<TextMeshProUGUI>(FindObjectsSortMode.None))
        {
            if (tmp.fontMaterial != null && tmp.fontMaterial.name.Contains("Sprites-Default"))
            {
                problematicObjects.Add(tmp.gameObject.name);
            }
        }

        if (problematicObjects.Count > 0)
        {
            Debug.LogWarning("?? ��⵽ʹ�ô�����ʵ� UI �����");
            foreach (var objName in problematicObjects)
            {
                Debug.Log($"?? {objName}");
            }
        }
        else
        {
            Debug.Log("? δ���ֲ���ʹ�ô���� UI �����");
        }
    }
}
#endif