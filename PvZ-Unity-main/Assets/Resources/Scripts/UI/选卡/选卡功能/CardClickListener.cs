using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class CardClickListener : MonoBehaviour
{// ��Ƭ���������,����ѡ������Ŀ�Ƭ 
    public PlantStruct plant_Struct;
    private ChooseCardManager manager;


    public Image BackgroundImage;
    public Image PlantImage;
    public Sprite[] BackgroundImages;
    public TextMeshProUGUI SunText;


    public void Initialize(ChooseCardManager manager, PlantStruct my_struct)//����ֲ����Ϣ�ṹ��
    {
        this.manager = manager;
        // this.card_name = String;
        plant_Struct = my_struct;

        string road = plant_Struct.plantName;

        Sprite sprite = Resources.Load<Sprite>("Sprites/Plants/" + road);//��ȡ��Ӧ�ļ��е�ͼƬ�ļ�
        if (sprite != null)
        {
            PlantImage.sprite = sprite;
        }
        else
        {
            print("��Ƭ����ͼƬʧ��");
        }

        switch (plant_Struct.envType)
        {
            case EnvironmentType.Day: BackgroundImage.sprite = BackgroundImages[0]; break;
            //case EnvironmentType.Night: BackgroundImage.sprite = BackgroundImages[1]; break;
            case EnvironmentType.Forest: BackgroundImage.sprite = BackgroundImages[2]; break;
            case EnvironmentType.SnowIce: BackgroundImage.sprite = BackgroundImages[3]; break;
            case EnvironmentType.Steel: BackgroundImage.sprite = BackgroundImages[4]; break;
            case EnvironmentType.Special: BackgroundImage.sprite = BackgroundImages[5]; break;
            case EnvironmentType.Collaboration: BackgroundImage.sprite = BackgroundImages[6]; break;
            default: BackgroundImage.sprite = BackgroundImages[0]; break;
        }

        SunText.text = plant_Struct.Cost.ToString();

    }

    public void Click()
    {
        AudioManager.Instance.PlaySoundEffect(5);
        // ��������¼�
        manager.OnCardClicked(this.gameObject);
        manager.ShowPlantInfro(this);
        print("���");
    }
  
}