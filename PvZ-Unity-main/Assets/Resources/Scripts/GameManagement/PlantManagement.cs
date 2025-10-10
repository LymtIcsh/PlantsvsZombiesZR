using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public  class PlantManagement : MonoBehaviour
{
    public static PlantManagement instance; // 添加此行
    [Header("场上植物")]
    public static List<GameObject> PlantsInFieldList = new List<GameObject>();


    public void Start()
    {
        instance = this;
         PlantsInFieldList.Clear();
    }

    public static void AddPlant(GameObject plant)
    {
        print("加入植物");
        PlantsInFieldList.Add(plant);
    }

    public static void RemovePlant(GameObject plant)
    {
        PlantsInFieldList.Remove(plant);
    }

}
