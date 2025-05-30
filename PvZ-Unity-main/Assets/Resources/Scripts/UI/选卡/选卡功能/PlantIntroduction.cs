using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PlantIntroduction : MonoBehaviour
{
    public Text PlantNameText;//植物名Text
    public Text PlantInfoText;//植物信息Text
    public Transform plantSpawnLocation; // 指定植物实例化的位置
    private void Start()
    {
        PlantNameText.text = "点击植物以查看介绍";
        PlantInfoText.text = "请点击需要查看介绍的植物";
    }

    public void ShowIntroduction(string Name,string Info,GameObject plantPrefab) { //将PlantStruct的信息打到Text上
        PlantNameText.text = Name;
        PlantInfoText.text = Info;

        foreach (Transform child in plantSpawnLocation)
        {
            // 删除子物体
            Destroy(child.gameObject);
            Debug.Log("删除子物体: " + child.name);
        }

        // 在指定位置实例化新的植物
        if (plantPrefab != null && plantSpawnLocation != null)
        {

            GameObject currentPlantInstance = Instantiate(plantPrefab, plantSpawnLocation.position, Quaternion.identity, plantSpawnLocation.transform); // 在指定位置实例化植物

            Plant plantScript = currentPlantInstance.GetComponent<Plant>();
            Present plantScript2 = currentPlantInstance.GetComponent<Present>();
            plantScript.initialize(null, "Plant-0", 1);
            if (plantScript != null)
            {
                if(plantScript.detectZombieRegion != null)
                {
                    Destroy(plantScript.detectZombieRegion.gameObject);
                }
                Destroy(plantScript);//删除Plant脚本
                Debug.Log("删除了植物上的 Plant 脚本");
            }
            if (plantScript2 != null)
            {
                Destroy(plantScript2);//删除Plant脚本
                Debug.Log("删除了植物上的 Plant 脚本");
            }
        }
    }

   
}
