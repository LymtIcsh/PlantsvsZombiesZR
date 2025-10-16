using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlantButtonHandler : MonoBehaviour
{
    public int plantId;  // 用于指定按钮对应的植物ID
    public Transform plantSpawnLocation; // 指定植物实例化的位置

    private PlantStruct myPlantStruct;

    public TextMeshProUGUI SunText;
    public Image PlantImage;
    public Image BackgroundImage;

    public Sprite[] BackgroundImages;

    private GameObject plantPrefab; // 该植物的预制体




    private InfoDisplay plantInfoDisplay;

    void Start()
    {
        myPlantStruct = PlantStructManager.GetPlantStructById(plantId);
        InitUI(myPlantStruct);

        plantInfoDisplay = FindFirstObjectByType<InfoDisplay>();  // 获取 PlantInfoDisplay 实例
        Button button = GetComponent<Button>();  // 获取按钮组件
        button.onClick.AddListener(OnButtonClick);  // 注册按钮点击事件

            
    }

    private void InitUI(PlantStruct plant_Struct) {

        string road = plant_Struct.plantName;

        Sprite sprite = Resources.Load<Sprite>("Sprites/Plants/" + road);//读取对应文件夹的图片文件
        if (sprite != null)
        {
            PlantImage.sprite = sprite;
        }
        else
        {
            print("卡片查找图片失败");
        }

        switch (plant_Struct.envType)
        {
            case EnvironmentType.Day: BackgroundImage.sprite = BackgroundImages[0]; break;
            //case EnvironmentType.Night: BackgroundImage.sprite = BackgroundImages[1]; break;
            case EnvironmentType.Forest: BackgroundImage.sprite = BackgroundImages[2]; break;
            case EnvironmentType.SnowIce: BackgroundImage.sprite = BackgroundImages[3]; break;
            case EnvironmentType.Steel: BackgroundImage.sprite = BackgroundImages[4]; break;
            case EnvironmentType.Special: BackgroundImage.sprite = BackgroundImages[5]; break;
            case EnvironmentType.Other: BackgroundImage.sprite = BackgroundImages[5]; break;
            case EnvironmentType.Collaboration: BackgroundImage.sprite = BackgroundImages[6]; break;
            default: BackgroundImage.sprite = BackgroundImages[0]; break;
        }
        if (plant_Struct.envType != EnvironmentType.Other)
        {
            
            SunText.text = plant_Struct.Cost.ToString();
        }
        else {

            SunText.text = null;
        }


    }


    // 按钮点击时调用此方法
    public void OnButtonClick()
    {
        if (plantInfoDisplay != null)
        {
            plantInfoDisplay.ShowPlantInfo(plantId);  // 显示对应植物的信息
        }
        
        foreach (Transform child in plantSpawnLocation)
        {
            // 删除子物体
            Destroy(child.gameObject);
            Debug.Log("删除子物体: " + child.name);
        }

        plantPrefab = Resources.Load<GameObject>("Prefabs/Plants/" + PlantStructManager.GetPlantStructById(plantId).plantName);
        // 在指定位置实例化新的植物
        if (plantPrefab != null && plantSpawnLocation != null)
        {
            
            GameObject currentPlantInstance = Instantiate(plantPrefab, plantSpawnLocation.position, Quaternion.identity,plantSpawnLocation.transform); // 在指定位置实例化植物
            
            Plant plantScript = currentPlantInstance.GetComponent<Plant>();
            Present plantScript2 = currentPlantInstance.GetComponent<Present>();
            plantScript.initialize(null,"Plant-0",1);
            if (plantScript != null)
            {
                Debug.Log(111);
                if(plantScript.detectZombieRegion != null)
                {
                    Destroy(plantScript.detectZombieRegion.gameObject);
                }
                
                Destroy(plantScript);//删除Plant脚本
                Debug.Log("删除了植物上的 Plant 脚本");
            }
            if (plantScript2 != null)
            {
                Destroy(plantScript2);//删除Plant脚本
                Debug.Log("删除了植物上的 Plant 脚本");
            }
            
        }
    }
}
