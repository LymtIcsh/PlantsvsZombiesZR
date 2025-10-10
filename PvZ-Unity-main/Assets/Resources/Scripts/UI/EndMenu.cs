using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class EndMenu : MonoBehaviour
{
    public Text dialogText;   //�Ӷ���DialogText��Text��������ڸ�������
    public ManagedAudioSource backgroundAudio;   //�������ֵĲ������
    public GameObject Trophies;
    public GameObject zombieWin;
    [FormerlySerializedAs("�Ѿ����������ʾ")] [Header("�Ѿ����������ʾ")]
    public bool OutcomePromptBeenGenerated;

    [FormerlySerializedAs("����")] [Header("����")]
    public Sprite[] backgroundSpritesAry;
    public void Start()
    {
        OutcomePromptBeenGenerated = false;
    }

    public void gameOver()
    {
        if (!OutcomePromptBeenGenerated)
        {
            //����
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
    /// ʤ��
    /// </summary>
    /// <param name="firstSuccessful">����ͨ��</param>
    /// <param name="level"></param>
    public void Win(bool firstSuccessful,int level)
    {
        if (!OutcomePromptBeenGenerated)
        {
            //����
            GameObject trophyObj = Instantiate(Trophies, new Vector3(0, 0, 0), Quaternion.identity);
            trophyObj.GetComponent<TrophiesWin>().backgroundAudio = backgroundAudio;
            OutcomePromptBeenGenerated = true;

            if (firstSuccessful && 
                Resources.Load<Sprite>(
                    "Sprites/Plants/" + PlantStructManager.GetPlantStructByGetLevel(level).plantName
                    ) != null)
            {
                Debug.Log("����");
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
