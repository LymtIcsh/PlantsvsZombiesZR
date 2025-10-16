using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class ImitaterCloud : MonoBehaviour
{
    [FormerlySerializedAs("�ɾ�ʹ��")] [Header("�ɾ�ʹ��")]
    public bool _achievementUtilization = false;

    public void Awake()
    {
        if(_achievementUtilization)
        {
            Camera c = FindFirstObjectByType<Camera>();
            gameObject.GetComponent<Canvas>().worldCamera = c;
        }
        
    }

    public void Disappear()
    {
            Destroy(gameObject);
        
        
    }
}
