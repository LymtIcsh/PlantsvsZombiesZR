using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;
using UnityEngine.UI;

/// <summary>
/// 生成对应关卡管理.
/// </summary>
public class LevelGenerationManager : MonoBehaviour
{
    [FormerlySerializedAs("关卡框")] [Header("UI 框、文本、标语")]
    public GameObject levelFrame;
    [FormerlySerializedAs("标语")] public Text sloganText;

    [Header("布局参数")]
    public int cardWidth = 300;  
    [FormerlySerializedAs("高度偏移")] [Header("高度偏移")]// 卡片宽度
    public int heightOffset = 275;         // 行高间距
    public List<GameObject> Pages;     // 每页的容器

    [Header("背景图集")]
    /// <summary>
    /// 冒险模式背景 - 存储不同模式的背景图集
    /// </summary>
    public List<Sprite> AdventureModeBackgrounds;   // 草地白天/夜晚等

    private int nowPage = 0;

    void Start()
    {
        InitializeLevels();
        SwitchToPage(nowPage);
    }

    /// <summary>
    /// 切换到页面
    /// </summary>
    /// <param name="pageIndex"></param>
    private void SwitchToPage(int pageIndex)
    {
        for (int i = 0; i < Pages.Count; i++)
            Pages[i].SetActive(i == pageIndex);
    }

    /// <summary>
    /// 初始化关卡
    /// </summary>
    public void InitializeLevels()
    {
        // 参数
        const int maxCols = 6;
        const int maxRows = 3;
        int col = 0, row = 1, page = 0;

        // 清空旧卡
        foreach (var pg in Pages)
            foreach (Transform t in pg.transform)
                Destroy(t.gameObject);

        // 根据模式选不同逻辑
        if (LevelReturnCode.CurrentGameMode == GameMode.AdventureMode)
        {
            sloganText.text = "冒险模式 - 自由选关";
            TraverseAdventureMode(LevelReturnCode.AdventureModeDict, ref col, ref row, ref page, maxCols, maxRows);
        }
        else if (LevelReturnCode.CurrentGameMode == GameMode.MiniGameMode)
        {
            sloganText.text = "迷你游戏";
            TraverseMiniGameMode(LevelReturnCode.MiniGameModeDict, ref col, ref row, ref page, maxCols, maxRows);
        }
        else if (LevelReturnCode.CurrentGameMode == GameMode.EnvironmentMode)
        {
            sloganText.text = "环境模式";
            TraverseEnvironmentMode(LevelReturnCode.EnvironmentModeDict, ref col, ref row, ref page, maxCols, maxRows);
        }
    }

