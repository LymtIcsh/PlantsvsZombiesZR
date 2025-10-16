using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public enum CardGroup { Common, Forest, Winter, Shop }
public class CardFatherGameObject : MonoBehaviour
{
    public Text GroupText;
    private int nowGroup;//现在的组号
    public List<Button> GroupButtons = new List<Button>();//切换卡片组按钮\
    public Transform CreateShowPlantPositon;
    public GameObject PlantInroduceCard;
    public GameObject ZombieInroduceCard;
    public GameObject CardGroup;
    public bool isZombie;//是否为僵尸图鉴
    /*卡片组号定义：
     0：普通植物
    1：森林植物
    2：雪地植物
    3;商店植物
    */
    private void Start()
    {
        if(LevelReturnCode.CurrentIllustratedMode == IllustratedMode.PlantIllustrated)
        {
            isZombie = false;
        }
        else
        {
            isZombie = true;
        }
        if (!isZombie) GroupText.text = "植物卡组——基础";
        else GroupText.text = "僵尸卡组——基础";

        nowGroup = 0;
    }

    public void ChangeGroup(int Group)
    {
        nowGroup = Group;
        string myString;
        EnvironmentType environmentType = EnvironmentType.Day;
        switch (nowGroup)
        {
            case 0: environmentType = EnvironmentType.Day; break;
            case 1: environmentType = EnvironmentType.Forest; break;
            case 2: environmentType = EnvironmentType.SnowIce; break;
            case 3: environmentType = EnvironmentType.Steel; break;
            case 4: environmentType = EnvironmentType.Other; break;
            case 5: environmentType = EnvironmentType.Special; break;
            case 6: environmentType = EnvironmentType.Phonograph; break;
            case 7: environmentType = EnvironmentType.Collaboration; break;
            default: break;
        }
        if (!isZombie)
        {
            SetAchievement.SetAchievementCompleted("这植物怎么玩啊？");
            switch (nowGroup)
            {
                case 0: myString = "植物卡组——基础"; break;
                case 1: myString = "植物卡组——森林"; break;
                case 2: myString = "植物卡组——雪地"; break;
                case 3: myString = "植物卡组——钢铁"; break;
                case 4: myString = "植物卡组——其它"; break;
                case 5: myString = "植物卡组——稀有"; break;
                case 6: myString = "植物卡组——留声机"; break;
                case 7: myString = "植物卡组——联动"; break;
                default: myString = "植物卡组——基础"; break;
            }
        }
        else
        {
            switch (nowGroup)
            {
                case 0: myString = "僵尸卡组——基础"; break;
                case 1: myString = "僵尸卡组——森林"; break;
                case 2: myString = "僵尸卡组——雪地"; break;
                case 3: myString = "僵尸卡组——钢铁"; break;
                case 4: myString = "僵尸卡组——其它"; break;
                case 5: myString = "此卡组将在后续版本出现"; break;
                case 6: myString = "此卡组将在后续版本出现"; break;
                case 7: myString = "僵尸卡组——联动"; break;
                default: myString = "僵尸卡组——基础"; break;
            }

        }

        int nowXCount = 0;
        int nowYCount = 0;
        foreach(Transform transform in CardGroup.transform)
        {
            Destroy(transform.gameObject);
        }

        if(!isZombie)
        {
            foreach (PlantStruct plantStruct in PlantStructManager.GetPlantStructByEnvironment(environmentType))
            {
                GameObject plant = Instantiate(PlantInroduceCard, CardGroup.transform);
                plant.GetComponent<PlantButtonHandler>().plantId = plantStruct.id;
                plant.GetComponent<PlantButtonHandler>().plantSpawnLocation = CreateShowPlantPositon;

                Vector2 vector2 = plant.transform.position;
                vector2.x += nowXCount * 0.65f;
                vector2.y -= nowYCount * 0.9f;
                plant.transform.position = vector2;

                nowXCount++;
                if (nowXCount >= 6)
                {
                    nowXCount = 0;
                    nowYCount++;
                }
            }
            GroupText.text = myString;
        }
        else
        {
            foreach (ZombieStruct zombieStruct in ZombieStructManager.GetZombieStructByEnvironment(environmentType))
            {
                GameObject plant = Instantiate(ZombieInroduceCard, CardGroup.transform);
                plant.GetComponent<ZombieButtonHandler>().Id = zombieStruct.id;
                plant.GetComponent<ZombieButtonHandler>().SpawnLocation = CreateShowPlantPositon;

                Vector2 vector2 = plant.transform.position;
                vector2.x += nowXCount * 0.65f;
                vector2.y -= nowYCount * 0.9f;
                plant.transform.position = vector2;

                nowXCount++;
                if (nowXCount >= 6)
                {
                    nowXCount = 0;
                    nowYCount++;
                }
            }
            GroupText.text = myString;
        }
        

    }
}
