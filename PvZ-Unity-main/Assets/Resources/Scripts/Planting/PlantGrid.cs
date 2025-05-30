using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlantGrid : MonoBehaviour
{
    #region 变量

    public int row;   //在第几行

    GameObject toBePlanted;   //To Be Planted对象
    public GameObject selectedShovel;        //SelectedShovel对象
    public GameObject selectedGlove;        //Glove对象

    public SpriteRenderer spriteRenderer;  //自身SpriteRenderer组件

    public bool havePlanted = false;   //该格是否已种植植物
    public GameObject nowPlant;    //当前所种植物
    public GameObject gameManagement;

    public GameObject 种植特效;
    public GameObject[] 变化特效;

    public string EnvironmentString=null;//用于作为植物变种的字符串，为了以后按格子变换植物，不再只是按地图变换

    // 坑洞相关
    private bool hasCrater = false;             // 标记当前格子上是否存在坑洞
    private GameObject craterInstance;          // 当前坑洞实例


    #endregion

    #region 系统消息


    private void Awake()
    {
        gameManagement = GameManagement.instance.gameObject;
        selectedShovel = gameManagement.GetComponent<GameManagement>().Shovel;
        selectedGlove = gameManagement.GetComponent<GameManagement>().Glove;
        种植特效 = gameManagement.GetComponent<GameManagement>().种植特效;
        变化特效 = gameManagement.GetComponent<GameManagement>().变化特效;

        spriteRenderer = GetComponent<SpriteRenderer>();

        
    }

    private void Start()
    {
        toBePlanted = ToBePlanted.instance.gameObject;
        if (EnvironmentString.Length==0)//若不自己填入，默认按照leveldata的环境来
        {
            EnvironmentString = GameManagement.levelData.levelEnviornment;
        }
    }


    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Mouse1))
        {
            spriteRenderer.sprite = null;
        }
    }

    private void OnMouseEnter()
    {
        if(hasCrater)
        {
            return;
        }
        if(havePlanted == false && toBePlanted.activeSelf == true && !PlantStructManager.GetPlantStructByName(toBePlanted.GetComponent<ToBePlanted>().plantName).IsPurpleCard)
        {
            PlantStruct plantStruct = PlantStructManager.GetPlantStructByName(toBePlanted.GetComponent<ToBePlanted>().plantName);
            spriteRenderer.sprite = toBePlanted.GetComponent<SpriteRenderer>().sprite;
            if (GameManagement.levelData.LevelType==levelType.FaithHill && plantStruct.envType != EnvironmentType.Phonograph)
            {
                List<GameObject> matchingPlants = FindMatchingPlants();
                Debug.Log(matchingPlants.Count);
                foreach (var plant in matchingPlants)
                {
                    if (!plant.gameObject.GetComponent<PlantGrid>().havePlanted)
                    {
                        plant.gameObject.GetComponent<PlantGrid>().spriteRenderer.sprite = toBePlanted.GetComponent<SpriteRenderer>().sprite;
                    }
                }
            }
        }
        else if(havePlanted == true && selectedGlove.activeSelf == true && StaticThingsManagement.glovePlant != null)
        {

        }

    }

    private void OnMouseExit()
    {
        if (havePlanted == false && toBePlanted.activeSelf == true)
        {
            spriteRenderer.sprite = null;
            if (GameManagement.levelData.LevelType==levelType.FaithHill)
            {
                List<GameObject> matchingPlants = FindMatchingPlants();
                foreach (var plant in matchingPlants)
                {
                    plant.gameObject.GetComponent<PlantGrid>().spriteRenderer.sprite = null;
                }
            }
        }
        else if (havePlanted == false)
        {
            spriteRenderer.sprite = null;
        }
        else if (havePlanted == true && selectedShovel.activeSelf == true)
        {
        }
        else if (havePlanted == true && selectedGlove.activeSelf == true)
        {
        }
    }

    private void OnMouseDown()
    {
        if (hasCrater)
        {
            return;
        }
        PlantStruct plantStruct = PlantStructManager.GetPlantStructByName(toBePlanted.GetComponent<ToBePlanted>().plantName);
        if (havePlanted == false && toBePlanted.activeSelf == true && plantStruct.IsPurpleCard == false)
        {
            
            plant(plantStruct.plantName);
            Debug.Log(plantStruct.envType);
            if (GameManagement.levelData.LevelType == levelType.FaithHill && plantStruct.envType != EnvironmentType.Phonograph)
            {
                Debug.Log("不是留声机");
                List<GameObject> matchingPlants = FindMatchingPlants();
                foreach (var plant in matchingPlants)
                {
                    if (!plant.gameObject.GetComponent<PlantGrid>().havePlanted)
                    {
                        plant.gameObject.GetComponent<PlantGrid>().plant(plantStruct.plantName);
                    }
                }
            }
        }
        
        else if (havePlanted == true && selectedShovel.activeSelf == true)
        {
            nowPlant.GetComponent<Plant>().die("shovelPlant",nowPlant);
        }
        else if (havePlanted == true && toBePlanted.activeSelf == true && plantStruct.IsPurpleCard == true && plantStruct.BasePlantName == nowPlant.GetComponent<Plant>().plantStruct.plantName)
        {
            string nowPlantName = nowPlant.name;
            Destroy(nowPlant);
            plant(plantStruct.plantName);
            
        }
        else if (havePlanted == true && selectedGlove.activeSelf == true && StaticThingsManagement.glovePlant == null)
        {
            StaticThingsManagement.glovePlant = nowPlant;
        }
        else if (havePlanted == true && selectedGlove.activeSelf == true && StaticThingsManagement.glovePlant != null)
        {
            StaticThingsManagement.glovePlant = null;
        }
        else if (havePlanted == false && selectedGlove.activeSelf == true && StaticThingsManagement.glovePlant != null)
        {
            if(StaticThingsManagement.glovePlant != null)
            {
                AudioManager.Instance.PlaySoundEffect(32);
                Transform parentTransform = StaticThingsManagement.glovePlant.transform.parent;
                StaticThingsManagement.glovePlant.transform.SetParent(this.transform);
                parentTransform.GetComponent<PlantGrid>().havePlanted = false;
                parentTransform.GetComponent<PlantGrid>().nowPlant = null;
                havePlanted = true;
                nowPlant = StaticThingsManagement.glovePlant;
                StaticThingsManagement.glovePlant = null;
                
                nowPlant.transform.position = transform.position + new Vector3(0, 0, 5);
                nowPlant.GetComponent<Plant>().initialize(
                this,
                spriteRenderer.sortingLayerName,
                spriteRenderer.sortingOrder
                );

                GameManagement.instance.GloveUI倒计时.启动冷却();
            }
        }
    }

    #endregion

    #region 私有自定义函数

    #endregion

    #region 公有自定义函数

    public void plant(string Name)
    {
        if (hasCrater)
        {
            return;
        }

        String name =  GetNameFromJson(this.EnvironmentString, Name);
        
        spriteRenderer.sprite = null;   //隐藏虚影
        //if (GameManagement.levelData.LevelType==levelType.FaithHill)
        //{
        //    List<GameObject> matchingPlants = FindMatchingPlants();
        //    foreach (var plant in matchingPlants)
        //    {
        //        plant.gameObject.GetComponent<PlantGrid>().spriteRenderer.sprite = null;
        //    }
        //}

        havePlanted = true;   //已种植物

            //生成植物
            nowPlant = Instantiate(Resources.Load<GameObject>("Prefabs/Plants/" + name),
                                    transform.position + new Vector3(0, 0, 5),
                                    Quaternion.Euler(0, 0, 0),
                                    transform);
            nowPlant.GetComponent<Plant>().initialize(
                this,
                spriteRenderer.sortingLayerName,
                spriteRenderer.sortingOrder
            );

            if (name != null && name != Name)//植物变化时的特效
            {
                switch (EnvironmentString)
                {
                    case "Day":
                        Instantiate(变化特效[0], nowPlant.transform.position, Quaternion.identity);
                        break;
                    case "Forest":
                        Instantiate(变化特效[1], nowPlant.transform.position, Quaternion.identity);
                        break;
                    case "SnowIce":
                        Instantiate(变化特效[2], nowPlant.transform.position, Quaternion.identity);
                        break;
                    case "Forest_P":
                    Debug.Log(EnvironmentString);
                        Instantiate(变化特效[3], nowPlant.transform.position, Quaternion.identity);
                        break;
                    default: break;
                }

            }


        AudioManager.Instance.PlaySoundEffect(32);

        Vector3 currentPosition = transform.position;
        Vector3 spawnPosition = new Vector3(currentPosition.x, currentPosition.y - 0.374f, currentPosition.z);
        Instantiate(种植特效, spawnPosition, Quaternion.identity);
      
        //向PlantingManagement发送消息以处理UI相关事件
        PlantingManagement.instance.GetComponent<PlantingManagement>().plant();

        //if(GameManagement.levelData.LevelType==levelType.FaithHill)
        //{
        //    List<GameObject> matchingPlants = FindMatchingPlants();
        //    foreach (var plant in matchingPlants)
        //    {
        //        if(!plant.gameObject.GetComponent<PlantGrid>().havePlanted)
        //        {
        //            plant.gameObject.GetComponent<PlantGrid>().plantByGod(name,true);
        //        }
                
        //    }
        //}

    }

    //上帝模式种植，用于关卡开始对话生成参与对话的植物
    public GameObject plantByGod(string name)
    {
        if (hasCrater)
        {
            return null;
        }

        havePlanted = true;   //已种植物

        //生成植物
        nowPlant = Instantiate(Resources.Load<GameObject>("Prefabs/Plants/" + name),
                                          transform.position + new Vector3(0, 0, 5),
                                          Quaternion.Euler(0, 0, 0),
                                          transform);
        nowPlant.GetComponent<Plant>().initialize(
            this,
            spriteRenderer.sortingLayerName,
            spriteRenderer.sortingOrder
        );

        return nowPlant;
    }

    //上帝模式种植，可以进行变种
    public GameObject plantByGod(string name,bool 可以变种)
    {
        if (hasCrater)
        {
            return null;
        }

        if (可以变种)
        {
            name = GetNameFromJson(GameManagement.levelData.levelEnviornment, name);
        }
        havePlanted = true;   //已种植物

        //生成植物
        nowPlant = Instantiate(Resources.Load<GameObject>("Prefabs/Plants/" + name),
                                          transform.position + new Vector3(0, 0, 5),
                                          Quaternion.Euler(0, 0, 0),
                                          transform);
        nowPlant.GetComponent<Plant>().initialize(
            this,
            spriteRenderer.sortingLayerName,
            spriteRenderer.sortingOrder
        );

        return nowPlant;
    }

    public void plantDie(string reason,GameObject plantObject)
    {
        havePlanted = false;   //已没有植物
        if(reason == "shovelPlant")
        {
            AudioManager.Instance.PlaySoundEffect(33);
        }
        else if(reason == "beEated")
        {
            AudioManager.Instance.PlaySoundEffect(34);
        }
    }

    /// <summary>
    /// 外部调用，在本格子生成坑洞
    /// </summary>
    public void SpawnCrater()
    {
        if (hasCrater) return;

        // 从对象池取出坑洞
        craterInstance = DynamicObjectPoolManager.Instance.GetFromPool(PoolType.NormalCrater);

        // 设置坑洞位置（x-0.05, y-0.35）
        Vector3 craterPos = transform.position + new Vector3(-0.05f, -0.35f, 0);
        craterInstance.transform.position = craterPos;
        craterInstance.transform.rotation = Quaternion.identity;

        // 设置初始坑洞sprite
        SpriteRenderer craterSr = craterInstance.GetComponent<SpriteRenderer>();
        craterSr.sprite = Resources.Load<Sprite>("Sprites/Items/crater/crater_Normal_1");

        hasCrater = true;
        StartCoroutine(CraterLifeCycle());
    }

    /// <summary>
    /// 坑洞生命周期协程：90秒后换图，再90秒后归还池中
    /// </summary>
    private IEnumerator CraterLifeCycle()
    {
        SpriteRenderer craterSr = craterInstance.GetComponent<SpriteRenderer>();

        // 前90秒保持初始状态
        yield return new WaitForSeconds(90f);
        // 切换到第二阶段Sprite
        craterSr.sprite = Resources.Load<Sprite>("Sprites/Items/crater/crater_Normal_2");

        // 再过90秒后归还池中
        yield return new WaitForSeconds(90f);

        DynamicObjectPoolManager.Instance.ReturnToPool(PoolType.NormalCrater, craterInstance);
        craterInstance = null;
        hasCrater = false;
    }


    List<GameObject> FindMatchingPlants()//用于查找对应列所有植物
    {
        List<GameObject> matchingPlants = new List<GameObject>();

        string currentName = gameObject.name;

        string[] parts = currentName.Split('-');
        if (parts.Length != 3)
        {
            return matchingPlants;
        }


        string X1 = parts[1];
        string X2 = parts[2];


        GameObject[] allObjects = GameObject.FindGameObjectsWithTag("PlantGrid");

        foreach (var obj in allObjects)
        {
            string[] otherParts = obj.name.Split('-');
            if (otherParts.Length == 3)
            {
                string otherX1 = otherParts[1]; 
                string otherX2 = otherParts[2];

                if (otherX1 == X1 && otherX2 != X2)
                {
                    if(obj.GetComponent<PlantGrid>() != null && !obj.GetComponent<PlantGrid>().hasCrater)
                    {
                        matchingPlants.Add(obj);
                    }
                    
                }
            }
        }

        return matchingPlants;
    }

    public string GetNameFromJson(string enviornment, string raw)
    {
        Debug.Log(enviornment + raw);

        // 从 Resources 目录加载 JSON 文件
        TextAsset jsonFile = Resources.Load<TextAsset>("Json/EnviornmentPlants");

        // 检查文件是否成功加载
        if (jsonFile != null)
        {
            // 读取 JSON 文件内容
            string json = jsonFile.text;

            // 将 JSON 转换回对象列表
            List<Data> dataList = JsonUtility.FromJson<ListWrapper>(json).items;

            // 查找符合条件的对象
            foreach (var item in dataList)
            {
                if (item.Enviornment == enviornment && item.Raw == raw)
                {
                    Debug.Log(item.Name);
                    return item.Name; // 返回对应的 Name
                }
            }
        }

        // 如果没有找到符合的条目，则返回 Raw
        return raw;
}

    // 用于从 JSON 文件读取 List 数据

   

    // 用于初始化一个测试 JSON 文件
    public void WriteTestJson()
    {
        List<Data> dataList = new List<Data>
        {
            new Data { Name = "Apple", Enviornment = "A1", Raw = "RawData1" },
            new Data { Name = "Banana", Enviornment = "B1", Raw = "RawData2" }
        };

        ListWrapper listWrapper = new ListWrapper { items = dataList };

        string json = JsonUtility.ToJson(listWrapper, true);

    }

    [System.Serializable]
    public class ListWrapper
    {
        public List<Data> items;
    }

    [System.Serializable]
    public class Data
    {
        public string Name;
        public string Enviornment;
        public string Raw;
    }



    #endregion
}
