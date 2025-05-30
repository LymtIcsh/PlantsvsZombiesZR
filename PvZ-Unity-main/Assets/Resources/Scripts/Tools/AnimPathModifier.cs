#if UNITY_EDITOR
using UnityEngine;
using UnityEditor;
using System.IO;

public class AnimPathModifier : EditorWindow
{
    private string folderPath = "Assets/Animations";

    [MenuItem("Tools/��������/�������Body·��ǰ׺")]
    public static void ShowWindow()
    {
        GetWindow<AnimPathModifier>("����·���޸���");
    }

    private void OnGUI()
    {
        GUILayout.Label("������Ӷ���·��ǰ׺", EditorStyles.boldLabel);
        folderPath = EditorGUILayout.TextField("�����ļ���·��", folderPath);

        if (GUILayout.Button("�������ļ�"))
        {
            ProcessAnimations();
        }
    }

    private void ProcessAnimations()
    {
        string[] guids = AssetDatabase.FindAssets("t:AnimationClip", new[] { folderPath });

        int modifiedCount = 0;
        foreach (string guid in guids)
        {
            string assetPath = AssetDatabase.GUIDToAssetPath(guid);
            AnimationClip clip = AssetDatabase.LoadAssetAtPath<AnimationClip>(assetPath);

            if (clip == null) continue;

            Undo.RegisterCompleteObjectUndo(clip, "Modify Animation Paths");

            EditorCurveBinding[] bindings = AnimationUtility.GetCurveBindings(clip);
            foreach (var binding in bindings)
            {
                string oldPath = binding.path;

                // �������·����""�����Ѿ���Bodyǰ׺��·��
                if (string.IsNullOrEmpty(oldPath) || oldPath.StartsWith("Body/")) continue;

                EditorCurveBinding newBinding = binding;
                newBinding.path = "Body/" + oldPath;

                AnimationCurve curve = AnimationUtility.GetEditorCurve(clip, binding);

                // ɾ��������
                AnimationUtility.SetEditorCurve(clip, binding, null);
                // ���������
                AnimationUtility.SetEditorCurve(clip, newBinding, curve);
            }

            modifiedCount++;
            EditorUtility.SetDirty(clip);
        }

        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();

        Debug.Log($"������ɣ��޸��� {modifiedCount} �������ļ���");
    }
}
#endif