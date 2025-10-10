using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class GameManagement : MonoBehaviour
{
    [FormerlySerializedAs("��ֲ��Ч")] [Header("��ֲ��Ч")]
    public GameObject _plantingEffects;
    [FormerlySerializedAs("�仯��Ч")] [Header("�仯��Ч")]
    public GameObject[] _transformationEffects;

   
    public static GameManagement instance; // ��Ӵ���
    [Header("�Ƿ��������Ż�")]
    public static bool isPerformance = false;
    [Header("�Ƿ���ʾѪ��")]
    public static bool isShowHp;
    public static float GameDifficult = 2.0f;
    [Header("������Ϸ�ٶ�")]
    public static float InternalGameSpeed = 1.0f;
    public static bool CollectSun = true;
    [FormerlySerializedAs("��Ϸ����")] [Header("��Ϸ����")]
    public bool IsGameing;
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
    [FormerlySerializedAs("һ�е�ͼ")] 
    [Header("һ�е�ͼ")]
    public GameObject aRowMaps;
    public ForestSlider forestSlider;
    public ZombieForestSlider zombieForestSlider;
    public SunNumber SunText;
    [FormerlySerializedAs("��׮�����������")] [Header("��׮�����������")]
    public GameObject TreeStumpDreamController;
    [FormerlySerializedAs("��׮�������ֻ�����")] [Header("��׮�������ֻ�����")]
    public GameObject TreeStumpDreamMobilePhoneCompatibility;
    [FormerlySerializedAs("GloveUI����ʱ")] [Header("GloveUI����ʱ")]
    public GloveUICountdown _gloveUICountdown;
    [FormerlySerializedAs("����")] [Header("����")]
    public GameObject cardSlot;
    public GameObject[] TheDreamOfPotatoMine;

    [FormerlySerializedAs("������ʾ")] [Header("������ʾ")]
    public Text nameShowText;
    public GameObject sunManagement;
    public void Awake()
    {
        instance = this;
        nameShowText.text = LevelManagerStatic.GetCurrentSaveName() + "�ķ���";
        nameShowText.gameObject.SetActive(true);
    
        if (SceneManager.GetActiveScene().name == "GameScene")
        {
            Time.timeScale = GameManagement.InternalGameSpeed;
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
        if((!LevelManagerStatic.IsLevelCompleted(level) && !levelData.canSelectCardsInFirstPlaythrough) || (levelData.disableCardSelection))
        {
            cardSlot.transform.localScale = new Vector3(levelData.TheSizeofNeck, levelData.TheSizeofNeck, levelData.TheSizeofNeck);
        }
        else
        {
            cardSlot.transform.localScale = new Vector3(0.9f, 0.9f, 0.9f);
        }
        
        if (level == 2)
        {
            GameObject gameObject = Instantiate(aRowMaps, new Vector3(0.85f, -0.1036548f, 0), Quaternion.identity);
            gameObject.transform.SetParent(background.transform,true);
            
        }
    }

    private void OnDisable()
    {
        IsGameing = false;
    }

    /// <summary>
    /// ��ʼ����Ϸ
    /// </summary>
    public void InitializeGame()
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
        IsGameing = true;
        foreach (GameObject gameObject in awakeList)
        {
            gameObject.SetActive(true);
        }
        uiManagement.GetComponent<UIManagement>().appear();
        zombieManagement.GetComponent<ZombieManagement>().activate();
        levelController.activate();
        Invoke("SwitchMusic",2f);

        if (levelData.LevelType == levelType.TheDreamOfWood)
        {
            TreeStumpDreamController.gameObject.SetActive(true);
#if UNITY_EDITOR || UNITY_ANDROID || UNITY_IOS || UNITY_WEBGL
            TreeStumpDreamMobilePhoneCompatibility.gameObject.SetActive(true);
#endif
        }
        else if(levelData.LevelType == levelType.TheDreamOfPotatoMine)
        {
            foreach(GameObject gameObject in TheDreamOfPotatoMine)
            gameObject.SetActive(true);
        }
        LevelSpecialPlanting();
        EventHandler.CallGameStart();
    }

    /// <summary>
    /// �ؿ�������ֲ
    /// </summary>
    public void LevelSpecialPlanting()
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

    /// <summary>
    /// ת������
    /// </summary>
    public virtual void SwitchMusic()
    {
        background.GetComponent<BGMusicControl>().changeMusicSmoothly("Music" + levelData.backgroundSuffix);
    }

    public void gameOver()
    {
        IsGameing = false;
        if (levelData.MustLost) win();
        else
        {
            endMenuPanel.GetComponent<EndMenu>().gameOver();
        }
    }

    public void win()
    {
        IsGameing = false;
        endMenuPanel.GetComponent<EndMenu>().Win(!LevelManagerStatic.IsLevelCompleted(level), level);
        LevelManagerStatic.SetLevelCompleted(level);
    }
}