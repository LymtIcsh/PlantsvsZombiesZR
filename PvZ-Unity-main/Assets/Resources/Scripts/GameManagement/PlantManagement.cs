using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public  class PlantManagement : MonoBehaviour
{
    public static PlantManagement instance; // ��Ӵ���
    public static List<GameObject> ����ֲ�� = new List<GameObject>();


    public void Start()
    {
        instance = this;
         ����ֲ��.Clear();
    }

    public static void AddPlant(GameObject plant)
    {
        print("����ֲ��");
        ����ֲ��.Add(plant);
    }

    public static void RemovePlant(GameObject plant)
    {
        ����ֲ��.Remove(plant);
    }

}
