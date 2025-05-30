using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class 生成对应关卡管理 : MonoBehaviour
{
    [Header("UI 框、文本、标语")]
    public GameObject 关卡框;
    public Text 标语;

    [Header("布局参数")]
    public int cardWidth = 300;         // 卡片宽度
    public int 高度偏移 = 275;         // 行高间距
    public List<GameObject> Pages;     // 每页的容器

    [Header("背景图集")]
    public List<Sprite> 冒险模式背景;   // 草地白天/夜晚等

    private int nowPage = 0;

    void Start()
    {
        初始化关卡();
        切换到页面(nowPage);
    }

    private void 切换到页面(int pageIndex)
    {
        for (int i = 0; i < Pages.Count; i++)
            Pages[i].SetActive(i == pageIndex);
    }

    public void 初始化关卡()
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
        if (关卡返回代码.游戏模式 == 游戏模式.冒险模式)
        {
            标语.text = "冒险模式 - 自由选关";
            遍历冒险模式(关卡返回代码.冒险模式, ref col, ref row, ref page, maxCols, maxRows);
        }
        else if (关卡返回代码.游戏模式 == 游戏模式.小游戏模式)
        {
            标语.text = "迷你游戏";
            遍历小游戏模式(关卡返回代码.小游戏模式, ref col, ref row, ref page, maxCols, maxRows);
        }
        else if (关卡返回代码.游戏模式 == 游戏模式.环境模式)
        {
            标语.text = "环境模式";
            遍历环境模式(关卡返回代码.环境模式, ref col, ref row, ref page, maxCols, maxRows);
        }
    }

    private void 遍历冒险模式(
        Dictionary<int, 冒险信息> dict,
        ref int col, ref int row, ref int page,
        int maxCols, int maxRows)
    {
        foreach (var kv in dict)
        {
            int key = kv.Key;
            冒险信息 info = kv.Value;

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
            GameObject newCard = Instantiate(关卡框, Pages[page].transform);

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
                        info.Type == NormalGameType.草地白天
                            ? 冒险模式背景[0]
                            : 冒险模式背景[1];
                }

                // 通关标志
                if (n == "是否通关")
                {
                    child.gameObject.SetActive(LevelManagerStatic.IsLevelCompleted(key));
                }
            }

            // 布局
            var rt = newCard.GetComponent<RectTransform>();
            rt.anchoredPosition = new Vector2((col - 1) * cardWidth, -(row - 1) * 高度偏移);

            // 锁定 & 点击
            if (dict.ContainsKey(key - 1) && !LevelManagerStatic.IsLevelCompleted(key - 1))
                newCard.SetActive(false);
            else
                newCard.GetComponent<Button>()?
                       .onClick.AddListener(() => OnButtonClicked(key));
        }
    }

    private void 遍历小游戏模式(
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

            GameObject newCard = Instantiate(关卡框, Pages[page].transform);

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
            rt.anchoredPosition = new Vector2((col - 1) * cardWidth, -(row - 1) * 高度偏移);

                newCard.GetComponent<Button>()?
                       .onClick.AddListener(() => OnButtonClicked(key));
        }
    }

    private void 遍历环境模式(
        Dictionary<int, 环境信息> dict,
        ref int col, ref int row, ref int page,
        int maxCols, int maxRows)
    {
        foreach (var kv in dict)
        {
            int key = kv.Key;
            环境信息 info = kv.Value;

            if (key < 0)
            {
                col = maxCols;
                continue;
            }

            col++;
            if (col > maxCols) { col = 1; row++; }
            if (row > maxRows) { row = 1; page++; if (page >= Pages.Count) break; }

            GameObject newCard = Instantiate(关卡框, Pages[page].transform);

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
                            ? 冒险模式背景[2]
                            : 冒险模式背景[3];
                }

                if (n == "是否通关")
                    child.gameObject.SetActive(LevelManagerStatic.IsLevelCompleted(key));
            }

            var rt = newCard.GetComponent<RectTransform>();
            rt.anchoredPosition = new Vector2((col - 1) * cardWidth, -(row - 1) * 高度偏移);

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
        切换到页面(nowPage);
    }

    public void NextPage()
    {
        nowPage = (nowPage + 1) % Pages.Count;
        切换到页面(nowPage);
    }
}
