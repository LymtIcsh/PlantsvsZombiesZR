using System.Collections;
using UnityEngine;

public class PooledObjectWatcher : MonoBehaviour
{
    public PoolType poolType;

    private void OnDestroy()
    {
        // 防止在场景卸载、父物体销毁、App 退出时误调用
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
