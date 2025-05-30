using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class ZombieButtonHandler : MonoBehaviour
{
    //基于原本的图鉴按钮更改的通用卡片按钮
    public int Id;  // 用于指定按钮对应的ID
    public Transform SpawnLocation; // 指定实例化的位置
    public ZombieStruct zombieStruct;//僵尸结构体
    public Image ZombieImage;
    public TextMeshProUGUI SunText;



    private GameObject Prefab; // 预制体

    private InfoDisplay InfoDisplay;
    public bool isZombie;

    // 缓存所有一级目录下的僵尸预制体
    private GameObject[] allZombiePrefabs;
    // 按名字快速查找的字典
    private Dictionary<string, GameObject> prefabByName;

    private void Awake()
    {
        // 1. 一次性加载 Prefabs/Zombies 下的所有 GameObject（不含子文件夹）
        allZombiePrefabs = Resources.LoadAll<GameObject>("Prefabs/Zombies");
        Debug.Log($"Loaded {allZombiePrefabs.Length} zombie prefabs.");

        // 2. 构建名字->预制体 的字典
        prefabByName = allZombiePrefabs
            .ToDictionary(prefab => prefab.name, prefab => prefab);
    }

    void Start()
    {
        InfoDisplay = FindFirstObjectByType<InfoDisplay>();  // 获取 PlantInfoDisplay 实例
        Button button = GetComponent<Button>();  // 获取按钮组件
        button.onClick.AddListener(OnButtonClick);  // 注册按钮点击事件

        zombieStruct = ZombieStructManager.GetZombieStructById(Id);
        InitUI();


    }

    private void InitUI() {

        string road = zombieStruct.zombieName;

        Sprite sprite = Resources.Load<Sprite>("Sprites/Zombies/僵尸图鉴/" + road);//读取对应文件夹的图片文件
        if (sprite != null)
        {
            ZombieImage.sprite = sprite;
        }
        else
        {
            print("卡片查找图片失败");
        }

        SunText.text = zombieStruct.Cost.ToString();


    }




    // 按钮点击时调用此方法
    public void OnButtonClick()
    {
        if (isZombie)
        {
            Debug.Log(InfoDisplay);
            if (InfoDisplay != null)
            {
                InfoDisplay.ShowZombieInfo(Id);  // 显示对应僵尸的信息
            }

            foreach (Transform child in SpawnLocation)
            {
                // 删除子物体
                Destroy(child.gameObject);
                Debug.Log("删除子物体: " + child.name);
            }

            Prefab = GetZombiePrefabById(Id);

            // 在指定位置实例化新的僵尸
            if (Prefab != null && SpawnLocation != null)
            {

                GameObject currentZombieInstance = Instantiate(Prefab, SpawnLocation.position, Quaternion.identity, SpawnLocation.transform); // 在指定位置实例化僵尸
                Zombie zombieScript = currentZombieInstance.GetComponent<Zombie>();
                currentZombieInstance.GetComponent<Animator>().SetBool("Idle", true);//待机动画
                                                                                     // zombieScript.initialize(null, "Plant-0", 1);
                if (zombieScript != null)
                {
                    Destroy(zombieScript);//删除Zombie脚本
                    Debug.Log("删除了僵尸上的 Zombie 脚本");

                }
           
            }
        }
       
    }

    /// <summary>
    /// 根据结构体里的名字去字典里取预制体
    /// </summary>
    public GameObject GetZombiePrefabById(int id)
    {
        // 假设这个方法返回了一个包含 zombieName 字段的结构体
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
