using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class EndMenu : MonoBehaviour
{
    public Text dialogText;   //子对象DialogText的Text组件，用于更新字体
    public ManagedAudioSource backgroundAudio;   //背景音乐的播放组件
    public GameObject Trophies;
    public GameObject zombieWin;
    [FormerlySerializedAs("已经产生结局提示")] [Header("已经产生结局提示")]
    public bool OutcomePromptBeenGenerated;

    [FormerlySerializedAs("背景")] [Header("背景")]
    public Sprite[] backgroundSpritesAry;
    public void Start()
    {
        OutcomePromptBeenGenerated = false;
    }

    public void gameOver()
    {
        if (!OutcomePromptBeenGenerated)
        {
            //奖杯
            GameObject trophyObj = Instantiate(zombieWin, new Vector3(0, 0, 0), Quaternion.identity);
            trophyObj.GetComponent<ZombieWin>().backgroundAudio = backgroundAudio;
            OutcomePromptBeenGenerated = true;
        }
    }

    public void Win()
    {
        GameManagement.instance.GetComponent<GameManagement>().win();

    }

    
    /// <summary>
    /// 胜利
    /// </summary>
    /// <param name="firstSuccessful">初次通关</param>
    /// <param name="level"></param>
    public void Win(bool firstSuccessful,int level)
    {
        if (!OutcomePromptBeenGenerated)
        {
            //奖杯
            GameObject trophyObj = Instantiate(Trophies, new Vector3(0, 0, 0), Quaternion.identity);
            trophyObj.GetComponent<TrophiesWin>().backgroundAudio = backgroundAudio;
            OutcomePromptBeenGenerated = true;

            if (firstSuccessful && 
                Resources.Load<Sprite>(
                    "Sprites/Plants/" + PlantStructManager.GetPlantStructByGetLevel(level).plantName
                    ) != null)
            {
                Debug.Log("存在");
                trophyObj.GetComponent<SpriteRenderer>().sprite = backgroundSpritesAry[1];
                trophyObj.transform.Find("Card").GetComponent<SpriteRenderer>().sprite = 
                    Resources.Load<Sprite>(
                        "Sprites/Plants/" + PlantStructManager.GetPlantStructByGetLevel(level).plantName
                        );
            }
            else
            {
                trophyObj.GetComponent<SpriteRenderer>().sprite = backgroundSpritesAry[0];
            }
        }
        
    }


}
