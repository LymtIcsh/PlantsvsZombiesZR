using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AchievementCoroutineHandler : MonoBehaviour
{

    

    public void StartAchievementCoroutine(IEnumerator coroutine)
    {
        StartCoroutine(coroutine);  // ���������Э��
    }


    private static AchievementCoroutineHandler _instance;

    public static AchievementCoroutineHandler Instance
    {
        get
        {
            if (_instance == null)
            {
                // ���������û���������Բ�����
                _instance = FindFirstObjectByType<AchievementCoroutineHandler>();
                if (_instance == null)
                {
                    // ����������Ҳ���������һ���µ� GameObject �����ش˽ű�
                    GameObject obj = new GameObject("AchievementCoroutineHandler");
                    _instance = obj.AddComponent<AchievementCoroutineHandler>();
                }
            }
            return _instance;
        }
    }

    // ȷ�� Start �� Awake �е��߼�������ɴ���
    void Awake()
    {
        // ȷ��ֻ��һ��ʵ��
        if (_instance == null)
        {
            _instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

}
