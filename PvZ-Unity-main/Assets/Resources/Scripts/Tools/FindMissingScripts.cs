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
        if (GUILayout.Button("���Ҷ�ʧ�Ľű�"))
        {
            FindMissingScriptInScene();
        }
    }

    // ���ҳ����еĶ�ʧ�ű�
    private static void FindMissingScriptInScene()
    {
        // ��ȡ�����е���������
        GameObject[] allObjects = GameObject.FindObjectsByType<GameObject>(FindObjectsSortMode.None);
        List<GameObject> objectsWithMissingScripts = new List<GameObject>();

        foreach (GameObject obj in allObjects)
        {
            // ��ȡ�������ϵ��������
            Component[] components = obj.GetComponents<Component>();

            foreach (Component component in components)
            {
                if (component == null)
                {
                    // ������Ϊ null��˵������ű���ʧ
                    objectsWithMissingScripts.Add(obj);
                    break;
                }
            }
        }

        // �����ʧ�ű�������
        if (objectsWithMissingScripts.Count > 0)
        {
            Debug.Log("�ҵ���ʧ�ű�������:");
            foreach (GameObject obj in objectsWithMissingScripts)
            {
                Debug.Log("��ʧ�ű�������: " + obj.name);
            }
        }
        else
        {
            Debug.Log("û�з��ֶ�ʧ�ű�������");
        }
    }
}
#endif