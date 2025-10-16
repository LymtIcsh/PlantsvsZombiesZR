#if UNITY_EDITOR
using UnityEngine;
using UnityEditor;
using UnityEditor.Animations;
using System.Collections.Generic;

public class ApplyAnimationFirstFrameToPrefab : MonoBehaviour
{
    [MenuItem("Tools/动画工具/应用第一帧Transform到物体并设置所有材质_IsVisible为-1")]
    private static void ApplyFirstFrameTransforms()
    {
        GameObject selected = Selection.activeGameObject;

        if (selected == null)
        {
            Debug.LogError("请先选中一个带有 Animator 的 GameObject！");
            return;
        }

        Animator animator = selected.GetComponent<Animator>();
        if (animator == null)
        {
            Debug.LogError("选中的物体没有 Animator 组件！");
            return;
        }

        AnimatorController controller = animator.runtimeAnimatorController as AnimatorController;
        if (controller == null || controller.animationClips.Length == 0)
        {
            Debug.LogError("未找到动画剪辑！");
            return;
        }

        AnimationClip clip = controller.animationClips[0];
        Undo.RegisterFullObjectHierarchyUndo(selected, "Apply First Frame Transforms");

        var bindings = AnimationUtility.GetCurveBindings(clip);
        Dictionary<string, Transform> nameToTransform = new Dictionary<string, Transform>();

        foreach (Transform t in selected.GetComponentsInChildren<Transform>(true))
        {
            string relativePath = AnimationUtility.CalculateTransformPath(t, selected.transform);
            nameToTransform[relativePath] = t;
        }

        foreach (var binding in bindings)
        {
            if (!nameToTransform.ContainsKey(binding.path))
                continue;

            Transform target = nameToTransform[binding.path];
            AnimationCurve curve = AnimationUtility.GetEditorCurve(clip, binding);
            float value = curve.Evaluate(0f);

            if (binding.propertyName == "m_LocalPosition.x")
                target.localPosition = new Vector3(value, target.localPosition.y, target.localPosition.z);
            else if (binding.propertyName == "m_LocalPosition.y")
                target.localPosition = new Vector3(target.localPosition.x, value, target.localPosition.z);
            else if (binding.propertyName == "m_LocalPosition.z")
                target.localPosition = new Vector3(target.localPosition.x, target.localPosition.y, value);
            else if (binding.propertyName == "m_LocalScale.x")
                target.localScale = new Vector3(value, target.localScale.y, target.localScale.z);
            else if (binding.propertyName == "m_LocalScale.y")
                target.localScale = new Vector3(target.localScale.x, value, target.localScale.z);
            else if (binding.propertyName == "m_LocalScale.z")
                target.localScale = new Vector3(target.localScale.x, target.localScale.y, value);
        }

        // ? 统一设置所有材质中的 _IsVisible 为 -1
        foreach (Renderer renderer in selected.GetComponentsInChildren<Renderer>(true))
        {
            foreach (Material mat in renderer.sharedMaterials)
            {
                if (mat != null && mat.HasProperty("_IsVisible"))
                {
                    Undo.RecordObject(mat, "Set _IsVisible to -1");
                    mat.SetFloat("_IsVisible", -1f);
                    EditorUtility.SetDirty(mat);
                }
            }
        }

        Debug.Log("已将动画第一帧的位置和缩放应用到物体，所有材质的 _IsVisible 已设置为 -1。");
    }
}
#endif
