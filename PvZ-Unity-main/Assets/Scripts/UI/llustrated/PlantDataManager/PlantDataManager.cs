//using System.Collections.Generic;
//using UnityEngine;
//using System.IO;  // 引入System.IO用于文件操作

//public class PlantDataManager : MonoBehaviour
//{
//    private Dictionary<string, PlantData> plants;  // 使用 PlantData 代替 Plant

//    void Awake()
//    {
//        plants = new Dictionary<string, PlantData>();
//        LoadPlantData();
//    }

//    void LoadPlantData()
//    {
//        // 读取 JSON 文件路径
//        string filePath = "Assets/Resources/Json/Plant.json";  // 指定你的 JSON 文件路径
//        if (File.Exists(filePath))
//        {
//            // 读取文件内容，确保使用 UTF-8 编码
//            string jsonText = File.ReadAllText(filePath, System.Text.Encoding.UTF8);

//            // 将 JSON 内容反序列化为 PlantDataArray
//            PlantDataArray plantArray = JsonUtility.FromJson<PlantDataArray>(jsonText);
//            foreach (var plant in plantArray.plants)
//            {
//                plants.Add(plant.id, plant);  // 添加 PlantData 到字典
//            }
//        }
//        else
//        {
//            Debug.LogError("Plant.json 文件未找到！");
//        }
//    }

//    public PlantData GetPlantById(string id)
//    {
//        if (plants.ContainsKey(id))
//        {
//            return plants[id];
//        }
//        return null;  // 如果没有找到，则返回 null
//    }
//}

//[System.Serializable]
//public class PlantDataArray  // 创建一个 PlantDataArray 类来包装所有的 PlantData 数据
//{
//    public PlantData[] plants;
//}

//// 为了方便解析JSON，我们需要一个包装类
//[System.Serializable]
//public class PlantData  // 修改了类名为 PlantData
//{
//    public string id;
//    public string name;
//    public string description;
//}
