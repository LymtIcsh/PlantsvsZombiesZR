using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManagement : MonoBehaviour
{
    public GameObject topMotionPanel;
    public GameObject bottomMotionPanel;
    public GameObject seedBank;
    public GameObject conveyorBelt;
    public GameObject shovelBank;
    public Text levelNameText;
    public GameObject Glove;

    public GameObject cardGroup;   //����Ⱥ��

    private void Awake()
    {
        
    }

    // Start is called before the first frame update
    public void initUI()
    {
        //���عؿ�����
        levelNameText.text = GameManagement.levelData.levelName;

        //���ؿ���Ⱥ�飬���������UI�Ĵ�Сλ��
        List<string> plantCards = GameManagement.levelData.plantCards;
        Debug.Log(plantCards + "UI");
        List<Card> cards = new List<Card>();
        foreach (string plantName in plantCards)
        {
            if(Resources.Load<Object>("Prefabs/UI/Card/" + "TheCard") != null)
            {
              //������Ϣ
                Card newCard = (
                    Instantiate(
                        Resources.Load<Object>("Prefabs/UI/Card/" + "TheCard"),
                        cardGroup.transform
                    ) as GameObject
                ).GetComponent<Card>();

                newCard.PlantStruct = PlantStructManager.GetPlantStructByName(plantName);
               

                cards.Add(newCard
                 );

            }
            else
            {
                Debug.Log(plantName);
            }
            
        }
        GameManagement.instance.SunText.GetComponent<SunNumber>().setCardGroup(cards);
        float cardGroupWidth = plantCards.Count * 43 - 1;
        cardGroup.GetComponent<RectTransform>()
            .SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, cardGroupWidth);
        seedBank.GetComponent<RectTransform>()
            .SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, cardGroupWidth + 78);
        
        seedBank.SetActive(false);
        conveyorBelt.SetActive(false);
    }


    public void appear()
    {
        StartCoroutine(Begin(2.5f));
    }
    private IEnumerator Begin(float delay)
    {
        // �ȴ�ָ�����ӳ�ʱ��
        yield return new WaitForSeconds(delay);
        //����Ⱥ�鱾Ϊ����Ծ���Ա�������ڼ俨����ȴ����
        if(!GameManagement.levelData.ConveyorGame)
        {
            seedBank.SetActive(true);
            cardGroup.SetActive(true);
            conveyorBelt.SetActive(false);
        }
        else
        {
            conveyorBelt.SetActive(true);
            cardGroup.SetActive(false);
            seedBank.SetActive(false);
        }
        
        topMotionPanel.GetComponent<MotionPanel>().startMove();
        bottomMotionPanel.GetComponent<MotionPanel>().startMove();

        
    }
}
