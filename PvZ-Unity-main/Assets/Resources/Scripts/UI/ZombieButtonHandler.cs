using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class ZombieButtonHandler : MonoBehaviour
{
    //����ԭ����ͼ����ť���ĵ�ͨ�ÿ�Ƭ��ť
    public int Id;  // ����ָ����ť��Ӧ��ID
    public Transform SpawnLocation; // ָ��ʵ������λ��
    public ZombieStruct zombieStruct;//��ʬ�ṹ��
    public Image ZombieImage;
    public TextMeshProUGUI SunText;



    private GameObject Prefab; // Ԥ����

    private InfoDisplay InfoDisplay;
    public bool isZombie;

    // ��������һ��Ŀ¼�µĽ�ʬԤ����
    private GameObject[] allZombiePrefabs;
    // �����ֿ��ٲ��ҵ��ֵ�
    private Dictionary<string, GameObject> prefabByName;

    private void Awake()
    {
        // 1. һ���Լ��� Prefabs/Zombies �µ����� GameObject���������ļ��У�
        allZombiePrefabs = Resources.LoadAll<GameObject>("Prefabs/Zombies");
        Debug.Log($"Loaded {allZombiePrefabs.Length} zombie prefabs.");

        // 2. ��������->Ԥ���� ���ֵ�
        prefabByName = allZombiePrefabs
            .ToDictionary(prefab => prefab.name, prefab => prefab);
    }

    void Start()
    {
        InfoDisplay = FindFirstObjectByType<InfoDisplay>();  // ��ȡ PlantInfoDisplay ʵ��
        Button button = GetComponent<Button>();  // ��ȡ��ť���
        button.onClick.AddListener(OnButtonClick);  // ע�ᰴť����¼�

        zombieStruct = ZombieStructManager.GetZombieStructById(Id);
        InitUI();


    }

    private void InitUI() {

        string road = zombieStruct.zombieName;

        Sprite sprite = Resources.Load<Sprite>("Sprites/Zombies/��ʬͼ��/" + road);//��ȡ��Ӧ�ļ��е�ͼƬ�ļ�
        if (sprite != null)
        {
            ZombieImage.sprite = sprite;
        }
        else
        {
            print("��Ƭ����ͼƬʧ��");
        }

        SunText.text = zombieStruct.Cost.ToString();


    }




    // ��ť���ʱ���ô˷���
    public void OnButtonClick()
    {
        if (isZombie)
        {
            Debug.Log(InfoDisplay);
            if (InfoDisplay != null)
            {
                InfoDisplay.ShowZombieInfo(Id);  // ��ʾ��Ӧ��ʬ����Ϣ
            }

            foreach (Transform child in SpawnLocation)
            {
                // ɾ��������
                Destroy(child.gameObject);
                Debug.Log("ɾ��������: " + child.name);
            }

            Prefab = GetZombiePrefabById(Id);

            // ��ָ��λ��ʵ�����µĽ�ʬ
            if (Prefab != null && SpawnLocation != null)
            {

                GameObject currentZombieInstance = Instantiate(Prefab, SpawnLocation.position, Quaternion.identity, SpawnLocation.transform); // ��ָ��λ��ʵ������ʬ
                Zombie zombieScript = currentZombieInstance.GetComponent<Zombie>();
                currentZombieInstance.GetComponent<Animator>().SetBool("Idle", true);//��������
                                                                                     // zombieScript.initialize(null, "Plant-0", 1);
                if (zombieScript != null)
                {
                    Destroy(zombieScript);//ɾ��Zombie�ű�
                    Debug.Log("ɾ���˽�ʬ�ϵ� Zombie �ű�");

                }
           
            }
        }
       
    }

    /// <summary>
    /// ���ݽṹ���������ȥ�ֵ���ȡԤ����
    /// </summary>
    public GameObject GetZombiePrefabById(int id)
    {
        // �����������������һ������ zombieName �ֶεĽṹ��
        string name = ZombieStructManager.GetZombieStructById(id).zombieName;

        if (prefabByName.TryGetValue(name, out var prefab))
        {
            return prefab;
        }
        else
        {
            Debug.LogWarning($"Zombie prefab named '{name}' not found in Prefabs/Zombies!");
            return null;
        }
    }
}
