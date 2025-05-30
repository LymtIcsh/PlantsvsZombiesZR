using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManagement : MonoBehaviour
{
    public GameObject ��ֲ��Ч;
    public GameObject[] �仯��Ч;

    public static GameManagement instance; // ��Ӵ���
    public static bool isPerformance = false;//�Ƿ��������Ż�
    public static bool �Ƿ���ʾѪ��;
    public static float GameDifficult = 2.0f;
    public static float ������Ϸ�ٶ� = 1.0f;
    public static bool CollectSun = true;
    public bool ��Ϸ����;
    public bool lockMusic = false;

    public static int level;   //��ǰLevelNumber

    private LevelController levelController;
    public static LevelData levelData;   //��ǰ�ؿ�����

    public List<GameObject> firstAwakeList;//�Ȼ��Ѷ���
    public List<GameObject> awakeList;  //�������б����ڿ�������������Ѹû��ѵĶ���

    public GameObject endMenuPanel;   //��Ϸ�������
    public GameObject dialogUI;
    public GameObject background;   //��������
    public GameObject SettingButton;

    public GameObject zombieManagement;   //��ʬ�������
    public ChooseCardManager ChooseCardManager;
    public GameObject uiManagement;   //UI�������
    public GameObject melonPollingObject; //���ϱ��������
    public GameObject Glove;
    public GameObject Shovel;
    public GameObject һ�е�ͼ;
    public ForestSlider forestSlider;
    public ZombieForestSlider zombieForestSlider;
    public SunNumber SunText;
    public GameObject ��׮�����������;
    public GameObject ��׮�������ֻ�����;
    public GloveUI����ʱ GloveUI����ʱ;
    public GameObject ����;
    public GameObject[] TheDreamOfPotatoMine;
    public static GameManagement gameManagement;
    public Text ������ʾ;
    public GameObject sunManagement;
    public void Awake()
    {
        instance = this;
        ������ʾ.text = LevelManagerStatic.GetCurrentSaveName() + "�ķ���";
        ������ʾ.gameObject.SetActive(true);
        gameManagement = gameObject.GetComponent<GameManagement>();
        if (SceneManager.GetActiveScene().name == "GameScene")
        {
            Time.timeScale = GameManagement.������Ϸ�ٶ�;
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
            //���ر���ͼƬ
            background.GetComponent<SpriteRenderer>().sprite =
                    Resources.Load<Sprite>("Sprites/Background/Background" + levelData.mapSuffix);
        }
        

        //���ñ�������
        background.GetComponent<BGMusicControl>()
            .changeMusic("Music_ChooseGame");
        if((!LevelManagerStatic.IsLevelCompleted(level) && !levelData.һ��Ŀ��ѡ��) || (levelData.��ֹ�κ���Ŀѡ��))
        {
            ����.transform.localScale = new Vector3(levelData.TheSizeofNeck, levelData.TheSizeofNeck, levelData.TheSizeofNeck);
        }
        else
        {
            ����.transform.localScale = new Vector3(0.9f, 0.9f, 0.9f);
        }
        
        if (level == 2)
        {
            GameObject gameObject = Instantiate(һ�е�ͼ, new Vector3(0.85f, -0.1036548f, 0), Quaternion.identity);
            gameObject.transform.SetParent(background.transform,true);
            
        }
    }

    private void OnDisable()
    {
        ��Ϸ���� = false;
    }

    public void ��ʼ����Ϸ()
    {
        SettingButton.SetActive(false);

        foreach (GameObject gameObject in firstAwakeList)
        {
            gameObject.SetActive(true);
        }
        //���ض�Ӧ����ֲ�������
        GameObject pm = Instantiate(
            Resources.Load<GameObject>(
                "Prefabs/PlantingManagement/PlantingManagement" + levelData.plantingManagementSuffix),
            new Vector3(0, 0, 0),
            Quaternion.Euler(0, 0, 0)
        );
        pm.name = "Planting Management";

        //����UI
        uiManagement.GetComponent<UIManagement>().initUI();

        Debug.Log("init");

        //foreach (GameObject gameObject in awakeList)
        //{
        //    gameObject.SetActive(true);
        //}
        //uiManagement.GetComponent<UIManagement>().appear();
        //levelController.activate();

        string panelPath = "Prefabs/UI/DialogPanel/DialogPanel-Level";

        // ���Լ�����Դ
        UnityEngine.Object dialogPanelPrefab = Resources.Load<UnityEngine.Object>(panelPath);

        if (dialogPanelPrefab != null /*&& !LevelManagerStatic.IsLevelCompleted(level)*/)
        {
            // ����ҵ��˶Ի���Ӧ�þ����ܶԻ���
            GameObject dialog = (GameObject)Instantiate(dialogPanelPrefab,
                new Vector3(0, 0, 0),
                Quaternion.Euler(0, 0, 0),
                dialogUI.transform);
            dialog.GetComponent<DialogLevel>().LevelNumber = level;

        }
        else
        {
            //�Ҳ�����ֱ�������ؿ���
            awakeAll();
        }


    }

    public void awakeAll()
    {
        Debug.Log("Awake");
        SettingButton.SetActive(true);
        ��Ϸ���� = true;
        foreach (GameObject gameObject in awakeList)
        {
            gameObject.SetActive(true);
        }
        uiManagement.GetComponent<UIManagement>().appear();
        zombieManagement.GetComponent<ZombieManagement>().activate();
        levelController.activate();
        Invoke("ת������",2f);

        if (levelData.LevelType == levelType.TheDreamOfWood)
        {
            ��׮�����������.gameObject.SetActive(true);
#if UNITY_EDITOR || UNITY_ANDROID || UNITY_IOS || UNITY_WEBGL
            ��׮�������ֻ�����.gameObject.SetActive(true);
#endif
        }
        else if(levelData.LevelType == levelType.TheDreamOfPotatoMine)
        {
            foreach(GameObject gameObject in TheDreamOfPotatoMine)
            gameObject.SetActive(true);
        }
        �ؿ�������ֲ();
        EventHandler.CallGameStart();
    }

    public void �ؿ�������ֲ()
    {
        switch(level)
        {
            case 20:
                PlantGrid[] allObjects = FindObjectsByType<PlantGrid>(FindObjectsSortMode.None); // �������� PlantGrid ���󣬰����Ǽ����
                
                foreach (var obj in allObjects)
                {
                    obj.plantByGod("DiamonWood");
                }
                break;

        }
    }

    public virtual void ת������()
    {
        background.GetComponent<BGMusicControl>().changeMusicSmoothly("Music" + levelData.backgroundSuffix);
    }

    public void gameOver()
    {
        ��Ϸ���� = false;
        if (levelData.MustLost) win();
        else
        {
            endMenuPanel.GetComponent<EndMenu>().gameOver();
        }
    }

    public void win()
    {
        ��Ϸ���� = false;
        endMenuPanel.GetComponent<EndMenu>().win(!LevelManagerStatic.IsLevelCompleted(level), level);
        LevelManagerStatic.SetLevelCompleted(level);
    }
}