    /// <summary>
    /// 遍历冒险模式
    /// </summary>
    /// <param name="dict"></param>
    /// <param name="col"></param>
    /// <param name="row"></param>
    /// <param name="page"></param>
    /// <param name="maxCols"></param>
    /// <param name="maxRows"></param>
    private void TraverseAdventureMode(
        Dictionary<int, AdventureInfo> dict,
        ref int col, ref int row, ref int page,
        int maxCols, int maxRows)
    {
        foreach (var kv in dict)
        {
            int key = kv.Key;
            AdventureInfo info = kv.Value;

            // 负 Key → 换行
            if (key < 0)
            {
                col = maxCols;
                continue;
            }

            // 移动指针
            col++;
            if (col > maxCols) { col = 1; row++; }
            if (row > maxRows) { row = 1; page++; if (page >= Pages.Count) break; }

            // 实例化
            GameObject newCard = Instantiate(levelFrame, Pages[page].transform);

            // 文本
            var txt = newCard.GetComponentInChildren<Text>();
            if (txt != null) txt.text = info.Name;

            // 填充子节点
            var plantList = PlantStructManager.GetPlantStructsByGetLevel(key);
            foreach (var child in newCard.GetComponentsInChildren<Transform>(true))
            {
                string n = child.name;
                // 单植物
                if (plantList.Count == 1 && n == "显示植物")
                {
                    child.gameObject.SetActive(true);
                    var sp = Resources.Load<Sprite>($"Sprites/Plants/{plantList[0].plantName}");
                    if (sp != null) child.GetComponent<SpriteRenderer>().sprite = sp;
                }
                else if (plantList.Count == 1 && n == "显示植物2")
                {
                    child.gameObject.SetActive(false);
                }
                // 多植物
                else if (plantList.Count >= 2 && n == "显示植物")
                {
                    child.gameObject.SetActive(false);
                }
                else if (plantList.Count >= 2 && n == "显示植物2")
                {
                    child.gameObject.SetActive(true);
                    int idx = 0;
                    foreach (Transform sub in child)
                    {
                        if (idx < plantList.Count)
                        {
                            var sp = Resources.Load<Sprite>($"Sprites/Plants/{plantList[idx].plantName}");
                            if (sp != null) sub.GetComponent<SpriteRenderer>().sprite = sp;
                        }
                        idx++;
                    }
                }
                // 无植物
                else if (plantList.Count == 0 && n == "显示植物")
                {
                    child.gameObject.SetActive(true);
                }

                // 背景
                if (n == "显示背景")
                {
                    child.GetComponent<SpriteRenderer>().sprite =
                        info.Type == NormalGameType.GrasslandDay
                            ? AdventureModeBackgrounds[0]
                            : AdventureModeBackgrounds[1];
                }

                // 通关标志
                if (n == "是否通关")
                {
                    child.gameObject.SetActive(LevelManagerStatic.IsLevelCompleted(key));
                }
            }

            // 布局
            var rt = newCard.GetComponent<RectTransform>();
            rt.anchoredPosition = new Vector2((col - 1) * cardWidth, -(row - 1) * heightOffset);

            // 锁定 & 点击
            if (dict.ContainsKey(key - 1) && !LevelManagerStatic.IsLevelCompleted(key - 1))
                newCard.SetActive(false);
            else
                newCard.GetComponent<Button>()?
                       .onClick.AddListener(() => OnButtonClicked(key));
        }
    }

    /// <summary>
    /// 遍历小游戏模式
    /// </summary>
    /// <param name="dict"></param>
    /// <param name="col"></param>
    /// <param name="row"></param>
    /// <param name="page"></param>
    /// <param name="maxCols"></param>
    /// <param name="maxRows"></param>
    private void TraverseMiniGameMode(
        Dictionary<int, string> dict,
        ref int col, ref int row, ref int page,
        int maxCols, int maxRows)
    {
        foreach (var kv in dict)
        {
            int key = kv.Key;
            string name = kv.Value;

            if (key < 0)
            {
                col = maxCols;
                continue;
            }

            col++;
            if (col > maxCols) { col = 1; row++; }
            if (row > maxRows) { row = 1; page++; if (page >= Pages.Count) break; }

            GameObject newCard = Instantiate(levelFrame, Pages[page].transform);

            var txt = newCard.GetComponentInChildren<Text>();
            if (txt != null) txt.text = name;

            var plantList = PlantStructManager.GetPlantStructsByGetLevel(key);
            var zombieList = ZombieStructManager.GetZombieStructByLevel(key);
            foreach (var child in newCard.GetComponentsInChildren<Transform>(true))
            {
                string n = child.name;
                if (plantList.Count == 1 && n == "显示植物")
                {
                    child.gameObject.SetActive(true);
                    var sp = Resources.Load<Sprite>($"Sprites/Plants/{plantList[0].plantName}");
                    if (sp != null) child.GetComponent<SpriteRenderer>().sprite = sp;
                }
                else if (plantList.Count == 1 && n == "显示植物2")
                {
                    child.gameObject.SetActive(false);
                }
                else if (plantList.Count >= 2 && n == "显示植物")
                {
                    child.gameObject.SetActive(false);
                }
                else if (plantList.Count >= 2 && n == "显示植物2")
                {
                    child.gameObject.SetActive(true);
                    int idx = 0;
                    foreach (Transform sub in child)
                    {
                        if (idx < plantList.Count)
                        {
                            
                            Sprite sp = Resources.Load<Sprite>($"Sprites/Plants/{plantList[idx].plantName}");
                            if (sp != null)
                            {
                                sub.GetComponent<SpriteRenderer>().sprite = sp;
                            }
                            
                        }
                        idx++;
                    }
                }
                else if (plantList.Count == 0 && n == "显示植物")
                {
                    if(zombieList.Count >= 1)
                    {
                        Sprite sprite = Resources.Load<Sprite>($"Sprites/Zombies/僵尸图鉴/{zombieList[0].zombieName}");
                        Debug.Log(sprite);
                        if (sprite != null)
                        {
                            child.GetComponent<SpriteRenderer>().sprite = sprite;
                        }
                    }
                    child.gameObject.SetActive(true);

                }

                if (n == "是否通关")
                    child.gameObject.SetActive(LevelManagerStatic.IsLevelCompleted(key));
            }

            var rt = newCard.GetComponent<RectTransform>();
            rt.anchoredPosition = new Vector2((col - 1) * cardWidth, -(row - 1) * heightOffset);

                newCard.GetComponent<Button>()?
                       .onClick.AddListener(() => OnButtonClicked(key));
        }
    }

