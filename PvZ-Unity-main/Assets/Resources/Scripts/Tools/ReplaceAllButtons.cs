#if UNITY_EDITOR
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;

public class ReplaceAllPicoButtons : EditorWindow
{
    [MenuItem("Tools/Replace All PicoButtons with Button")]
    public static void ReplacePicoButtons()
    {
        // ʹ�� FindObjectsOfTypeAll ��ȷ�������Ǽ������
        PicoButton[] picoButtons = Resources.FindObjectsOfTypeAll<PicoButton>();

        if (picoButtons.Length == 0)
        {
            Debug.Log("û���ҵ� PicoButton �����������ȡ����");
            return;
        }

        Undo.RecordObjects(picoButtons, "Replace PicoButtons");

        foreach (PicoButton oldPico in picoButtons)
        {
            GameObject obj = oldPico.gameObject;
            bool wasActive = obj.activeSelf;

            if (!wasActive)
            {
                obj.SetActive(true); // ���� GameObject �Ա�������
            }

            Undo.RecordObject(obj, "Replace PicoButton");

            // ���� PicoButton ����������
            Selectable.Transition transition = oldPico.transition;
            ColorBlock colors = oldPico.colors;
            SpriteState spriteState = oldPico.spriteState;
            Navigation navigation = oldPico.navigation;
            Graphic targetGraphic = oldPico.targetGraphic;
            bool interactable = oldPico.interactable;
            Button.ButtonClickedEvent onClick = oldPico.onClick;

            // ɾ�� PicoButton�����ԭ�� Button
            DestroyImmediate(oldPico);

            Button newButton = obj.AddComponent<Button>();

            // �ָ�����
            newButton.transition = transition;
            newButton.colors = colors;
            newButton.spriteState = spriteState;
            newButton.navigation = navigation;
            newButton.targetGraphic = targetGraphic;
            newButton.interactable = interactable;
            newButton.onClick = onClick;

            // �ָ� GameObject ��ԭʼ����״̬
            obj.SetActive(wasActive);

            EditorUtility.SetDirty(obj);
        }

        AssetDatabase.SaveAssets();
        Debug.Log($"�ѳɹ��滻 {picoButtons.Length} �� PicoButton Ϊ Button��");
    }
}
#endif