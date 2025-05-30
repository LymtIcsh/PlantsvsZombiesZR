using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlantButtonHandler : MonoBehaviour
{
    public int plantId;  // ����ָ����ť��Ӧ��ֲ��ID
    public Transform plantSpawnLocation; // ָ��ֲ��ʵ������λ��

    private PlantStruct myPlantStruct;

    public TextMeshProUGUI SunText;
    public Image PlantImage;
    public Image BackgroundImage;

    public Sprite[] BackgroundImages;

    private GameObject plantPrefab; // ��ֲ���Ԥ����




    private InfoDisplay plantInfoDisplay;

    void Start()
    {
        myPlantStruct = PlantStructManager.GetPlantStructById(plantId);
        InitUI(myPlantStruct);

        plantInfoDisplay = FindFirstObjectByType<InfoDisplay>();  // ��ȡ PlantInfoDisplay ʵ��
        Button button = GetComponent<Button>();  // ��ȡ��ť���
        button.onClick.AddListener(OnButtonClick);  // ע�ᰴť����¼�

            
    }

    private void InitUI(PlantStruct plant_Struct) {

        string road = plant_Struct.plantName;

        Sprite sprite = Resources.Load<Sprite>("Sprites/Plants/" + road);//��ȡ��Ӧ�ļ��е�ͼƬ�ļ�
        if (sprite != null)
        {
            PlantImage.sprite = sprite;
        }
        else
        {
            print("��Ƭ����ͼƬʧ��");
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


    // ��ť���ʱ���ô˷���
    public void OnButtonClick()
    {
        if (plantInfoDisplay != null)
        {
            plantInfoDisplay.ShowPlantInfo(plantId);  // ��ʾ��Ӧֲ�����Ϣ
        }
        
        foreach (Transform child in plantSpawnLocation)
        {
            // ɾ��������
            Destroy(child.gameObject);
            Debug.Log("ɾ��������: " + child.name);
        }

        plantPrefab = Resources.Load<GameObject>("Prefabs/Plants/" + PlantStructManager.GetPlantStructById(plantId).plantName);
        // ��ָ��λ��ʵ�����µ�ֲ��
        if (plantPrefab != null && plantSpawnLocation != null)
        {
            
            GameObject currentPlantInstance = Instantiate(plantPrefab, plantSpawnLocation.position, Quaternion.identity,plantSpawnLocation.transform); // ��ָ��λ��ʵ����ֲ��
            
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
                
                Destroy(plantScript);//ɾ��Plant�ű�
                Debug.Log("ɾ����ֲ���ϵ� Plant �ű�");
            }
            if (plantScript2 != null)
            {
                Destroy(plantScript2);//ɾ��Plant�ű�
                Debug.Log("ɾ����ֲ���ϵ� Plant �ű�");
            }
            
        }
    }
}
