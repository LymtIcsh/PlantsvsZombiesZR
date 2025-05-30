//using System.Collections.Generic;
//using UnityEngine;
//using System.IO;  // ����System.IO�����ļ�����

//public class PlantDataManager : MonoBehaviour
//{
//    private Dictionary<string, PlantData> plants;  // ʹ�� PlantData ���� Plant

//    void Awake()
//    {
//        plants = new Dictionary<string, PlantData>();
//        LoadPlantData();
//    }

//    void LoadPlantData()
//    {
//        // ��ȡ JSON �ļ�·��
//        string filePath = "Assets/Resources/Json/Plant.json";  // ָ����� JSON �ļ�·��
//        if (File.Exists(filePath))
//        {
//            // ��ȡ�ļ����ݣ�ȷ��ʹ�� UTF-8 ����
//            string jsonText = File.ReadAllText(filePath, System.Text.Encoding.UTF8);

//            // �� JSON ���ݷ����л�Ϊ PlantDataArray
//            PlantDataArray plantArray = JsonUtility.FromJson<PlantDataArray>(jsonText);
//            foreach (var plant in plantArray.plants)
//            {
//                plants.Add(plant.id, plant);  // ��� PlantData ���ֵ�
//            }
//        }
//        else
//        {
//            Debug.LogError("Plant.json �ļ�δ�ҵ���");
//        }
//    }

//    public PlantData GetPlantById(string id)
//    {
//        if (plants.ContainsKey(id))
//        {
//            return plants[id];
//        }
//        return null;  // ���û���ҵ����򷵻� null
//    }
//}

//[System.Serializable]
//public class PlantDataArray  // ����һ�� PlantDataArray ������װ���е� PlantData ����
//{
//    public PlantData[] plants;
//}

//// Ϊ�˷������JSON��������Ҫһ����װ��
//[System.Serializable]
//public class PlantData  // �޸�������Ϊ PlantData
//{
//    public string id;
//    public string name;
//    public string description;
//}
