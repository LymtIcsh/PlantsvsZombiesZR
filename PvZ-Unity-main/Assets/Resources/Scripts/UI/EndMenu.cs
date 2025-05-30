using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EndMenu : MonoBehaviour
{
    public Text dialogText;   //�Ӷ���DialogText��Text��������ڸ�������
    public ManagedAudioSource backgroundAudio;   //�������ֵĲ������
    public GameObject Trophies;
    public GameObject zombieWin;
    public bool �Ѿ����������ʾ;

    public Sprite[] ����;
    public void Start()
    {
        �Ѿ����������ʾ = false;
    }

    public void gameOver()
    {
        if (!�Ѿ����������ʾ)
        {
            GameObject ���� = Instantiate(zombieWin, new Vector3(0, 0, 0), Quaternion.identity);
            ����.GetComponent<ZombieWin>().backgroundAudio = backgroundAudio;
            �Ѿ����������ʾ = true;
        }
    }

    public void win()
    {
        GameManagement.instance.GetComponent<GameManagement>().win();

    }

    public void win(bool ����ͨ��,int level)
    {
        if (!�Ѿ����������ʾ)
        {
            GameObject ���� = Instantiate(Trophies, new Vector3(0, 0, 0), Quaternion.identity);
            ����.GetComponent<TrophiesWin>().backgroundAudio = backgroundAudio;
            �Ѿ����������ʾ = true;

            if (����ͨ�� && 
                Resources.Load<Sprite>(
                    "Sprites/Plants/" + PlantStructManager.GetPlantStructByGetLevel(level).plantName
                    ) != null)
            {
                Debug.Log("����");
                ����.GetComponent<SpriteRenderer>().sprite = ����[1];
                ����.transform.Find("Card").GetComponent<SpriteRenderer>().sprite = 
                    Resources.Load<Sprite>(
                        "Sprites/Plants/" + PlantStructManager.GetPlantStructByGetLevel(level).plantName
                        );
            }
            else
            {
                ����.GetComponent<SpriteRenderer>().sprite = ����[0];
            }
        }
        
    }


}
