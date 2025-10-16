using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // ����UI�����ռ��Ա�ʹ��Button���

public class ButtonSetLevel : MonoBehaviour
{
    public int Buttonlevel; // ��ǰ��ť��Ӧ�Ĺؿ���
    public bool isInitialLevel; // �Ƿ�Ϊ��ʼ�ؿ�
    private Button button; // ��ť�������

    void Start()
    {
        button = GetComponent<Button>(); // ��ȡ��ǰGameObject��Button���
        UpdateButtonVisibility(); // ��ʼ��ʱ���°�ť�Ŀɼ���
    }

    public void SetLevel()
    {
        BeginManagement.level = Buttonlevel;
    }

    // ���°�ť�Ŀɼ���
    public void UpdateButtonVisibility()
    {
        // ����ǳ�ʼ�ؿ�������ǰһ���ؿ��Ƿ�ͨ�ض���ʾ��ť
        if (isInitialLevel)
        {
            button.gameObject.SetActive(true);
            return; // �˳�����
        }

        // ��⵱ǰ��ť�Ĺؿ���-1
        int previousLevel = Buttonlevel - 1;

        // ���ǰһ���ؿ��Ƿ�ͨ��
        if (LevelManagerStatic.IsLevelCompleted(previousLevel))
        {
            button.gameObject.SetActive(true); // ��ʾ��ť
        }
        else
        {
            button.gameObject.SetActive(false); // ���ذ�ť
        }
    }
}
