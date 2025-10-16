using UnityEngine;

public class FullscreenToggle : MonoBehaviour
{
    void Update()
    {
        // ��� F ���İ���du
        if (Input.GetKeyDown(KeyCode.F))
        {
            // �л�ȫ���ʹ��ڻ�ģʽ
            ToggleFullscreen();
        }
    }

    // �л�ȫ��ģʽ�ʹ���ģʽ
    void ToggleFullscreen()
    {
        // �жϵ�ǰ�Ƿ�ȫ������������л�Ϊ����ģʽ�������л�Ϊȫ��
        if (Screen.fullScreen)
        {
            Screen.fullScreen = false;  // �л�Ϊ���ڻ�
            Debug.Log("�л�Ϊ����ģʽ");
        }
        else
        {
            Screen.fullScreen = true;   // �л�Ϊȫ��
            Debug.Log("�л�Ϊȫ��ģʽ");
        }
    }
}
