using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class OpenLink : MonoBehaviour
{
    public string url = "https://space.bilibili.com/1956298381"; // ��������Ҫ��ת������

    // �����������Button���ʱ����
    public void OpenWebsite()
    {
        Application.OpenURL(url); // ʹ��Application.OpenURL������
    }
}

