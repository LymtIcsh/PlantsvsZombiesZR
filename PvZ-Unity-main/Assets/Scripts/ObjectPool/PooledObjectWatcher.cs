using System.Collections;
using UnityEngine;

public class PooledObjectWatcher : MonoBehaviour
{
    public PoolType poolType;

    private void OnDestroy()
    {
        // ��ֹ�ڳ���ж�ء����������١�App �˳�ʱ�����
        if (DynamicObjectPoolManager.Instance == null ||
            DynamicObjectPoolManager.Instance.IsShuttingDown)
        {
            return;
        }
        DynamicObjectPoolManager.Instance.HandleDestroyed(poolType);
    }

    private void OnDisable()
    {
        if (!gameObject.scene.IsValid() || DynamicObjectPoolManager.Instance == null)
            return;

        try
        {
            DynamicObjectPoolManager.Instance.ReturnToPool(poolType, gameObject);
        }
        finally
        {

        }
        
        
    }
}
