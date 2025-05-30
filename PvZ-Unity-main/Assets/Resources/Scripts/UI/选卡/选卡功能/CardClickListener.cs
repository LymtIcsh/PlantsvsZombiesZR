using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class CardClickListener : MonoBehaviour
{// 卡片点击监听类,用于选卡界面的卡片 
    public PlantStruct plant_Struct;
    private ChooseCardManager manager;


    public Image BackgroundImage;
    public Image PlantImage;
    public Sprite[] BackgroundImages;
    public TextMeshProUGUI SunText;


    public void Initialize(ChooseCardManager manager, PlantStruct my_struct)//填入植物信息结构体
    {
        this.manager = manager;
        // this.card_name = String;
        plant_Struct = my_struct;

        string road = plant_Struct.plantName;

        Sprite sprite = Resources.Load<Sprite>("Sprites/Plants/" + road);//读取对应文件夹的图片文件
        if (sprite != null)
        {
            PlantImage.sprite = sprite;
        }
        else
        {
            print("卡片查找图片失败");
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
        // 触发点击事件
        manager.OnCardClicked(this.gameObject);
        manager.ShowPlantInfro(this);
        print("点击");
    }
  
}