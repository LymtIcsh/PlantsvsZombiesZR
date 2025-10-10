using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;

public class PlantGrid : MonoBehaviour
{
    #region ����

    public int row; //�ڵڼ���

    GameObject toBePlanted; //To Be Planted����
    public GameObject selectedShovel; //SelectedShovel����
    public GameObject selectedGlove; //Glove����

    public SpriteRenderer spriteRenderer; //����SpriteRenderer���

    public bool havePlanted = false; //�ø��Ƿ�����ֲֲ��
    public GameObject nowPlant; //��ǰ����ֲ��
    public GameObject gameManagement;


    [FormerlySerializedAs("��ֲ��Ч")] [Header("��ֲ��Ч")]
    public GameObject _plantingEffects;

    [FormerlySerializedAs("�仯��Ч")] [Header("�仯��Ч")]
    public GameObject[] _transformationEffects;

    public string EnvironmentString = null; //������Ϊֲ����ֵ��ַ�����Ϊ���Ժ󰴸��ӱ任ֲ�����ֻ�ǰ���ͼ�任

    // �Ӷ����
    private bool hasCrater = false; // ��ǵ�ǰ�������Ƿ���ڿӶ�
    private GameObject craterInstance; // ��ǰ�Ӷ�ʵ��

    #endregion

    #region ϵͳ��Ϣ

