#if UNITY_EDITOR
using UnityEngine;
using UnityEditor;
using System.IO;

public class AnimPathModifier : EditorWindow
{
    private string folderPath = "Assets/Animations";

    [MenuItem("Tools/动画工具/批量添加Body路径前缀")]
    public static void ShowWindow()
    {
        GetWindow<AnimPathModifier>("动画路径修改器");
    }

    private void OnGUI()
    {
        GUILayout.Label("批量添加动画路径前缀", EditorStyles.boldLabel);
        folderPath = EditorGUILayout.TextField("动画文件夹路径", folderPath);

        if (GUILayout.Button("处理动画文件"))
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

                // 不处理根路径（""）和已经有Body前缀的路径
                if (string.IsNullOrEmpty(oldPath) || oldPath.StartsWith("Body/")) continue;

                EditorCurveBinding newBinding = binding;
                newBinding.path = "Body/" + oldPath;

                AnimationCurve curve = AnimationUtility.GetEditorCurve(clip, binding);

                // 删除旧曲线
                AnimationUtility.SetEditorCurve(clip, binding, null);
                // 添加新曲线
                AnimationUtility.SetEditorCurve(clip, newBinding, curve);
            }

            modifiedCount++;
            EditorUtility.SetDirty(clip);
        }

        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();

        Debug.Log($"处理完成，修改了 {modifiedCount} 个动画文件。");
    }
}
#endif