using UnityEngine;
using System.Collections.Generic;
using UnityEngine.SceneManagement;


public enum PoolType//一定要往下写！！！不然会导致错误，不要插队写！！！
{
    Doom,//大毁灭菇爆炸
    CherryBigBoom,//大樱桃炸弹爆炸
    NormalCrater,
    SteelBomb,//空爆

    SteelBullet,

    ZombieNormalHeadDrop,
    ZombieNormalArmDrop,
    ZombiePendantDrop,

    Fire,
}

[System.Serializable]
public class PoolItem
{
    public PoolType type;
    public GameObject prefab;
    public int initialSize = 10;
}

public class DynamicObjectPoolManager : MonoBehaviour
{
    public static DynamicObjectPoolManager Instance { get; private set; }

    // 在销毁或应用退出时置为 true
    internal bool IsShuttingDown { get; private set; }

    public List<PoolItem> poolPrefabs = new List<PoolItem>();
    public int defaultInitialSize = 10;
    private Dictionary<PoolType, Queue<GameObject>> poolDict = new Dictionary<PoolType, Queue<GameObject>>();
    private Dictionary<PoolType, GameObject> prefabDict = new Dictionary<PoolType, GameObject>();

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }



    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            // 不要 DestroyOnLoad，这里假设宿主物体已设置
            InitializePools();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void OnApplicationQuit()
    {
        IsShuttingDown = true;
    }

    private void OnDestroy()
    {
        IsShuttingDown = true;
    }

    private void InitializePools()
    {
        foreach (var item in poolPrefabs)
            RegisterPrefab(item.type, item.prefab, item.initialSize);
    }

    /// <summary>
    /// 注册或动态创建一个池
    /// </summary>
    public void RegisterPrefab(PoolType type, GameObject prefab, int initialSize = -1)
    {
        if (prefabDict.ContainsKey(type))
            return;

        int size = initialSize > 0 ? initialSize : defaultInitialSize;
        var queue = new Queue<GameObject>();

        for (int i = 0; i < size; i++)
        {
            var obj = Instantiate(prefab);
            obj.SetActive(false);
            // 挂到管理器 GameObject 下
            obj.transform.SetParent(this.transform, false);
            AttachWatcher(obj, type);
            queue.Enqueue(obj);
        }

        poolDict[type] = queue;
        prefabDict[type] = prefab;
    }

    /// <summary>
    /// 从池中获取一个对象
    /// </summary>
    public GameObject GetFromPool(PoolType type)
    {
        if (!poolDict.ContainsKey(type))
        {
            Debug.LogWarning($"[DynamicObjectPoolManager] 对象池 {type} 不存在，请先 RegisterPrefab");
            return null;
        }

        GameObject obj;
        if (poolDict[type].Count > 0)
        {
            obj = poolDict[type].Dequeue();
        }
        else
        {
            obj = Instantiate(prefabDict[type]);
            obj.transform.SetParent(this.transform, false);
            AttachWatcher(obj, type);
        }

        obj.SetActive(true);
        return obj;
    }

    /// <summary>
    /// 将对象归还到池
    /// </summary>
    public void ReturnToPool(PoolType type, GameObject obj)
    {
        if (!poolDict.ContainsKey(type))
        {
            Destroy(obj);
            return;
        }

        obj.SetActive(false);
        obj.transform.SetParent(this.transform, false);
        poolDict[type].Enqueue(obj);
    }

    private void AttachWatcher(GameObject obj, PoolType type)
    {
        var watcher = obj.GetComponent<PooledObjectWatcher>();
        if (watcher == null) watcher = obj.AddComponent<PooledObjectWatcher>();
        watcher.poolType = type;
    }

    /// <summary>
    /// 当池内对象被意外销毁时，补充池容量
    /// </summary>
    public void HandleDestroyed(PoolType type)
    {
        // 如果正在退出或者 Manager 自身已销毁，就跳过
        if (IsShuttingDown || Instance == null)
            return;

        if (!prefabDict.ContainsKey(type)) return;

        var obj = Instantiate(prefabDict[type]);
        obj.SetActive(false);
        obj.transform.SetParent(this.transform, false);
        AttachWatcher(obj, type);
        poolDict[type].Enqueue(obj);
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // 遍历所有子物体，把活跃的池化对象都禁用
        // 禁用后，它们会通过 PooledObjectWatcher 延迟回池
        for (int i = 0; i < transform.childCount; i++)
        {
            var child = transform.GetChild(i).gameObject;
            if (child.activeSelf)
                child.SetActive(false);
        }
    }
}
