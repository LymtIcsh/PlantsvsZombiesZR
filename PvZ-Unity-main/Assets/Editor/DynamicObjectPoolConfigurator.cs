using UnityEditor;
using UnityEngine;
using System.Collections.Generic;

public class DynamicObjectPoolConfigurator : EditorWindow
{
    private DynamicObjectPoolManager manager;
    private SerializedObject so;
    private SerializedProperty poolPrefabsProp;
    private Vector2 scrollPos;

    [MenuItem("Tools/Object Pool Configurator")]
    public static void ShowWindow()
    {
        GetWindow<DynamicObjectPoolConfigurator>("Pool Configurator");
    }

    private void OnEnable()
    {
        // �����ҵ������е� Manager
        if (manager == null)
            manager = FindFirstObjectByType<DynamicObjectPoolManager>();

        if (manager != null)
        {
            so = new SerializedObject(manager);
            poolPrefabsProp = so.FindProperty("poolPrefabs");
        }
    }

    private void OnGUI()
    {
        EditorGUILayout.Space();
        EditorGUILayout.LabelField("Dynamic Object Pool Configurator", EditorStyles.boldLabel);

        // 1. ѡ��򴴽� Manager
        EditorGUILayout.BeginHorizontal();
        manager = (DynamicObjectPoolManager)EditorGUILayout.ObjectField("Pool Manager", manager, typeof(DynamicObjectPoolManager), true);
        if (manager == null)
        {
            if (GUILayout.Button("Create", GUILayout.MaxWidth(60)))
            {
                // ����һ���µ� GameObject �����ؽű�
                var go = new GameObject("DynamicObjectPoolManager");
                manager = go.AddComponent<DynamicObjectPoolManager>();
                Undo.RegisterCreatedObjectUndo(go, "Create Pool Manager");
                OnEnable();
            }
        }
        EditorGUILayout.EndHorizontal();

        if (manager == null)
        {
            EditorGUILayout.HelpBox("����ָ���򴴽�һ�� DynamicObjectPoolManager ʵ����", MessageType.Warning);
            return;
        }

        so.Update();

        // 2. ���б�
        EditorGUILayout.Space();
        EditorGUILayout.LabelField("Pool Items", EditorStyles.boldLabel);
        scrollPos = EditorGUILayout.BeginScrollView(scrollPos);

        for (int i = 0; i < poolPrefabsProp.arraySize; i++)
        {
            var itemProp = poolPrefabsProp.GetArrayElementAtIndex(i);
            var typeProp = itemProp.FindPropertyRelative("type");
            var prefabProp = itemProp.FindPropertyRelative("prefab");
            var sizeProp = itemProp.FindPropertyRelative("initialSize");

            EditorGUILayout.BeginVertical("box");
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.PropertyField(typeProp, GUILayout.MaxWidth(200));
            if (GUILayout.Button("Remove", GUILayout.MaxWidth(60)))
            {
                poolPrefabsProp.DeleteArrayElementAtIndex(i);
                break;
            }
            EditorGUILayout.EndHorizontal();

            EditorGUILayout.PropertyField(prefabProp);
            EditorGUILayout.PropertyField(sizeProp);
            EditorGUILayout.EndVertical();
            EditorGUILayout.Space(4);
        }

        EditorGUILayout.EndScrollView();

        // 3. �������
        EditorGUILayout.Space();
        if (GUILayout.Button("Add New Pool Item"))
        {
            poolPrefabsProp.InsertArrayElementAtIndex(poolPrefabsProp.arraySize);
            var newItem = poolPrefabsProp.GetArrayElementAtIndex(poolPrefabsProp.arraySize - 1);
            newItem.FindPropertyRelative("type").enumValueIndex = 0;
            newItem.FindPropertyRelative("prefab").objectReferenceValue = null;
            newItem.FindPropertyRelative("initialSize").intValue = manager.defaultInitialSize;
        }

        // 4. Ӧ�� & ����
        so.ApplyModifiedProperties();
        if (GUI.changed)
        {
            EditorUtility.SetDirty(manager);
            // ��������ڸĶ�ʱ�Զ���ʼ���أ�����ȡ������ע�ͣ�
            // manager.InitializePools();
        }
    }
}
