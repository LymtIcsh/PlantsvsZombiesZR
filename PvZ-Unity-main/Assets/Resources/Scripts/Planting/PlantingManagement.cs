using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Runtime.CompilerServices;
using UnityEngine;

public class PlantingManagement : MonoBehaviour
{
    #region 变量

    //待种植植物相关
    public static PlantingManagement instance;
    public GameObject toBePlanted_Object;
    ToBePlanted toBePlanted_Script;

    Card nowCard;  //当前所选植物的卡槽的Card
    public List<PlantGrid> plantgrids;
    #endregion

    #region 系统消息

    // Start is called before the first frame update
    void Start()
    {
        //获取游戏对象和组件
        toBePlanted_Script = toBePlanted_Object.GetComponent<ToBePlanted>();
        instance = this;
        GetGrids();
        Debug.Log(114513);
        CreateSomethingByLevel();
    }

    #endregion

    #region 私有自定义函数

    private void GetGrids()
    {//从子物体中获取格子
        Transform[] children = GetComponentsInChildren<Transform>();
        foreach (Transform child in children)
        {
            //child.name.EndsWith(row.ToString()) &&
            if (child.GetComponent<PlantGrid>() != null)
            {           
                // 可以对找到的子物体进行操作
                plantgrids.Add(child.GetComponent<PlantGrid>());
            }
        }
    }

    #endregion

    #region 公有自定义函数

    //点击按钮选择植物
    public void clickPlant(string plant, Card card)
    {
        nowCard = card;
        toBePlanted_Script.showPlantPreview(plant);
    }

    //种植植物
    public void plant()
    {
        if (GameManagement.instance.SunText != null && GameManagement.instance.SunText.isActiveAndEnabled)
        {
            GameManagement.instance.SunText.GetComponent<SunNumber>().subSun(nowCard.sunNeeded);
        }
        if (nowCard != null)
        {
            nowCard.cooling();
        }

    }

    //获取某行的的格子
    public List<PlantGrid> GetRowGrids(int row)
    {
        if (row >= GameManagement.levelData.rowCount || row < 0) { Debug.Log("数组越界："); return null; }
        List<PlantGrid> GotGrids = new List<PlantGrid>();
        foreach (PlantGrid grid in plantgrids)
        {
            if (grid.row == row)
            {
                GotGrids.Add(grid);
            }
        }
        return GotGrids;
    }
    //获取某列的格子
    public List<PlantGrid> GetColGrids(int col)
    {
        List<PlantGrid> GotGrids = new List<PlantGrid>();
        foreach (PlantGrid grid in plantgrids)
        {
            if (grid.name.StartsWith("Plant-" + col.ToString()))
            {
                GotGrids.Add(grid);
            }
        }
        return GotGrids;
    }

    //获取某个格子
    public PlantGrid GetTheGrid(int row,int col) {
        List<PlantGrid> GotGrids = GetRowGrids(row);
        foreach (PlantGrid grid in GotGrids)
        {
            if (grid.row == row)
            {
                if (grid.name.StartsWith("Plant-" + col.ToString()))
                {
                    return grid;
                }
            }
        }

        return null;
    }
    #endregion


    public void CreateSomethingByLevel()
    {//根据关卡生成一些东西
        Debug.Log(GameManagement.levelData.mapSuffix);
        if (GameManagement.levelData.levelEnviornment == "Forest")
        {
            generateBushes();
        }
        if (GameManagement.levelData.mapSuffix == "_Forest_P") {
            Debug.Log("确定了污染环境");
            for (int i = 0; i < 25; i++) {
                int randRow = Random.Range(0, GameManagement.levelData.landRowCount);
                int randCol = Random.Range(0, 8);
                GetTheGrid(randRow, randCol).EnvironmentString = "Forest_P";

            }

            InvokeRepeating("AttackPlant",0,5);
        }
    }

    private void AttackPlant()
    {//攻击
        for (int i = 0; i < PlantManagement.场上植物.Count; i++) {

            PlantManagement.场上植物[i].GetComponent<Plant>().beAttacked(
                PlantManagement.场上植物[i].GetComponent<Plant>().最大血量/20
                , null, null);
        }
       
    }



    public void generateBushes()
    {

        int min = Random.Range(1, (int)GameManagement.GameDifficult);
        int max = Random.Range((int)GameManagement.GameDifficult,
            (int)GameManagement.GameDifficult + 3);
        // 第一次直接生成灌木丛
        GenerateBushes(min, max);
        Invoke("generateBushes",60f);
        
    }

    private void GenerateBushes(int minBushes, int maxBushes)
    {
        if(!GameManagement.levelData.EnablesForestBushGeneration)
        {
            return;
        }

        // 确保生成的灌木丛数量在最小和最大范围之间
        int gravesToGenerate = Random.Range(minBushes, maxBushes + 1);

        // 获取所有带有 "PlantGrid" 标签的物体（就是种植格子）
        GameObject[] plantGrids = GameObject.FindGameObjectsWithTag("PlantGrid");

        // 随机生成灌木丛
        for (int i = 0; i < gravesToGenerate; i++)
        {
            // 如果找到了至少一个带有 PlantGrid 标签的物体
            if (plantGrids.Length > 0)
            {
                // 随机选择一个 PlantGrid 物体
                int randRow = Random.Range(0, GameManagement.levelData.rowCount);
                int randCol = Random.Range(4, 9);
                PlantGrid randomPlantGrid = GetTheGrid(randRow, randCol);
                print("生成灌木丛");
                // 检查该父物体下是否已有灌木丛
                if (!HasBushesUnderParent(randomPlantGrid.gameObject))
                {
                    // 获取父物体的位置
                    Vector3 parentPosition = randomPlantGrid.transform.position;

                    // 生成灌木丛对象
                    int rand = Random.Range(0,ZombieManagement.instance.Bushes.Length);
                    GameObject newBushes = Instantiate(ZombieManagement.instance.Bushes[rand],
                        parentPosition,
                        Quaternion.Euler(0, 0, 0),
                        this.transform);

                    // 将灌木丛设置为随机选择的 PlantGrid 物体的子物体
                    newBushes.transform.SetParent(randomPlantGrid.gameObject.transform);

                    // 设置灌木丛的位置为相对于父物体的 (0, 0, 0)
                    newBushes.transform.localPosition = Vector3.zero;

                    //设置灌木丛的行
                    newBushes.GetComponent<Bushes>().setPosRow(randomPlantGrid.row);

                }
            }
        }
    }
    // 检查父物体下是否已有灌木丛子物体
    private bool HasBushesUnderParent(GameObject parent)
    {
        // 遍历父物体的所有子物体，检查是否已经有灌木丛
        foreach (Transform child in parent.transform)
        {
            if (child.CompareTag("Tombstone"))  // 灌木丛的标签是 "Tombstone"
            {
                return true;  // 如果找到灌木丛，则返回 true
            }
        }
        return false;  // 没有灌木丛时返回 false
    }
    private IEnumerator GenerateBushesWithInterval(int minBushes, int maxBushes, int generateTimes, float generationInterval)
    {
        // 随后每次生成灌木丛时加入间隔
        for (int t = 0; t < generateTimes; t++)
        {
            // 延迟一段时间再生成灌木丛
            yield return new WaitForSeconds(generationInterval);

            // 生成灌木丛
            GenerateBushes(minBushes, maxBushes);
        }
    }

}
