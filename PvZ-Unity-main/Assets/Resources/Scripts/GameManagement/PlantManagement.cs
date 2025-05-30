using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public  class PlantManagement : MonoBehaviour
{
    public static PlantManagement instance; // 添加此行
    public static List<GameObject> 场上植物 = new List<GameObject>();


    public void Start()
    {
        instance = this;
         场上植物.Clear();
    }

    public static void AddPlant(GameObject plant)
    {
        print("加入植物");
        场上植物.Add(plant);
    }

    public static void RemovePlant(GameObject plant)
    {
        场上植物.Remove(plant);
    }

}
