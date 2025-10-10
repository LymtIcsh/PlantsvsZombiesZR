using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorManagement : MonoBehaviour
{
    public static CursorManagement instance;

    [Header("光标")]
    public static Texture2D[] cursorTexture2DAry = new Texture2D[2];
    void Start()
    {
        LoadAndInitializeCursor();
    }

/// <summary>
/// 加载并初始化光标
/// </summary>
    void LoadAndInitializeCursor()
    {
        cursorTexture2DAry[0] = Resources.Load<Texture2D>("Sprites/特殊/光标/CursorDefault");
        cursorTexture2DAry[1] = Resources.Load<Texture2D>("Sprites/特殊/光标/CursorClick");
        Cursor.SetCursor(cursorTexture2DAry[0], Vector2.zero, CursorMode.Auto);
    }

/// <summary>
/// 切换光标
/// </summary>
/// <param name="i"></param>
    public static void SwitchCursor(int i)
    {
        Cursor.SetCursor(cursorTexture2DAry[i], Vector2.zero, CursorMode.Auto);
    }
}
