using UnityEngine;
using System.Collections.Generic;
using UnityEngine.SceneManagement;


public enum PoolType//һ��Ҫ����д��������Ȼ�ᵼ�´��󣬲�Ҫ���д������
{
    Doom,//����𹽱�ը
    CherryBigBoom,//��ӣ��ը����ը
    NormalCrater,
    SteelBomb,//�ձ�

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

    // �����ٻ�Ӧ���˳�ʱ��Ϊ true
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
            // ��Ҫ DestroyOnLoad�����������������������
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
    /// ע���̬����һ����
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
            // �ҵ������� GameObject ��
            obj.transform.SetParent(this.transform, false);
            AttachWatcher(obj, type);
            queue.Enqueue(obj);
        }

        poolDict[type] = queue;
        prefabDict[type] = prefab;
    }

    /// <summary>
    /// �ӳ��л�ȡһ������
    /// </summary>
    public GameObject GetFromPool(PoolType type)
    {
        if (!poolDict.ContainsKey(type))
        {
            Debug.LogWarning($"[DynamicObjectPoolManager] ����� {type} �����ڣ����� RegisterPrefab");
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
    /// ������黹����
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
    /// �����ڶ�����������ʱ�����������
    /// </summary>
    public void HandleDestroyed(PoolType type)
    {
        // ��������˳����� Manager ���������٣�������
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
        // �������������壬�ѻ�Ծ�ĳػ����󶼽���
        // ���ú����ǻ�ͨ�� PooledObjectWatcher �ӳٻس�
        for (int i = 0; i < transform.childCount; i++)
        {
            var child = transform.GetChild(i).gameObject;
            if (child.activeSelf)
                child.SetActive(false);
        }
    }
}
