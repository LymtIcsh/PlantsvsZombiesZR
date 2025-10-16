using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class ChooseForestBuff : MonoBehaviour
{
    // ��������ָʾ��İ�ť����
    public Button triggerButton;

    // ָʾ�򣨵����򣩵�����
    public GameObject dialogBox;

    // ���� Toggle ������
    public Toggle toggleOption1;
    public Toggle toggleOption2;

    // ȷ�ϰ�ť������
    public Button confirmButton;

    // ÿ�� Toggle ѡ���Ӧ���¼�
    public UnityEvent onToggle1Selected;
    public UnityEvent onToggle2Selected;

    // Start ���ڵ�һ��֡����֮ǰ����
    void Start()
    {
        // ��ʼʱ����ָʾ��
        dialogBox.SetActive(false);

        // Ϊ��ť�� toggle ��Ӽ����¼�
        triggerButton.onClick.AddListener(OpenDialog);
        confirmButton.onClick.AddListener(ConfirmSelection);

        // Ϊ Toggle ��Ӽ�������ȷ��ѡ��ʱ��һ�� Toggle ��ȡ��
        toggleOption1.onValueChanged.AddListener((value) => OnToggleValueChanged(value, toggleOption2));
        toggleOption2.onValueChanged.AddListener((value) => OnToggleValueChanged(value, toggleOption1));
    }

    // ��ָʾ��ĺ�������������ť���ʱ����
    void OpenDialog()
    {
        dialogBox.SetActive(true); // ��ʾָʾ��
        toggleOption1.isOn = false; // ��ָʾ��ʱ���� Toggle ״̬
        toggleOption2.isOn = false;
    }

    // ȷ�ϰ�ť���ʱ�Ĵ�����
    void ConfirmSelection()
    {
        if (toggleOption1.isOn)
        {
            Debug.Log("ѡ��1");
            StaticThingsManagement.forestBuff = 0;
        }
        else if (toggleOption2.isOn)
        {
            Debug.Log("ѡ��2");
            StaticThingsManagement.forestBuff = 1;
        }

        // ȷ��ѡ���ر�ָʾ��
        dialogBox.SetActive(false);
    }

    // Toggle ѡ��仯ʱ�Ĵ�������ȷ����һ�� Toggle ȡ��ѡ��
    void OnToggleValueChanged(bool value, Toggle otherToggle)
    {
        if (value) // �����ǰ Toggle ��ѡ��
        {
            otherToggle.isOn = false; // ȡ����һ�� Toggle ��ѡ��״̬
        }
    }
}
