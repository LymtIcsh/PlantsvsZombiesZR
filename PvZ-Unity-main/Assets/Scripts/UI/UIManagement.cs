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

    public GameObject cardGroup;   //卡槽群组

    private void Awake()
    {
        
    }

    // Start is called before the first frame update
    public void initUI()
    {
        //加载关卡名字
        levelNameText.text = GameManagement.levelData.levelName;

        //加载卡槽群组，并设置相关UI的大小位置
        List<string> plantCards = GameManagement.levelData.plantCards;
        Debug.Log(plantCards + "UI");
        List<Card> cards = new List<Card>();
        foreach (string plantName in plantCards)
        {
            if(Resources.Load<Object>("Prefabs/UI/Card/" + "TheCard") != null)
            {
              //填入信息
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
        // 等待指定的延迟时间
        yield return new WaitForSeconds(delay);
        //卡槽群组本为不活跃，以避免剧情期间卡槽冷却减少
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
