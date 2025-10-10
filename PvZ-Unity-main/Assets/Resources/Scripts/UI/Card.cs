using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Card : MonoBehaviour
{
    //冷却贴图
    public GameObject upperImageObj;
    public Image lowerImage;
    public GameObject lowerImageObj;
    public GameObject Probational;

    private Button myButton;   //自身Button组件

    //冷却时间与冷却状态
    public float coolingTime;
    float timer;
    bool coolingState = true;
    

    //阳光是否充足状态
    bool sunEnough;

    //种植相关
    PlantingManagement planting;
    private string plantName;
    private Image BackgroundImage;
    public int sunNeeded;

    public bool notCoolingInFirst;

    //植物图片和阳光Text
    public Image PlantImage;
    public TextMeshProUGUI  SunText;

    public Sprite[] BackgroundImages;
 
    public PlantStruct PlantStruct;

    public bool ConveyorCard = false;
    private ConveyorManager manager;

    private void Awake()
    {
        myButton = GetComponent<Button>();
    }

    // Start is called before the first frame update
    void Start()
    {
        BackgroundImage = GetComponent<Image>();

        //该组件须由管理对象加载，故在Start获取
        planting = PlantingManagement.instance;

        if(!ConveyorCard)
        {
            InitCard(PlantStruct);
            if (!notCoolingInFirst) cooling();
            else endCooling();
        }
        else
        {
            InitConveyorCard(PlantStruct);
        }
        
        
    }

    private void InitCard(PlantStruct plantStruct)
    {
        plantName = plantStruct.plantName;
        coolingTime = PlantStructManager.GetPlantStructByName(plantName).CD;
        sunNeeded = PlantStructManager.GetPlantStructByName(plantName).Cost;

        switch (PlantStructManager.GetPlantStructByName(plantName).ChineseName)//部分植物开始无CD
        {
            default: break;
            case "豌豆射手":
            case "向日葵":
            case "随机礼盒":
            case "小喷菇":
            case "阳光菇":
            case "森林向日葵":
            case "荧光蘑菇":
            case "毒烟小喷菇":
            case "冰封向日葵":
            case "故事最初的留声机":
                notCoolingInFirst = true;
                break;
        }
        if (SunText != null)
        {
            SunText.text = sunNeeded.ToString();
        }
        if (plantName != null)
        {
            string road = plantName;

            Sprite sprite = Resources.Load<Sprite>("Sprites/Plants/" + road);//读取对应文件夹的图片文件
            if (sprite != null)
            {
                PlantImage.sprite = sprite;
            }
            else {
                print("卡片查找图片失败");
            }
        }

        if (dev.DeveloperMode)
        {
            coolingTime = 0;
            sunNeeded = 0;
        }

        updateSunEnough(GameManagement.instance.SunText.GetSunNum() >= sunNeeded);

        switch (PlantStructManager.GetPlantStructByName(plantName).envType) {
            case EnvironmentType.Day: BackgroundImage.sprite = BackgroundImages[0]; break;
            case EnvironmentType.Forest: BackgroundImage.sprite = BackgroundImages[2]; break;
            case EnvironmentType.SnowIce: BackgroundImage.sprite = BackgroundImages[3]; break;
            case EnvironmentType.Steel:BackgroundImage.sprite = BackgroundImages[4]; break;
            case EnvironmentType.Special: BackgroundImage.sprite = BackgroundImages[5]; break;
            case EnvironmentType.Collaboration: BackgroundImage.sprite = BackgroundImages[6]; break;
            default: BackgroundImage.sprite = BackgroundImages[0]; break;
        }


        //if (PlantStructManager.GetPlantStructByName(plantName).envType.ToString() !=
        //    GameManagement.levelData.levelEnviornment) {//对不属于改群系的卡片的惩罚效果--冷却需要时间增加50%
        //    print("卡片归属是"+PlantStructManager.GetPlantStructByName(plantName).envType.ToString());
        //    print("环境是："+GameManagement.levelData.levelEnviornment);

        //    this.coolingTime *= (float)3/2;
        //}
        if(GameManagement.GameDifficult == 1 && PlantStructManager.GetPlantStructByName(plantName).envType != EnvironmentType.Phonograph)
        {
            coolingTime = 0;
        }

        

        Probational.SetActive(!LevelManagerStatic.IsLevelCompleted(plantStruct.GetLevel));

        
    }

    private void InitConveyorCard(PlantStruct plantStruct)
    {
        plantName = plantStruct.plantName;
        coolingTime = 0;
        sunNeeded = 0;
        notCoolingInFirst = true;

        if (SunText != null)
        {
            SunText.text = "";
        }
        if (plantName != null)
        {
            string road = plantName;

            Sprite sprite = Resources.Load<Sprite>("Sprites/Plants/" + road);//读取对应文件夹的图片文件
            if (sprite != null)
            {
                PlantImage.sprite = sprite;
            }
            else
            {
                print("卡片查找图片失败");
            }
        }

        updateSunEnough(GameManagement.instance.SunText.GetSunNum() >= sunNeeded);

        switch (PlantStructManager.GetPlantStructByName(plantName).envType)
        {
            case EnvironmentType.Day: BackgroundImage.sprite = BackgroundImages[0]; break;
            case EnvironmentType.Forest: BackgroundImage.sprite = BackgroundImages[2]; break;
            case EnvironmentType.SnowIce: BackgroundImage.sprite = BackgroundImages[3]; break;
            case EnvironmentType.Steel: BackgroundImage.sprite = BackgroundImages[4]; break;
            case EnvironmentType.Special: BackgroundImage.sprite = BackgroundImages[5]; break;
            default: BackgroundImage.sprite = BackgroundImages[0]; break;
        }


        //if (PlantStructManager.GetPlantStructByName(plantName).envType.ToString() !=
        //    GameManagement.levelData.levelEnviornment) {//对不属于改群系的卡片的惩罚效果--冷却需要时间增加50%
        //    print("卡片归属是"+PlantStructManager.GetPlantStructByName(plantName).envType.ToString());
        //    print("环境是："+GameManagement.levelData.levelEnviornment);

        //    this.coolingTime *= (float)3/2;
        //}
        //if (GameManagement.GameDifficult == 1 && PlantStructManager.GetPlantStructByName(plantName).envType != EnvironmentType.Phonograph)
        //{
        //    coolingTime = 0;
        //}

        Probational.SetActive(!LevelManagerStatic.IsLevelCompleted(plantStruct.GetLevel));


    }



    // Update is called once per frame
    void Update()
    {
        if (!ConveyorCard && coolingState == true)
        {
            timer += Time.deltaTime;
            if (timer / coolingTime < 1)
                lowerImage.rectTransform.localScale = new Vector3(1, 1 - timer / coolingTime, 1);
            else endCooling();
        }
    }

    public void cooling()
    {
        if(!ConveyorCard)
        {
            coolingState = true;
            timer = 0;
            lowerImage.fillAmount = 1;
            upperImageObj.SetActive(true);
            lowerImageObj.SetActive(true);
            myButton.enabled = false;
        }
        else
        {
            manager.RemoveCard(gameObject);
        }
    }

    private void endCooling()
    {
        coolingState = false;
        lowerImageObj.SetActive(false);
        if (sunEnough)
        {
            upperImageObj.SetActive(false);
            myButton.enabled = true;
        }
    }

    public void updateSunEnough(bool state)
    {
        if(!ConveyorCard)
        {
            if (myButton == null)
            {
                myButton = GetComponent<Button>();
            }
            if (state == true)
            {
                sunEnough = true;
                if (coolingState == false)
                {
                    upperImageObj.SetActive(false);
                    myButton.enabled = true;
                }
            }
            else
            {
                sunEnough = false;
                upperImageObj.SetActive(true);
                myButton.enabled = false;
            }
        }
        else
        {
            myButton = GetComponent<Button>();
            sunEnough = true;
            lowerImageObj.SetActive(false);
            upperImageObj.SetActive(false);
        }
        
    }

    public void click()
    {
        if (!ConveyorCard && coolingState == false && sunEnough == true && gameObject.GetComponent<Card>() != null && plantName != null)
        {
            // 播放音效
            AudioManager.Instance.PlaySoundEffect(39);

            // 转交给种植管理
            planting.clickPlant(plantName, gameObject.GetComponent<Card>());
        }
        else if(ConveyorCard)
        {
            // 播放音效
            AudioManager.Instance.PlaySoundEffect(39);

            // 转交给种植管理
            planting.clickPlant(plantName, gameObject.GetComponent<Card>());
        }
    }

    public void ConveyorInitialize(ConveyorManager manager)
    {
        this.manager = manager;
        ConveyorCard = true;
    }


}
