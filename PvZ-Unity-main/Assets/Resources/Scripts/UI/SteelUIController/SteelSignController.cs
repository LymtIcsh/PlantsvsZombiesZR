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
                Debug.LogWarning($"��tmд��ö���ˣ�{entry.type}");
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
    /// ͨ��ö�ٻ�ȡ��Ӧ����
    /// </summary>
    public GameObject GetSignObject(SteelSign type)
    {
        if (_signMap.TryGetValue(type, out var go))
            return go;
        Debug.LogError($"δ�ҵ����� {type} ��Ӧ�����壡");
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
    public SteelSign type;       // ö��ֵ
    public GameObject prefab;    // ��Ӧ�ĳ��������Ԥ�Ƽ�
}