    /// <summary>
    /// 遍历环境模式
    /// </summary>
    /// <param name="dict"></param>
    /// <param name="col"></param>
    /// <param name="row"></param>
    /// <param name="page"></param>
    /// <param name="maxCols"></param>
    /// <param name="maxRows"></param>
    private void TraverseEnvironmentMode(
        Dictionary<int, EnvironmentInfo> dict,
        ref int col, ref int row, ref int page,
        int maxCols, int maxRows)
    {
        foreach (var kv in dict)
        {
            int key = kv.Key;
            EnvironmentInfo info = kv.Value;

            if (key < 0)
            {
                col = maxCols;
                continue;
            }

            col++;
            if (col > maxCols) { col = 1; row++; }
            if (row > maxRows) { row = 1; page++; if (page >= Pages.Count) break; }

            GameObject newCard = Instantiate(levelFrame, Pages[page].transform);

            var txt = newCard.GetComponentInChildren<Text>();
            if (txt != null) txt.text = info.Name;

            var plantList = PlantStructManager.GetPlantStructsByGetLevel(key);
            foreach (var child in newCard.GetComponentsInChildren<Transform>(true))
            {
                string n = child.name;
                if (plantList.Count == 1 && n == "显示植物")
                {
                    child.gameObject.SetActive(true);
                    var sp = Resources.Load<Sprite>($"Sprites/Plants/{plantList[0].plantName}");
                    if (sp != null) child.GetComponent<SpriteRenderer>().sprite = sp;
                }
                else if (plantList.Count == 1 && n == "显示植物2")
                {
                    child.gameObject.SetActive(false);
                }
                else if (plantList.Count >= 2 && n == "显示植物")
                {
                    child.gameObject.SetActive(false);
                }
                else if (plantList.Count >= 2 && n == "显示植物2")
                {
                    child.gameObject.SetActive(true);
                    int idx = 0;
                    foreach (Transform sub in child)
                    {
                        if (idx < plantList.Count)
                        {
                            var sp = Resources.Load<Sprite>($"Sprites/Plants/{plantList[idx].plantName}");
                            if (sp != null) sub.GetComponent<SpriteRenderer>().sprite = sp;
                        }
                        idx++;
                    }
                }
                else if (plantList.Count == 0 && n == "显示植物")
                {
                    child.gameObject.SetActive(true);
                }

                if (n == "显示背景")
                {
                    child.GetComponent<SpriteRenderer>().sprite =
                        info.Type == EnvironmentType.Forest
                            ? AdventureModeBackgrounds[2]
                            : AdventureModeBackgrounds[3];
                }

                if (n == "是否通关")
                    child.gameObject.SetActive(LevelManagerStatic.IsLevelCompleted(key));
            }

            var rt = newCard.GetComponent<RectTransform>();
            rt.anchoredPosition = new Vector2((col - 1) * cardWidth, -(row - 1) * heightOffset);

            if (dict.ContainsKey(key - 1) && !LevelManagerStatic.IsLevelCompleted(key - 1))
                newCard.SetActive(false);
            else
                newCard.GetComponent<Button>()?
                       .onClick.AddListener(() => OnButtonClicked(key));
        }
    }

    public void OnButtonClicked(int key)
    {
        Debug.Log($"加载第 {key} 关");
        BeginManagement.level = key;
        SceneManager.LoadScene("GameScene");
    }

    public void LastPage()
    {
        nowPage = (nowPage - 1 + Pages.Count) % Pages.Count;
        SwitchToPage(nowPage);
    }

    public void NextPage()
    {
        nowPage = (nowPage + 1) % Pages.Count;
        SwitchToPage(nowPage);
    }
}
