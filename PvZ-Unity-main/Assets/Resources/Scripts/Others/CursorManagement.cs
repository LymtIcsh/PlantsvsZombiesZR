using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorManagement : MonoBehaviour
{
    public static CursorManagement instance;

    public static Texture2D[] 光标 = new Texture2D[2];
    void Start()
    {
        加载并初始化光标();
    }


    void 加载并初始化光标()
    {
        光标[0] = Resources.Load<Texture2D>("Sprites/特殊/光标/CursorDefault");
        光标[1] = Resources.Load<Texture2D>("Sprites/特殊/光标/CursorClick");
        Cursor.SetCursor(光标[0], Vector2.zero, CursorMode.Auto);
    }

    public static void 切换光标(int i)
    {
        Cursor.SetCursor(光标[i], Vector2.zero, CursorMode.Auto);
    }
}
