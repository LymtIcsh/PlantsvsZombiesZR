using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorManagement : MonoBehaviour
{
    public static CursorManagement instance;

    public static Texture2D[] ��� = new Texture2D[2];
    void Start()
    {
        ���ز���ʼ�����();
    }


    void ���ز���ʼ�����()
    {
        ���[0] = Resources.Load<Texture2D>("Sprites/����/���/CursorDefault");
        ���[1] = Resources.Load<Texture2D>("Sprites/����/���/CursorClick");
        Cursor.SetCursor(���[0], Vector2.zero, CursorMode.Auto);
    }

    public static void �л����(int i)
    {
        Cursor.SetCursor(���[i], Vector2.zero, CursorMode.Auto);
    }
}
