using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorManagement : MonoBehaviour
{
    public static CursorManagement instance;

    [Header("���")]
    public static Texture2D[] cursorTexture2DAry = new Texture2D[2];
    void Start()
    {
        LoadAndInitializeCursor();
    }

/// <summary>
/// ���ز���ʼ�����
/// </summary>
    void LoadAndInitializeCursor()
    {
        cursorTexture2DAry[0] = Resources.Load<Texture2D>("Sprites/����/���/CursorDefault");
        cursorTexture2DAry[1] = Resources.Load<Texture2D>("Sprites/����/���/CursorClick");
        Cursor.SetCursor(cursorTexture2DAry[0], Vector2.zero, CursorMode.Auto);
    }

/// <summary>
/// �л����
/// </summary>
/// <param name="i"></param>
    public static void SwitchCursor(int i)
    {
        Cursor.SetCursor(cursorTexture2DAry[i], Vector2.zero, CursorMode.Auto);
    }
}
