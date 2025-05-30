using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ZombieManagement : MonoBehaviour
{
    public static ZombieManagement instance;
    public GameObject 旗帜控制;

    //僵尸相关
    private GameObject[] zombies;  // 可生成僵尸列表
    Dictionary<string, int> zombiesName = new Dictionary<string, int>();  // 僵尸名称与对象索引的字典


    //僵尸生成相关
    List<int> rowList = new List<int>();  // 可生成僵尸行列表

    //关卡数据
    public TimeNodes timeNodes;  //从 JSON 读取的时间节点列表
    public int nowNode_index = 0;  // 当前时间节点索引
    public int nodeCount = 0;      // 总时间节点数
    public TimeNode nowNode;      // 当前时间节点
    public bool waitWave = false; // 是否正在等待下一波僵尸
    public bool waitForZombieClear = false; // 是否等待场上僵尸清零
    public bool isOver = false;   // 游戏是否结束
    public int willTimeIndex;
    public DecreasingSlider flagMeter;  // 关卡进度条组件
    private Coroutine _cancelClimaxCoroutine;


    public GameObject tombstone;  // 灌木丛对象

    public  GameObject[] Bushes ;


    public Caption caption;  // 字幕组件

    public float totalWeight;
    public int totalWaves;

    int lastWaveIndex = -1;//标记，不用管

    public static List<GameObject> 场上僵尸 = new List<GameObject>();
    public static List<Zombie> allZombies = new List<Zombie>();//无论是否魅惑  
    public int ZombieCount;

    public float spawnInterval = 0.1f;

    private void Awake()
    {
        zombies = Resources.LoadAll<GameObject>("Prefabs/Zombies");
        场上僵尸.Clear();
        allZombies.Clear();
    }

    private void CalculateTotal()
    {

        willTimeIndex = -1;
        totalWeight = 0;
        foreach(ZombieWeight zombieWeight in timeNodes.zombieWeights)
        {
            totalWeight += zombieWeight.weight;
        }

        totalWaves = 0;

        for (int i = 0; i < timeNodes.info.Count; i++)
        {
            TimeNode timeNode = timeNodes.info[i];
            if (timeNode.isWave)
            {
                lastWaveIndex = i;
            }
        }

        for (int i = 0; i < timeNodes.info.Count; i++)
        {
            TimeNode timeNode = timeNodes.info[i];
            if (i == lastWaveIndex)
            {
                timeNode.isFinalWave = true;
            }
            else
            {
                timeNode.isFinalWave = false;
            }

            if (timeNode.isWave || timeNode.isFinalWave)
            {
                totalWaves += 1;
            }
        }
        if(totalWaves > 0)
        {
            GetChildByName(旗帜控制, totalWaves.ToString()).SetActive(true);
        }
        else
        {
            flagMeter.gameObject.SetActive(false);
        }
        
        
        Debug.Log("识别到总波数" + totalWaves);
    }

    void Start()
    {
        instance = this;
        //读取关卡 JSON 文件并转换为变量对象
        string info = Resources.Load<TextAsset>("Json/ZombieData/Level" + GameManagement.levelData.level).text;
        Debug.Log(info);
        timeNodes = JsonUtility.FromJson<TimeNodes>(info);
        CalculateTotal();
        //初始化可生成的僵尸的名称列表
        for (int i = 0; i < zombies.Length; i++)
        {
            zombiesName.Add(zombies[i].name, i);
        }

        //初始化僵尸生成相关
        initRowList();

        //初始化时间轴相关
        nodeCount = timeNodes.info.Count;
        nowNode_index = 0;
        nowNode = timeNodes.info[nowNode_index];

     
    }

    public void activate()
    {
        
        //延迟3秒后调用实际的激活逻辑
        Invoke("activateAfterDelay", 5f);
    }

    private void activateAfterDelay()
    {
        Debug.Log("5");
        场上僵尸.Clear();
        allZombies.Clear();
        //准备第一波
        Invoke("enterTimeNode", nowNode.deltaTime);
    }

    private void enterTimeNode()
    {
        if(!GameManagement.instance.游戏进行)
            return;
        if (nowNode.isWave == false)   //不是一波
        {
            generateZombies();
        }
        else   //是一波
        {
            if (场上僵尸.Count == 0)   //场上僵尸清零后才产生一波
            {
                waitWave = false;
                if (nowNode.isFinalWave == false) caption.showWave();
                else caption.showFinalWave();
                generateZombies();
            }
            else
            {
                waitWave = true;
            }
        }
    }

    // 把原来的 generateZombies 改为启动一个 Coroutine
    public void generateZombies()
    {
        if (!this.isActiveAndEnabled || !GameManagement.instance.游戏进行) return;

        // 1. 先算出这波要生成多少只
        int zombiesForThisWave = CalculateZombiesCount();

        // 2. 调用协程，逐个生成
        StartCoroutine(SpawnZombiesCoroutine(zombiesForThisWave));

        // 3. 换节点逻辑依然在这里触发
        changeTimeNode();
    }

    private IEnumerator SpawnZombiesCoroutine(int totalCount)
    {
        // 确保行列表已初始化
        if (rowList.Count == 0) initRowList();

        for (int i = 0; i < totalCount; i++)
        {
            // 按权重选出 prefab
            GameObject prefab = GenerateZombieByWeight();

            // 随机行
            int randY = rowList[Random.Range(0, rowList.Count)];
            rowList.Remove(randY);
            if (rowList.Count == 0) initRowList();

            // 实例化
            GameObject newZombie = Instantiate(
                prefab,
                new Vector3(6.0f, GameManagement.levelData.zombieInitPosY[randY], 0),
                Quaternion.identity,
                transform
            );
            SetZombieRowAndAddCount(newZombie, randY);

            // 等一下再生成下一只
            yield return new WaitForSeconds(spawnInterval);
        }
    }

    /// <summary>
    /// 把你原来一大堆算数的那段代码抽成一个方法
    /// </summary>
    private int CalculateZombiesCount()
    {
        float zombieMultiple = (float)timeNodes.totalZombies / nodeCount;
        float countFloat = zombieMultiple * (nowNode_index + 1);

        if (!nowNode.isWave && !nowNode.isFinalWave)
            countFloat *= 0.6f;

        switch (GameManagement.GameDifficult)
        {
            case 1: countFloat /= 4; break;
            case 2:
            case 3: countFloat = countFloat / 4 * 3; break;
        }
        if (nowNode.isFinalWave)
            countFloat *= 3;

        int count = Mathf.Max(1, (int)countFloat);
        return count;
    }


    public GameObject GenerateZombieByWeight()
    {
        float rand = Random.Range(0f, totalWeight);
        float cumulativeWeight = 0f;

        if(SpawnByEnv() != null)
        {
            return SpawnByEnv();
        }

        foreach (ZombieWeight zombieWeight in timeNodes.zombieWeights)
        {
            cumulativeWeight += zombieWeight.weight;
            if (rand < cumulativeWeight)
            {
                return zombies[zombiesName[zombieWeight.name]];
            }
        }

        return zombies[0]; //默认返回第一个僵尸
    }

    public GameObject SpawnByEnv()
    {
        // 只在前 1/5 且为普通种植关（CommonPlanting）时做这段逻辑
        if (nowNode_index <= nodeCount / 5 && GameManagement.levelData.LevelType == levelType.CommonPlanting)
        {
            string targetName;
            if (GameManagement.levelData.levelEnviornment == "Forest")
            {
                targetName = "ForestZombie";
            }
            else if(GameManagement.levelData.levelEnviornment == "Forest")
            {
                targetName = "SteelZombie";
            }
            else
            {
                targetName = "ZombieNormal";
            }

            return GetZombieByName(targetName);
        }

        return null;
    }

    public GameObject GenerateZombieInlevel()
    {
        int randomIndex = Random.Range(0, timeNodes.zombieWeights.Count);
        ZombieWeight randomZombieWeight = timeNodes.zombieWeights[randomIndex];
        return zombies[zombiesName[randomZombieWeight.name]];
    }

    private void SetZombieRowAndAddCount(GameObject newZombie, int row)
    {
        //var zombie = newZombie.GetComponent<Zombie>();
        //if (zombie != null)
        //{
        //    zombie.setPosRow(row);
        //}
        //else
        //{
            Zombie zombieGeneric = newZombie.GetComponent<Zombie>();
            if (zombieGeneric != null)
            {
                zombieGeneric.setPosRow(row);
            }
        //}
    }

    private void changeTimeNode()
    {
        if (nowNode_index == 0)
        {
            AudioManager.Instance.PlaySoundEffect(51);
            InvokeRepeating("groan", 0, 5);   //每隔5秒执行僵尸叹息音效函数
        }
        nowNode_index++;
        if (nowNode_index < nodeCount)   //后面还有时间节点，就切换
        {
            nowNode = timeNodes.info[nowNode_index];
            
            Invoke("enterTimeNode", nowNode.deltaTime);
            
        }
        else
        {
            isOver = true;
        }

        flagMeter.setValue((nodeCount - nowNode_index) / (float)nodeCount);
    }

    private void CheckMusic()
    {
        if (GameManagement.instance.lockMusic)
            return;

        if (!this.isActiveAndEnabled) return;

        var musicControl = GameManagement.instance.background.GetComponent<BGMusicControl>();
        int zombieCount = 场上僵尸.Count;

        if(zombieCount > 3)
        {
            if (_cancelClimaxCoroutine != null)
            {
                StopCoroutine(_cancelClimaxCoroutine);
                _cancelClimaxCoroutine = null;
            }
        }

        // 1. 进入高潮：立即切换，并如果之前有延迟取消的协程，先停掉它
        if (zombieCount >= 10 && !musicControl.isClimax)
        {
            musicControl.isClimax = true;
            musicControl.changeMusicSmoothly("Music_Climax");
        }
        // 2. 退出高潮：如果满足条件且还没在倒计时，就启动一个 10s 延迟协程
        else if (zombieCount <= 3 && musicControl.isClimax)
        {
            if (_cancelClimaxCoroutine == null)
                _cancelClimaxCoroutine = StartCoroutine(DelayedCancelClimax());
        }
    }

    // 延迟 10 秒后再真正取消高潮音乐
    private IEnumerator DelayedCancelClimax()
    {
        if (!this.isActiveAndEnabled) yield break;

        yield return new WaitForSeconds(10f);

        var musicControl = GameManagement.instance.background.GetComponent<BGMusicControl>();
        // 如果这 10 秒间又切到普通状态（或被外部逻辑改掉），就不再切换
        if (!musicControl.isClimax)
        {
            _cancelClimaxCoroutine = null;
            yield break;
        }

        musicControl.isClimax = false;
        musicControl.changeMusicSmoothly("Music" + GameManagement.levelData.backgroundSuffix);
        _cancelClimaxCoroutine = null;
    }


    private void groan()
    {
        int rand = Random.Range(1, 50);
        if (rand <= 6 && isOver == false)
        {
            AudioManager.Instance.PlaySoundEffectByName("groan" + rand);
        }
    }

    public void addZombieNumAll(GameObject zombie)
    {
        场上僵尸.Add(zombie);
        ZombieCount = 场上僵尸.Count;
        CheckMusic();
    }

    public void minusZombieNumAll(GameObject zombie)
    {
        if (!this.isActiveAndEnabled)
            return;
        try
        {
            if (zombie != null && this != null && gameObject != null && 场上僵尸 != null)
            {
                场上僵尸.Remove(zombie);
            }
            else if (场上僵尸 != null)
            {
                场上僵尸.RemoveAll(z => z == null);
            }
            else
            {
                return;
            }

            ZombieCount = 场上僵尸.Count;
            if (场上僵尸.Count <= 0)
            {
                for (int i = 0; i < 场上僵尸.Count; i++)
                {
                    if (场上僵尸[i] == null)
                    {
                        场上僵尸.RemoveAt(i);
                    }
                }
                if (waitWave)
                {
                    enterTimeNode();
                }
                else if (!waitWave && !isOver)
                {
                    CancelInvoke("enterTimeNode");
                    enterTimeNode();
                }
                else if (isOver)
                {
                    if (isOver && 场上僵尸.Count == 0)
                    {
                        GameManagement.instance.GetComponent<GameManagement>().win();
                    }
                }
            }
            CheckMusic();
        }
        finally
        {

        }
        
    }

    

    #region 关卡专用函数区域

    // 初始化可生成僵尸行列表
    private void initRowList()
    {
        for (int i = 0; i < GameManagement.levelData.landRowCount; i++)
        {
            rowList.Add(i);
        }
    }

 
    //获取指定父物体下的指定名字子物体
    public static GameObject GetChildByName(GameObject parent, string childName)
    {
        Transform[] allChildren = parent.GetComponentsInChildren<Transform>(true);

        foreach (Transform child in allChildren)
        {
            if (child.name == childName)
            {
                return child.gameObject;
            }
        }

        return null;
    }

    /// <summary>
    /// 根据名字从 zombies 数组中查找对应的预制体，没找到时 fallback 到第一个
    /// </summary>
    private GameObject GetZombieByName(string name)
    {
        var prefab = zombies.FirstOrDefault(z => z.name == name);
        if (prefab == null)
        {
            Debug.LogWarning($"找不到名为 '{name}' 的僵尸预制体，自动使用第一个：{(zombies.Length > 0 ? zombies[0].name : "null")}");
            prefab = zombies.Length > 0 ? zombies[0] : null;
        }
        return prefab;
    }


    [System.Serializable]
    public class ZombieType
    {
        public string name;
        public float weight;

        public ZombieType(string name, float weight)
        {
            this.name = name;
            this.weight = weight;
        }
    }



    #endregion
}

[System.Serializable]
public class ZombieWeight
{
    public string name;  // 僵尸类型名称
    public float weight; // 僵尸权重
}

[System.Serializable]
public class TimeNode
{
    public bool isWave;
    public float deltaTime;
    public bool isFinalWave;
}

[System.Serializable]
public class TimeNodes
{
    public int totalZombies;
    public List<TimeNode> info;   // 每波的时间信息
    public List<ZombieWeight> zombieWeights;  // 每个僵尸的权重
}