    private void Awake()
    {
        gameManagement = GameManagement.instance.gameObject;
        selectedShovel = gameManagement.GetComponent<GameManagement>().Shovel;
        selectedGlove = gameManagement.GetComponent<GameManagement>().Glove;
        _plantingEffects = gameManagement.GetComponent<GameManagement>()._plantingEffects;
        _transformationEffects = gameManagement.GetComponent<GameManagement>()._transformationEffects;

        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        toBePlanted = ToBePlanted.instance.gameObject;
        if (EnvironmentString.Length == 0) //�����Լ����룬Ĭ�ϰ���leveldata�Ļ�����
        {
            EnvironmentString = GameManagement.levelData.levelEnviornment;
        }
    }


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            spriteRenderer.sprite = null;
        }
    }

    private void OnMouseEnter()
    {
        if (hasCrater)
        {
            return;
        }

        if (havePlanted == false && toBePlanted.activeSelf == true && !PlantStructManager
                .GetPlantStructByName(toBePlanted.GetComponent<ToBePlanted>().plantName).IsPurpleCard)
        {
            PlantStruct plantStruct =
                PlantStructManager.GetPlantStructByName(toBePlanted.GetComponent<ToBePlanted>().plantName);
            spriteRenderer.sprite = toBePlanted.GetComponent<SpriteRenderer>().sprite;
            if (GameManagement.levelData.LevelType == levelType.FaithHill &&
                plantStruct.envType != EnvironmentType.Phonograph)
            {
                List<GameObject> matchingPlants = FindMatchingPlants();
                Debug.Log(matchingPlants.Count);
                foreach (var plant in matchingPlants)
                {
                    if (!plant.gameObject.GetComponent<PlantGrid>().havePlanted)
                    {
                        plant.gameObject.GetComponent<PlantGrid>().spriteRenderer.sprite =
                            toBePlanted.GetComponent<SpriteRenderer>().sprite;
                    }
                }
            }
        }
        else if (havePlanted == true && selectedGlove.activeSelf == true && StaticThingsManagement.glovePlant != null)
        {
        }
    }

    private void OnMouseExit()
    {
        if (havePlanted == false && toBePlanted.activeSelf == true)
        {
            spriteRenderer.sprite = null;
            if (GameManagement.levelData.LevelType == levelType.FaithHill)
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

        PlantStruct plantStruct =
            PlantStructManager.GetPlantStructByName(toBePlanted.GetComponent<ToBePlanted>().plantName);
        if (havePlanted == false && toBePlanted.activeSelf == true && plantStruct.IsPurpleCard == false)
        {
            plant(plantStruct.plantName);
            Debug.Log(plantStruct.envType);
            if (GameManagement.levelData.LevelType == levelType.FaithHill &&
                plantStruct.envType != EnvironmentType.Phonograph)
            {
                Debug.Log("����������");
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
            nowPlant.GetComponent<Plant>().die("shovelPlant", nowPlant);
        }
        else if (havePlanted == true && toBePlanted.activeSelf == true && plantStruct.IsPurpleCard == true &&
                 plantStruct.BasePlantName == nowPlant.GetComponent<Plant>().plantStruct.plantName)
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
            if (StaticThingsManagement.glovePlant != null)
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

                GameManagement.instance._gloveUICountdown.StartCooldown();
            }
        }
    }

    #endregion

    #region ˽���Զ��庯��

    #endregion

    #region �����Զ��庯��

    public void plant(string Name)
    {
        if (hasCrater)
        {
            return;
        }

        String name = GetNameFromJson(this.EnvironmentString, Name);

        spriteRenderer.sprite = null; //������Ӱ
        //if (GameManagement.levelData.LevelType==levelType.FaithHill)
        //{
        //    List<GameObject> matchingPlants = FindMatchingPlants();
        //    foreach (var plant in matchingPlants)
        //    {
        //        plant.gameObject.GetComponent<PlantGrid>().spriteRenderer.sprite = null;
        //    }
        //}

        havePlanted = true; //����ֲ��

        //����ֲ��
        nowPlant = Instantiate(Resources.Load<GameObject>("Prefabs/Plants/" + name),
            transform.position + new Vector3(0, 0, 5),
            Quaternion.Euler(0, 0, 0),
            transform);
        nowPlant.GetComponent<Plant>().initialize(
            this,
            spriteRenderer.sortingLayerName,
            spriteRenderer.sortingOrder
        );

        if (name != null && name != Name) //ֲ��仯ʱ����Ч
        {
            switch (EnvironmentString)
            {
                case "Day":
                    Instantiate(_transformationEffects[0], nowPlant.transform.position, Quaternion.identity);
                    break;
                case "Forest":
                    Instantiate(_transformationEffects[1], nowPlant.transform.position, Quaternion.identity);
                    break;
                case "SnowIce":
                    Instantiate(_transformationEffects[2], nowPlant.transform.position, Quaternion.identity);
                    break;
                case "Forest_P":
                    Debug.Log(EnvironmentString);
                    Instantiate(_transformationEffects[3], nowPlant.transform.position, Quaternion.identity);
                    break;
                default: break;
            }
        }


        AudioManager.Instance.PlaySoundEffect(32);

        Vector3 currentPosition = transform.position;
        Vector3 spawnPosition = new Vector3(currentPosition.x, currentPosition.y - 0.374f, currentPosition.z);
        Instantiate(_plantingEffects, spawnPosition, Quaternion.identity);

        //��PlantingManagement������Ϣ�Դ���UI����¼�
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

    //�ϵ�ģʽ��ֲ�����ڹؿ���ʼ�Ի����ɲ���Ի���ֲ��
    public GameObject plantByGod(string name)
    {
        if (hasCrater)
        {
            return null;
        }

        havePlanted = true; //����ֲ��

        //����ֲ��
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

    /// <summary>
    /// �ϵ�ģʽ��ֲ�����Խ��б���
    /// </summary>
    /// <param name="name"></param>
    /// <param name="CanVary">CanVary</param>
    /// <returns></returns>
    public GameObject plantByGod(string name, bool CanVary)
    {
        if (hasCrater)
        {
            return null;
        }

        if (CanVary)
        {
            name = GetNameFromJson(GameManagement.levelData.levelEnviornment, name);
        }

        havePlanted = true; //����ֲ��

        //����ֲ��
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

    public void plantDie(string reason, GameObject plantObject)
    {
        havePlanted = false; //��û��ֲ��
        if (reason == "shovelPlant")
        {
            AudioManager.Instance.PlaySoundEffect(33);
        }
        else if (reason == "beEated")
        {
            AudioManager.Instance.PlaySoundEffect(34);
        }
    }

    /// <summary>
    /// �ⲿ���ã��ڱ��������ɿӶ�
    /// </summary>
    public void SpawnCrater()
    {
        if (hasCrater) return;

        // �Ӷ����ȡ���Ӷ�
        craterInstance = DynamicObjectPoolManager.Instance.GetFromPool(PoolType.NormalCrater);

        // ���ÿӶ�λ�ã�x-0.05, y-0.35��
        Vector3 craterPos = transform.position + new Vector3(-0.05f, -0.35f, 0);
        craterInstance.transform.position = craterPos;
        craterInstance.transform.rotation = Quaternion.identity;

        // ���ó�ʼ�Ӷ�sprite
        SpriteRenderer craterSr = craterInstance.GetComponent<SpriteRenderer>();
        craterSr.sprite = Resources.Load<Sprite>("Sprites/Items/crater/crater_Normal_1");

        hasCrater = true;
        StartCoroutine(CraterLifeCycle());
    }

    /// <summary>
    /// �Ӷ���������Э�̣�90���ͼ����90���黹����
    /// </summary>
    private IEnumerator CraterLifeCycle()
    {
        SpriteRenderer craterSr = craterInstance.GetComponent<SpriteRenderer>();

        // ǰ90�뱣�ֳ�ʼ״̬
        yield return new WaitForSeconds(90f);
        // �л����ڶ��׶�Sprite
        craterSr.sprite = Resources.Load<Sprite>("Sprites/Items/crater/crater_Normal_2");

        // �ٹ�90���黹����
        yield return new WaitForSeconds(90f);

        DynamicObjectPoolManager.Instance.ReturnToPool(PoolType.NormalCrater, craterInstance);
        craterInstance = null;
        hasCrater = false;
    }


    List<GameObject> FindMatchingPlants() //���ڲ��Ҷ�Ӧ������ֲ��
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
                    if (obj.GetComponent<PlantGrid>() != null && !obj.GetComponent<PlantGrid>().hasCrater)
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

        // �� Resources Ŀ¼���� JSON �ļ�
        TextAsset jsonFile = Resources.Load<TextAsset>("Json/EnviornmentPlants");

        // ����ļ��Ƿ�ɹ�����
        if (jsonFile != null)
        {
            // ��ȡ JSON �ļ�����
            string json = jsonFile.text;

            // �� JSON ת���ض����б�
            List<Data> dataList = JsonUtility.FromJson<ListWrapper>(json).items;

            // ���ҷ��������Ķ���
            foreach (var item in dataList)
            {
                if (item.Enviornment == enviornment && item.Raw == raw)
                {
                    Debug.Log(item.Name);
                    return item.Name; // ���ض�Ӧ�� Name
                }
            }
        }

        // ���û���ҵ����ϵ���Ŀ���򷵻� Raw
        return raw;
    }

    // ���ڴ� JSON �ļ���ȡ List ����


    // ���ڳ�ʼ��һ������ JSON �ļ�
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