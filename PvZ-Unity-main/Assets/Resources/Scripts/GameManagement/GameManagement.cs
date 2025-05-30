using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManagement : MonoBehaviour
{
    public GameObject 种植特效;
    public GameObject[] 变化特效;

    public static GameManagement instance; // 添加此行
    public static bool isPerformance = false;//是否开启性能优化
    public static bool 是否显示血量;
    public static float GameDifficult = 2.0f;
    public static float 局内游戏速度 = 1.0f;
    public static bool CollectSun = true;
    public bool 游戏进行;
    public bool lockMusic = false;

    public static int level;   //当前LevelNumber

    private LevelController levelController;
    public static LevelData levelData;   //当前关卡数据

    public List<GameObject> firstAwakeList;//先唤醒对象
    public List<GameObject> awakeList;  //待唤醒列表，用于开场剧情结束后唤醒该唤醒的对象

    public GameObject endMenuPanel;   //游戏结束面板
    public GameObject dialogUI;
    public GameObject background;   //背景对象
    public GameObject SettingButton;

    public GameObject zombieManagement;   //僵尸管理对象
    public ChooseCardManager ChooseCardManager;
    public GameObject uiManagement;   //UI管理对象
    public GameObject melonPollingObject; //西瓜保龄球控制
    public GameObject Glove;
    public GameObject Shovel;
    public GameObject 一行地图;
    public ForestSlider forestSlider;
    public ZombieForestSlider zombieForestSlider;
    public SunNumber SunText;
    public GameObject 树桩的梦想控制器;
    public GameObject 树桩的梦想手机适配;
    public GloveUI倒计时 GloveUI倒计时;
    public GameObject 卡槽;
    public GameObject[] TheDreamOfPotatoMine;
    public static GameManagement gameManagement;
    public Text 名称显示;
    public GameObject sunManagement;
    public void Awake()
    {
        instance = this;
        名称显示.text = LevelManagerStatic.GetCurrentSaveName() + "的房子";
        名称显示.gameObject.SetActive(true);
        gameManagement = gameObject.GetComponent<GameManagement>();
        if (SceneManager.GetActiveScene().name == "GameScene")
        {
            Time.timeScale = GameManagement.局内游戏速度;
        }
    }

    public void Start()
    {
        Debug.Log(BeginManagement.level);
        level = BeginManagement.level;
        levelController =
                (LevelController)gameObject.AddComponent(Type.GetType("LevelController"));
        levelController.init();

        background.GetComponent<SpriteRenderer>().sprite = null;
        Transform targetChild = background.transform.Find("DynamicBackGorund" + levelData.mapSuffix);
        if(targetChild != null)
        {
            targetChild.gameObject.SetActive(true);
        }
        else
        {
            //加载背景图片
            background.GetComponent<SpriteRenderer>().sprite =
                    Resources.Load<Sprite>("Sprites/Background/Background" + levelData.mapSuffix);
        }
        

        //设置背景音乐
        background.GetComponent<BGMusicControl>()
            .changeMusic("Music_ChooseGame");
        if((!LevelManagerStatic.IsLevelCompleted(level) && !levelData.一周目可选卡) || (levelData.禁止任何周目选卡))
        {
            卡槽.transform.localScale = new Vector3(levelData.TheSizeofNeck, levelData.TheSizeofNeck, levelData.TheSizeofNeck);
        }
        else
        {
            卡槽.transform.localScale = new Vector3(0.9f, 0.9f, 0.9f);
        }
        
        if (level == 2)
        {
            GameObject gameObject = Instantiate(一行地图, new Vector3(0.85f, -0.1036548f, 0), Quaternion.identity);
            gameObject.transform.SetParent(background.transform,true);
            
        }
    }

    private void OnDisable()
    {
        游戏进行 = false;
    }

    public void 初始化游戏()
    {
        SettingButton.SetActive(false);

        foreach (GameObject gameObject in firstAwakeList)
        {
            gameObject.SetActive(true);
        }
        //加载对应的种植管理组件
        GameObject pm = Instantiate(
            Resources.Load<GameObject>(
                "Prefabs/PlantingManagement/PlantingManagement" + levelData.plantingManagementSuffix),
            new Vector3(0, 0, 0),
            Quaternion.Euler(0, 0, 0)
        );
        pm.name = "Planting Management";

        //加载UI
        uiManagement.GetComponent<UIManagement>().initUI();

        Debug.Log("init");

        //foreach (GameObject gameObject in awakeList)
        //{
        //    gameObject.SetActive(true);
        //}
        //uiManagement.GetComponent<UIManagement>().appear();
        //levelController.activate();

        string panelPath = "Prefabs/UI/DialogPanel/DialogPanel-Level";

        // 尝试加载资源
        UnityEngine.Object dialogPanelPrefab = Resources.Load<UnityEngine.Object>(panelPath);

        if (dialogPanelPrefab != null /*&& !LevelManagerStatic.IsLevelCompleted(level)*/)
        {
            // 如果找到了对话，应该就是能对话的
            GameObject dialog = (GameObject)Instantiate(dialogPanelPrefab,
                new Vector3(0, 0, 0),
                Quaternion.Euler(0, 0, 0),
                dialogUI.transform);
            dialog.GetComponent<DialogLevel>().LevelNumber = level;

        }
        else
        {
            //找不到就直接启动关卡！
            awakeAll();
        }


    }

    public void awakeAll()
    {
        Debug.Log("Awake");
        SettingButton.SetActive(true);
        游戏进行 = true;
        foreach (GameObject gameObject in awakeList)
        {
            gameObject.SetActive(true);
        }
        uiManagement.GetComponent<UIManagement>().appear();
        zombieManagement.GetComponent<ZombieManagement>().activate();
        levelController.activate();
        Invoke("转换音乐",2f);

        if (levelData.LevelType == levelType.TheDreamOfWood)
        {
            树桩的梦想控制器.gameObject.SetActive(true);
#if UNITY_EDITOR || UNITY_ANDROID || UNITY_IOS || UNITY_WEBGL
            树桩的梦想手机适配.gameObject.SetActive(true);
#endif
        }
        else if(levelData.LevelType == levelType.TheDreamOfPotatoMine)
        {
            foreach(GameObject gameObject in TheDreamOfPotatoMine)
            gameObject.SetActive(true);
        }
        关卡特殊种植();
        EventHandler.CallGameStart();
    }

    public void 关卡特殊种植()
    {
        switch(level)
        {
            case 20:
                PlantGrid[] allObjects = FindObjectsByType<PlantGrid>(FindObjectsSortMode.None); // 查找所有 PlantGrid 对象，包括非激活的
                
                foreach (var obj in allObjects)
                {
                    obj.plantByGod("DiamonWood");
                }
                break;

        }
    }

    public virtual void 转换音乐()
    {
        background.GetComponent<BGMusicControl>().changeMusicSmoothly("Music" + levelData.backgroundSuffix);
    }

    public void gameOver()
    {
        游戏进行 = false;
        if (levelData.MustLost) win();
        else
        {
            endMenuPanel.GetComponent<EndMenu>().gameOver();
        }
    }

    public void win()
    {
        游戏进行 = false;
        endMenuPanel.GetComponent<EndMenu>().win(!LevelManagerStatic.IsLevelCompleted(level), level);
        LevelManagerStatic.SetLevelCompleted(level);
    }
}