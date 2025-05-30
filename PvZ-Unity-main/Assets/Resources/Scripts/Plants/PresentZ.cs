using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


public class PresentZ : Plant
{
    //��ʬ���
    public GameObject cloud;
    public GameObject[] ZombiePrefabs; // ������� Inspector �����ֲ��Ԥ����


    public void createCloud()
    {
        if (!GameManagement.isPerformance)
        {
            GameObject cloudPrefab = Instantiate(cloud, gameObject.transform.position, Quaternion.identity);
        }

    }

    // �������������ʱ�����Լ���������һ�������ʬ
    public void createRandomZombie()
    {
        
        if (ZombiePrefabs.Length > 0)
        {
            // ���ѡ��һ��ֲ��Ԥ����
            int randomIndex = UnityEngine.Random.Range(0,ZombieStructManager.GetDataBaseLength());

            string random = ZombieStructManager.GetZombieStructById(randomIndex).zombieName;

            GameObject randomZombie = ZombieManagement.instance.GenerateZombieByWeight();
         
            //plantGrid = GetComponentInParent<PlantGrid>();
            //�ڵ�ǰ����λ������һ�������ʬ
            Vector3 vector3 = gameObject.transform.position;
            vector3.y = GameManagement.levelData.zombieInitPosY[row];
            vector3.z = 0;
            GameObject spawnedZombie = Instantiate(randomZombie, vector3, Quaternion.identity, GameManagement.instance.zombieManagement.transform);
            spawnedZombie.GetComponent<Zombie>().pos_row = this.row;
            spawnedZombie.GetComponent<Zombie>().setPosRow(this.row);//����ͼ��

        }
    }

    public void disappear()
    {
        PlantManagement.RemovePlant(gameObject);
        // ���ٵ�ǰ���
        die(null, this.gameObject);
    }
}
