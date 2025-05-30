using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class OpenLink : MonoBehaviour
{
    public string url = "https://space.bilibili.com/1956298381"; // 设置你想要跳转的链接

    // 这个方法将被Button点击时调用
    public void OpenWebsite()
    {
        Application.OpenURL(url); // 使用Application.OpenURL打开链接
    }
}

