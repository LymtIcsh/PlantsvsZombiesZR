using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImitaterCloud : MonoBehaviour
{
    public bool �ɾ�ʹ�� = false;

    public void Awake()
    {
        if(�ɾ�ʹ��)
        {
            Camera c = FindFirstObjectByType<Camera>();
            gameObject.GetComponent<Canvas>().worldCamera = c;
        }
        
    }

    public void disappear()
    {
            Destroy(gameObject);
        
        
    }
}
