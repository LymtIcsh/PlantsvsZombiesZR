using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Present : MonoBehaviour
{
    public GameObject cloud;
    public GameObject[] plantPrefabs; // ������� Inspector �����ֲ��Ԥ����
    private PlantGrid plantGrid; // ��¼������ڵĸ���

    void Start()
    {
        // ��ȡ��ǰ������ڵĸ���
        plantGrid = GetComponentInParent<PlantGrid>();
    }

    public void createCloud()
    {
        if(!GameManagement.isPerformance)
        {
            GameObject cloudPrefab = Instantiate(cloud, gameObject.transform.position, Quaternion.identity);
        }
        
    }

    // �������������ʱ�����Լ���������һ�����ֲ��
    public void createRandomPlant()
    {
        // ȷ��ֲ��Ԥ�����б�Ϊ��
        if (plantPrefabs.Length > 0)
        {
            // ���ѡ��һ��ֲ��Ԥ����
            int randomIndex = UnityEngine.Random.Range(0, plantPrefabs.Length);
            
            GameObject randomPlant = plantPrefabs[randomIndex];

            if(randomPlant.name == gameObject.name)
            {
                SetAchievement.SetAchievementCompleted("����������У��Ҳ���������У�");
            }
            plantGrid = GetComponentInParent<PlantGrid>();
            // �ڵ�ǰ����λ������һ�����ֲ��
            GameObject spawnedPlant = Instantiate(randomPlant, plantGrid.transform.position + new Vector3(0, 0, 5), Quaternion.identity, plantGrid.transform);

            // �������壨PlantGrid���� nowPlant ����Ϊ�����ɵ�ֲ��
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
        // ���ٵ�ǰ���
        Destroy(gameObject);
    }
}
