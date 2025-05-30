using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


public class PresentZ : Plant
{
    //僵尸礼盒
    public GameObject cloud;
    public GameObject[] ZombiePrefabs; // 你可以在 Inspector 中添加植物预制体


    public void createCloud()
    {
        if (!GameManagement.isPerformance)
        {
            GameObject cloudPrefab = Instantiate(cloud, gameObject.transform.position, Quaternion.identity);
        }

    }

    // 当调用这个方法时销毁自己，并生成一个随机僵尸
    public void createRandomZombie()
    {
        
        if (ZombiePrefabs.Length > 0)
        {
            // 随机选择一个植物预制体
            int randomIndex = UnityEngine.Random.Range(0,ZombieStructManager.GetDataBaseLength());

            string random = ZombieStructManager.GetZombieStructById(randomIndex).zombieName;

            GameObject randomZombie = ZombieManagement.instance.GenerateZombieByWeight();
         
            //plantGrid = GetComponentInParent<PlantGrid>();
            //在当前格子位置生成一个随机僵尸
            Vector3 vector3 = gameObject.transform.position;
            vector3.y = GameManagement.levelData.zombieInitPosY[row];
            vector3.z = 0;
            GameObject spawnedZombie = Instantiate(randomZombie, vector3, Quaternion.identity, GameManagement.instance.zombieManagement.transform);
            spawnedZombie.GetComponent<Zombie>().pos_row = this.row;
            spawnedZombie.GetComponent<Zombie>().setPosRow(this.row);//设置图层

        }
    }

    public void disappear()
    {
        PlantManagement.RemovePlant(gameObject);
        // 销毁当前礼盒
        die(null, this.gameObject);
    }
}
