using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EndMenu : MonoBehaviour
{
    public Text dialogText;   //子对象DialogText的Text组件，用于更新字体
    public ManagedAudioSource backgroundAudio;   //背景音乐的播放组件
    public GameObject Trophies;
    public GameObject zombieWin;
    public bool 已经产生结局提示;

    public Sprite[] 背景;
    public void Start()
    {
        已经产生结局提示 = false;
    }

    public void gameOver()
    {
        if (!已经产生结局提示)
        {
            GameObject 奖杯 = Instantiate(zombieWin, new Vector3(0, 0, 0), Quaternion.identity);
            奖杯.GetComponent<ZombieWin>().backgroundAudio = backgroundAudio;
            已经产生结局提示 = true;
        }
    }

    public void win()
    {
        GameManagement.instance.GetComponent<GameManagement>().win();

    }

    public void win(bool 初次通关,int level)
    {
        if (!已经产生结局提示)
        {
            GameObject 奖杯 = Instantiate(Trophies, new Vector3(0, 0, 0), Quaternion.identity);
            奖杯.GetComponent<TrophiesWin>().backgroundAudio = backgroundAudio;
            已经产生结局提示 = true;

            if (初次通关 && 
                Resources.Load<Sprite>(
                    "Sprites/Plants/" + PlantStructManager.GetPlantStructByGetLevel(level).plantName
                    ) != null)
            {
                Debug.Log("存在");
                奖杯.GetComponent<SpriteRenderer>().sprite = 背景[1];
                奖杯.transform.Find("Card").GetComponent<SpriteRenderer>().sprite = 
                    Resources.Load<Sprite>(
                        "Sprites/Plants/" + PlantStructManager.GetPlantStructByGetLevel(level).plantName
                        );
            }
            else
            {
                奖杯.GetComponent<SpriteRenderer>().sprite = 背景[0];
            }
        }
        
    }


}
