using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Present : MonoBehaviour
{
    public GameObject cloud;
    public GameObject[] plantPrefabs; // 你可以在 Inspector 中添加植物预制体
    private PlantGrid plantGrid; // 记录礼盒所在的格子

    void Start()
    {
        // 获取当前礼盒所在的格子
        plantGrid = GetComponentInParent<PlantGrid>();
    }

    public void createCloud()
    {
        if(!GameManagement.isPerformance)
        {
            GameObject cloudPrefab = Instantiate(cloud, gameObject.transform.position, Quaternion.identity);
        }
        
    }

    // 当调用这个方法时销毁自己，并生成一个随机植物
    public void createRandomPlant()
    {
        // 确保植物预制体列表不为空
        if (plantPrefabs.Length > 0)
        {
            // 随机选择一个植物预制体
            int randomIndex = UnityEngine.Random.Range(0, plantPrefabs.Length);
            
            GameObject randomPlant = plantPrefabs[randomIndex];

            if(randomPlant.name == gameObject.name)
            {
                SetAchievement.SetAchievementCompleted("我是幸运礼盒！我才是幸运礼盒！");
            }
            plantGrid = GetComponentInParent<PlantGrid>();
            // 在当前格子位置生成一个随机植物
            GameObject spawnedPlant = Instantiate(randomPlant, plantGrid.transform.position + new Vector3(0, 0, 5), Quaternion.identity, plantGrid.transform);

            // 将父物体（PlantGrid）的 nowPlant 设置为新生成的植物
            plantGrid.nowPlant = spawnedPlant;

            spawnedPlant.GetComponent<Plant>().initialize(
            GetComponentInParent<PlantGrid>(),
            GetComponentInParent<PlantGrid>().gameObject.GetComponent<SpriteRenderer>().sortingLayerName,
            GetComponentInParent<PlantGrid>().gameObject.GetComponent<SpriteRenderer>().sortingOrder);
        }    
    }

    public void disappear()
    {
        PlantManagement.RemovePlant(gameObject);
        // 销毁当前礼盒
        Destroy(gameObject);
    }
}
