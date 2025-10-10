using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class ChooseCardManager : MonoBehaviour
{
    public GameObject cardGroup;
    public GameObject seedBank;
    [FormerlySerializedAs("卡片背景")] [Header("卡片背景")]
    public GameObject _cardBackground;
    [FormerlySerializedAs("卡片")][Header("卡片")] public GameObject _card;
    public int cardWidth = 43;
    public int additionalWidth = 78;
    public GameObject gameManagement;
    public GameObject background;
    public GameObject SeedChooser_Background;
    public GameObject ExitButton;
    public PlantIntroduction plantIntroduction;


    private bool FinishChoosing;//判断是否选卡完毕

    public float moveDistance = -583f;
    public float moveTime = 2f;
    public float delayTime = 2f;

    private List<GameObject> availableCardPositions = new List<GameObject>(); // 用于记录卡片背景的位置
    private List<GameObject> selectedCards = new List<GameObject>(); // 记录已选择的卡片,要改成PRIVATE
    private List<GameObject> waitingCards = new List<GameObject>(); // 记录待选择的卡片
    private Dictionary<GameObject, Vector3> originalPositions = new Dictionary<GameObject, Vector3>(); // 记录卡片的原始世界坐标

    public List<GameObject> Pages = new List<GameObject>();//卡片的页
    private int nowPage = 0;//现在的卡片页码号 
    private int CardPage = 0;//卡片处在那一页中
    void Start()
    {

        seedBank.SetActive(false);
        SeedChooser_Background.SetActive(false);
        ExitButton.SetActive(false);
        FinishChoosing = false;
        plantIntroduction.gameObject.SetActive(false);

        StartChooseCard();
    }

    public void StartChooseCard()
    {
        StartCoroutine(MoveBackground());
    }

    private IEnumerator MoveBackground()
    {
        yield return new WaitForSeconds(0.5f);

        Vector3 startLocalPosition = background.transform.localPosition;
        Vector3 endLocalPosition = new Vector3(startLocalPosition.x + moveDistance, startLocalPosition.y, startLocalPosition.z);
        Vector3 returnLocalPosition = new Vector3(startLocalPosition.x, startLocalPosition.y, startLocalPosition.z);
        float elapsedTime = 0f;
        if (FinishChoosing == false)
        {

            while (elapsedTime < moveTime)
            {
                background.transform.localPosition = Vector3.Lerp(startLocalPosition, endLocalPosition, elapsedTime / moveTime);
                elapsedTime += Time.deltaTime * 2;
                yield return null;
            }
            background.transform.localPosition = endLocalPosition;
        }
        else
        {//如果选过了卡片，跳过向右移动，并且给向左移动重新赋值
            endLocalPosition = startLocalPosition;
            returnLocalPosition = new Vector3(startLocalPosition.x - moveDistance, startLocalPosition.y, startLocalPosition.z);
        }

        if ((GameManagement.levelData.canSelectCardsInFirstPlaythrough || LevelManagerStatic.IsLevelCompleted(GameManagement.level))
            && FinishChoosing == false && !GameManagement.levelData.disableCardSelection && !GameManagement.levelData.ConveyorGame)
        {
            /*
            seedBank.SetActive(true);
            SeedChooser_Background.SetActive(true);
            ExitButton.SetActive(true);
            plantIntroduction.gameObject.SetActive(true);*/
            this.GetComponent<Animator>().SetBool("Start", true);
            GenerateCards(13);
            // int count = cardDict.Count;
            int count = PlantStructManager.GetDataBaseLength();
            InitializeCardsSelected(count);
        }
        else
        {
            yield return new WaitForSeconds(delayTime);
            elapsedTime = 0f;
            while (elapsedTime < moveTime)
            {
                background.transform.localPosition = Vector3.Lerp(endLocalPosition, returnLocalPosition, elapsedTime / moveTime);
                elapsedTime += Time.deltaTime * 2;
                yield return null;
            }
            background.transform.localPosition = returnLocalPosition;

            // 3. 移动完成后调用场景移动后方法
            场景移动后();
        }

    }

    public void 场景移动后()
    {
        gameObject.GetComponent<ShowZombieManager>().ClearDisplayZombies();
        gameManagement.GetComponent<GameManagement>().InitializeGame();
    }
    #region 初始设置
    public void GenerateCards(int cardCount)//设置卡片槽卡片背景
    {
        // 清空现有的卡片容器
        foreach (Transform child in cardGroup.transform)
        {
            Destroy(child.gameObject);
        }
        // 动态生成卡片背景
        for (int i = 0; i < cardCount; i++)
        {
            GameObject newCardBackGround = Instantiate(_cardBackground, cardGroup.transform);
            newCardBackGround.GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);
            availableCardPositions.Add(newCardBackGround); // 记录卡片背景的位置
            RectTransform cardRectTransform = newCardBackGround.GetComponent<RectTransform>();
            cardRectTransform.anchoredPosition = new Vector2(i * cardWidth, 0);
        }

        // 设置cardGroup宽度
        float cardGroupWidth = cardCount * cardWidth - 1;
        cardGroup.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, cardGroupWidth);
        seedBank.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, cardGroupWidth + additionalWidth);
    }

    /// <summary>
    /// 初始化待选择卡片
    /// </summary>
    /// <param name="cardCount"></param>
    public void InitializeCardsSelected(int cardCount)
    {
        foreach(GameObject page in Pages)
        {
            // 清空现有的待选择卡片
            foreach (Transform child in page.transform)
            {
                Destroy(child.gameObject);
            }
        }
        

        //列
        int column = 1;
        //行
        int row = 1;
        float yOffset = 0;

        

        foreach (GameObject page in Pages)
        {
            column = 1;
            row = 1;
            yOffset = 0;
            for (int i = 0; i < 30; i++)
            {

                GameObject cardBackground = Instantiate(_cardBackground, page.transform);
                RectTransform cardRectTransform = cardBackground.GetComponent<RectTransform>();
                cardRectTransform.anchoredPosition = new Vector2((column - 1) * cardWidth, yOffset);
                column++;
                if (column > 6)
                {
                    column = 1;
                    yOffset -= 61;
                }
            }
        }

        column = 1;
        yOffset = 0;
        //植物	
        foreach (PlantStruct plant in PlantStructManager.PlantStructDatabase)
        {
            if ((LevelManagerStatic.IsLevelCompleted(plant.GetLevel) || plant.GetLevel == -1) && plant.envType != EnvironmentType.Other)
            {
                //灰色卡片
                GameObject grayCard = Instantiate(_card, Pages[0].transform);
                Vector3 vector3 = grayCard.transform.position;
                vector3.z += 1;
                grayCard.transform.position = vector3;
                GameObject newCard = Instantiate(_card, Pages[0].transform);

                grayCard.GetComponent<Image>().color = Color.gray;
                grayCard.GetComponent<CardClickListener>().PlantImage.color = Color.gray;
                grayCard.GetComponent<Button>().enabled = false;
                waitingCards.Add(newCard); // 添加到待选择卡片列表
                RectTransform newCardTransform = newCard.GetComponent<RectTransform>();
                newCardTransform.anchoredPosition = new Vector2((column - 1) * cardWidth, yOffset);
                grayCard.GetComponent<RectTransform>().anchoredPosition = new Vector2((column - 1) * cardWidth, yOffset);




                grayCard.GetComponent<CardClickListener>().Initialize(this, plant);
                newCard.GetComponent<CardClickListener>().Initialize(this, plant);

                column++;
                if (column > 6
                    )
                {
                    column = 1;
                    row += 1;
                    yOffset = -(row - 1) * 61;
                }





                grayCard.transform.SetParent(Pages[CardPage].transform);
                newCard.transform.SetParent(Pages[CardPage].transform);


                if (row > 5)
                {
                    row = 1;
                    yOffset = -(row - 1) * 61;
                    CardPage++;
                }
                //设置卡片的初始激活情况
                for (int i = 0; i < Pages.Count; i++)//初始在第一页
                {
                    if (nowPage == i)
                    {
                        foreach (Transform child in Pages[i].transform)
                        {

                            if (!selectedCards.Contains(child.gameObject))
                            {//在卡槽内的卡不改变激活状态
                                child.gameObject.SetActive(true);
                            }

                        }
                    }
                    else
                    {
                        foreach (Transform child in Pages[i].transform)
                        {
                            child.gameObject.SetActive(false);
                        }

                    }
                }

            }



        }
    }
    #endregion
    public void OnCardClicked(GameObject clickedCard)
    {
        print("点击卡片");
        // 如果卡片已被选择，取消选择并返回原位
        if (selectedCards.Contains(clickedCard))
        {
            // 恢复卡片的原始位置
            RectTransform cardRectTransform = clickedCard.GetComponent<RectTransform>();
            Debug.Log(cardRectTransform);
            cardRectTransform.position = originalPositions[clickedCard]; // 设置回原始的世界坐标

            selectedCards.Remove(clickedCard);
            waitingCards.Add(clickedCard);

            //确保取消选择后，不在现在页面的卡篇处于未激活状态
            Transform[] PageTransforms = Pages[nowPage].GetComponentsInChildren<Transform>();
            List<GameObject> CardgameObjects = new List<GameObject>();
            for (int i = 0; i < PageTransforms.Length; i++)
            {
                CardgameObjects.Add(PageTransforms[i].gameObject);
            }
            if (!CardgameObjects.Contains(clickedCard))
            {
                clickedCard.SetActive(false);
            }


            StartCoroutine(RearrangeSelectedCards());
            print("回收卡片");
        }
        else
        {
            if (selectedCards.Count < availableCardPositions.Count)
            {
                RectTransform newCardTransform = clickedCard.GetComponent<RectTransform>();
                originalPositions[clickedCard] = newCardTransform.position;

                GameObject availablePosition = availableCardPositions[selectedCards.Count];
                RectTransform cardRectTransform = clickedCard.GetComponent<RectTransform>();

                Vector3 targetWorldPosition = availablePosition.transform.position;

                // 将卡片的世界坐标设置为目标位置
                cardRectTransform.position = targetWorldPosition;
                selectedCards.Add(clickedCard);
                waitingCards.Remove(clickedCard);
                print("放置卡片");
            }
        }
    }

    public void ShowPlantInfro(CardClickListener card)
    {
        plantIntroduction.ShowIntroduction(card.plant_Struct.ChineseName, card.plant_Struct.briefIntroduction,
            Resources.Load<GameObject>("Prefabs/Plants/" + card.plant_Struct.plantName));
    }

    private IEnumerator RearrangeSelectedCards()//实现卡槽内卡片的紧密排列
    {
        for (int i = 0; i < selectedCards.Count; i++)
        {
            GameObject card = selectedCards[i];
            RectTransform cardRectTransform = card.GetComponent<RectTransform>();
            Vector3 newPosition = availableCardPositions[i].transform.position;
            cardRectTransform.position = newPosition;
        }

        yield return null;
    }

    public void ExitChoosingCard()
    { //结束选卡
        if (!FinishChoosing)
        {
            print("结束选卡");
            StopCoroutine(MoveBackground());
            StartCoroutine(MoveBackground());
            FinishChoosing = true;
            this.GetComponent<Animator>().SetBool("End", true);
        }


        RoadCards();
    }


    public void RoadCards()//将选好的卡片以Card的形式放进卡槽中
    {
        GameManagement.levelData.plantCards.Clear();//清空之前的选卡
        StaticThingsManagement.SavedLastSelectedCards.Clear();
        for (int i = 0; i < selectedCards.Count; i++)
        {
            GameManagement.levelData.plantCards.Add(selectedCards[i].
                GetComponent<CardClickListener>().plant_Struct.plantName);
        }
        for (int i = 0; i < selectedCards.Count; i++)
        {
            StaticThingsManagement.SavedLastSelectedCards.Add(selectedCards[i]);
        }
    }

    public void 重选上次卡牌()
    {

    }
    #region 翻页
    public void NextPage()
    {

        //翻页,下一页     
        nowPage++;
        nowPage = nowPage % Pages.Count;//防止过界
        if (nowPage < 0) { nowPage = Pages.Count - 1; }
        for (int i = 0; i < Pages.Count; i++)//初始在第一页
        {
            if (nowPage == i)
            {
                foreach (Transform child in Pages[i].transform)
                {

                    if (!selectedCards.Contains(child.gameObject))
                    {//在卡槽内的卡不改变激活状态
                        child.gameObject.SetActive(true);
                    }

                }
                // Pages[i].SetActive(true);
            }
            else
            {
                foreach (Transform child in Pages[i].transform)
                {

                    if (!selectedCards.Contains(child.gameObject))
                    {
                        child.gameObject.SetActive(false);
                    }

                }


                //Pages[i].SetActive(false);

            }
        }
    }
    public void LastPage()
    {
        //翻页,上一页     
        nowPage--;
        nowPage = nowPage % Pages.Count;//防止过界
        if (nowPage < 0) { nowPage = Pages.Count - 1; }
        for (int i = 0; i < Pages.Count; i++)//初始在第一页
        {
            if (nowPage == i)
            {
                foreach (Transform child in Pages[i].transform)
                {

                    if (!selectedCards.Contains(child.gameObject))
                    {//在卡槽内的卡不改变激活状态
                        child.gameObject.SetActive(true);
                    }

                }
                // Pages[i].SetActive(true);
            }
            else
            {
                foreach (Transform child in Pages[i].transform)
                {

                    if (!selectedCards.Contains(child.gameObject))
                    {
                        child.gameObject.SetActive(false);
                    }

                }


                //Pages[i].SetActive(false);

            }
        }
    }
    #endregion
}