using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public  class PlantManagement : MonoBehaviour
{
    public static PlantManagement instance; // ��Ӵ���
    [Header("����ֲ��")]
    public static List<GameObject> PlantsInFieldList = new List<GameObject>();


    public void Start()
    {
        instance = this;
         PlantsInFieldList.Clear();
    }

    public static void AddPlant(GameObject plant)
    {
        print("����ֲ��");
        PlantsInFieldList.Add(plant);
    }

    public static void RemovePlant(GameObject plant)
    {
        PlantsInFieldList.Remove(plant);
    }

}
