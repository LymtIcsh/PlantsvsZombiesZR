using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PlantIntroduction : MonoBehaviour
{
    public Text PlantNameText;//ֲ����Text
    public Text PlantInfoText;//ֲ����ϢText
    public Transform plantSpawnLocation; // ָ��ֲ��ʵ������λ��
    private void Start()
    {
        PlantNameText.text = "���ֲ���Բ鿴����";
        PlantInfoText.text = "������Ҫ�鿴���ܵ�ֲ��";
    }

    public void ShowIntroduction(string Name,string Info,GameObject plantPrefab) { //��PlantStruct����Ϣ��Text��
        PlantNameText.text = Name;
        PlantInfoText.text = Info;

        foreach (Transform child in plantSpawnLocation)
        {
            // ɾ��������
            Destroy(child.gameObject);
            Debug.Log("ɾ��������: " + child.name);
        }

        // ��ָ��λ��ʵ�����µ�ֲ��
        if (plantPrefab != null && plantSpawnLocation != null)
        {

            GameObject currentPlantInstance = Instantiate(plantPrefab, plantSpawnLocation.position, Quaternion.identity, plantSpawnLocation.transform); // ��ָ��λ��ʵ����ֲ��

            Plant plantScript = currentPlantInstance.GetComponent<Plant>();
            Present plantScript2 = currentPlantInstance.GetComponent<Present>();
            plantScript.initialize(null, "Plant-0", 1);
            if (plantScript != null)
            {
                if(plantScript.detectZombieRegion != null)
                {
                    Destroy(plantScript.detectZombieRegion.gameObject);
                }
                Destroy(plantScript);//ɾ��Plant�ű�
                Debug.Log("ɾ����ֲ���ϵ� Plant �ű�");
            }
            if (plantScript2 != null)
            {
                Destroy(plantScript2);//ɾ��Plant�ű�
                Debug.Log("ɾ����ֲ���ϵ� Plant �ű�");
            }
        }
    }

   
}
