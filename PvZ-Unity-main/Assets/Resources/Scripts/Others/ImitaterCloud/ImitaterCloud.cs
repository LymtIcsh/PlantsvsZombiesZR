using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImitaterCloud : MonoBehaviour
{
    public bool 成就使用 = false;

    public void Awake()
    {
        if(成就使用)
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
