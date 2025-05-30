using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SteelSignController : MonoBehaviour
{
    public static SteelSignController Instance { get; private set; }

    public List<SteelSignEntry> signEntries;

    private Dictionary<SteelSign, GameObject> _signMap;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        _signMap = new Dictionary<SteelSign, GameObject>(signEntries.Count);
        foreach (var entry in signEntries)
        {
            if (!_signMap.ContainsKey(entry.type))
                _signMap.Add(entry.type, entry.prefab);
            else
                Debug.LogWarning($"你tm写重枚举了：{entry.type}");
        }
    }

    public void ActivateSteelSign(SteelSign steelSign)
    {
        GameObject currentSign = GetSignObject(steelSign);
        if (currentSign != null)
        {
            currentSign.SetActive(false);
            currentSign.SetActive(true);
        }
    }

    /// <summary>
    /// 通过枚举获取对应物体
    /// </summary>
    public GameObject GetSignObject(SteelSign type)
    {
        if (_signMap.TryGetValue(type, out var go))
            return go;
        Debug.LogError($"未找到类型 {type} 对应的物体！");
        return null;
    }
}
public enum SteelSign
{
    LargeBombing,
    SmallBombing,
    KillZombies,
    UsingCore_0,
    UsingCore_1,
    UsingCore_2,
    UsingCore_3,
}

[System.Serializable]
public class SteelSignEntry
{
    public SteelSign type;       // 枚举值
    public GameObject prefab;    // 对应的场景物体或预制件
}
