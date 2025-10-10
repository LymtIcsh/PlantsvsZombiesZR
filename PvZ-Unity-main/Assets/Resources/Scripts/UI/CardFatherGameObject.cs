using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public enum CardGroup { Common, Forest, Winter, Shop }
public class CardFatherGameObject : MonoBehaviour
{
    public Text GroupText;
    private int nowGroup;//���ڵ����
    public List<Button> GroupButtons = new List<Button>();//�л���Ƭ�鰴ť\
    public Transform CreateShowPlantPositon;
    public GameObject PlantInroduceCard;
    public GameObject ZombieInroduceCard;
    public GameObject CardGroup;
    public bool isZombie;//�Ƿ�Ϊ��ʬͼ��
    /*��Ƭ��Ŷ��壺
     0����ֲͨ��
    1��ɭ��ֲ��
    2��ѩ��ֲ��
    3;�̵�ֲ��
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
        if (!isZombie) GroupText.text = "ֲ�￨�顪������";
        else GroupText.text = "��ʬ���顪������";

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
            SetAchievement.SetAchievementCompleted("��ֲ����ô�氡��");
            switch (nowGroup)
            {
                case 0: myString = "ֲ�￨�顪������"; break;
                case 1: myString = "ֲ�￨�顪��ɭ��"; break;
                case 2: myString = "ֲ�￨�顪��ѩ��"; break;
                case 3: myString = "ֲ�￨�顪������"; break;
                case 4: myString = "ֲ�￨�顪������"; break;
                case 5: myString = "ֲ�￨�顪��ϡ��"; break;
                case 6: myString = "ֲ�￨�顪��������"; break;
                case 7: myString = "ֲ�￨�顪������"; break;
                default: myString = "ֲ�￨�顪������"; break;
            }
        }
        else
        {
            switch (nowGroup)
            {
                case 0: myString = "��ʬ���顪������"; break;
                case 1: myString = "��ʬ���顪��ɭ��"; break;
                case 2: myString = "��ʬ���顪��ѩ��"; break;
                case 3: myString = "��ʬ���顪������"; break;
                case 4: myString = "��ʬ���顪������"; break;
                case 5: myString = "�˿��齫�ں����汾����"; break;
                case 6: myString = "�˿��齫�ں����汾����"; break;
                case 7: myString = "��ʬ���顪������"; break;
                default: myString = "��ʬ���顪������"; break;
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
