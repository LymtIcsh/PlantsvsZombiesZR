#if UNITY_EDITOR
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;

public class ReplaceAllPicoButtons : EditorWindow
{
    [MenuItem("Tools/Replace All PicoButtons with Button")]
    public static void ReplacePicoButtons()
    {
        // 使用 FindObjectsOfTypeAll 以确保包括非激活对象
        PicoButton[] picoButtons = Resources.FindObjectsOfTypeAll<PicoButton>();

        if (picoButtons.Length == 0)
        {
            Debug.Log("没有找到 PicoButton 组件，操作已取消。");
            return;
        }

        Undo.RecordObjects(picoButtons, "Replace PicoButtons");

        foreach (PicoButton oldPico in picoButtons)
        {
            GameObject obj = oldPico.gameObject;
            bool wasActive = obj.activeSelf;

            if (!wasActive)
            {
                obj.SetActive(true); // 激活 GameObject 以便操作组件
            }

            Undo.RecordObject(obj, "Replace PicoButton");

            // 保存 PicoButton 的属性数据
            Selectable.Transition transition = oldPico.transition;
            ColorBlock colors = oldPico.colors;
            SpriteState spriteState = oldPico.spriteState;
            Navigation navigation = oldPico.navigation;
            Graphic targetGraphic = oldPico.targetGraphic;
            bool interactable = oldPico.interactable;
            Button.ButtonClickedEvent onClick = oldPico.onClick;

            // 删除 PicoButton，添加原生 Button
            DestroyImmediate(oldPico);

            Button newButton = obj.AddComponent<Button>();

            // 恢复属性
            newButton.transition = transition;
            newButton.colors = colors;
            newButton.spriteState = spriteState;
            newButton.navigation = navigation;
            newButton.targetGraphic = targetGraphic;
            newButton.interactable = interactable;
            newButton.onClick = onClick;

            // 恢复 GameObject 的原始激活状态
            obj.SetActive(wasActive);

            EditorUtility.SetDirty(obj);
        }

        AssetDatabase.SaveAssets();
        Debug.Log($"已成功替换 {picoButtons.Length} 个 PicoButton 为 Button！");
    }
}
#endif