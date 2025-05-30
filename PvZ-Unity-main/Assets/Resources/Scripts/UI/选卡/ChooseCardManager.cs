using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ChooseCardManager : MonoBehaviour
{
    public GameObject cardGroup;
    public GameObject seedBank;
    public GameObject ��Ƭ����;
    public GameObject ��Ƭ;
    public int cardWidth = 43;
    public int additionalWidth = 78;
    public GameObject gameManagement;
    public GameObject background;
    public GameObject SeedChooser_Background;
    public GameObject ExitButton;
    public PlantIntroduction plantIntroduction;


    private bool FinishChoosing;//�ж��Ƿ�ѡ�����

    public float moveDistance = -583f;
    public float moveTime = 2f;
    public float delayTime = 2f;

    private List<GameObject> availableCardPositions = new List<GameObject>(); // ���ڼ�¼��Ƭ������λ��
    private List<GameObject> selectedCards = new List<GameObject>(); // ��¼��ѡ��Ŀ�Ƭ,Ҫ�ĳ�PRIVATE
    private List<GameObject> waitingCards = new List<GameObject>(); // ��¼��ѡ��Ŀ�Ƭ
    private Dictionary<GameObject, Vector3> originalPositions = new Dictionary<GameObject, Vector3>(); // ��¼��Ƭ��ԭʼ��������

    public List<GameObject> Pages = new List<GameObject>();//��Ƭ��ҳ
    private int nowPage = 0;//���ڵĿ�Ƭҳ��� 
    private int CardPage = 0;//��Ƭ������һҳ��
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
        {//���ѡ���˿�Ƭ�����������ƶ������Ҹ������ƶ����¸�ֵ
            endLocalPosition = startLocalPosition;
            returnLocalPosition = new Vector3(startLocalPosition.x - moveDistance, startLocalPosition.y, startLocalPosition.z);
        }

        if ((GameManagement.levelData.һ��Ŀ��ѡ�� || LevelManagerStatic.IsLevelCompleted(GameManagement.level))
            && FinishChoosing == false && !GameManagement.levelData.��ֹ�κ���Ŀѡ�� && !GameManagement.levelData.ConveyorGame)
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
            ��ʼ����ѡ��Ƭ(count);
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

            // 3. �ƶ���ɺ���ó����ƶ��󷽷�
            �����ƶ���();
        }

    }

    public void �����ƶ���()
    {
        gameObject.GetComponent<ShowZombieManager>().���չʾ��ʬ();
        gameManagement.GetComponent<GameManagement>().��ʼ����Ϸ();
    }
    #region ��ʼ����
    public void GenerateCards(int cardCount)//���ÿ�Ƭ�ۿ�Ƭ����
    {
        // ������еĿ�Ƭ����
        foreach (Transform child in cardGroup.transform)
        {
            Destroy(child.gameObject);
        }
        // ��̬���ɿ�Ƭ����
        for (int i = 0; i < cardCount; i++)
        {
            GameObject newCardBackGround = Instantiate(��Ƭ����, cardGroup.transform);
            newCardBackGround.GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);
            availableCardPositions.Add(newCardBackGround); // ��¼��Ƭ������λ��
            RectTransform cardRectTransform = newCardBackGround.GetComponent<RectTransform>();
            cardRectTransform.anchoredPosition = new Vector2(i * cardWidth, 0);
        }

        // ����cardGroup���
        float cardGroupWidth = cardCount * cardWidth - 1;
        cardGroup.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, cardGroupWidth);
        seedBank.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, cardGroupWidth + additionalWidth);
    }

    public void ��ʼ����ѡ��Ƭ(int cardCount)
    {
        foreach(GameObject page in Pages)
        {
            // ������еĴ�ѡ��Ƭ
            foreach (Transform child in page.transform)
            {
                Destroy(child.gameObject);
            }
        }
        

        int �� = 1;
        int �� = 1;
        float yOffset = 0;

        

        foreach (GameObject page in Pages)
        {
            �� = 1;
            �� = 1;
            yOffset = 0;
            for (int i = 0; i < 30; i++)
            {

                GameObject cardBackground = Instantiate(��Ƭ����, page.transform);
                RectTransform cardRectTransform = cardBackground.GetComponent<RectTransform>();
                cardRectTransform.anchoredPosition = new Vector2((�� - 1) * cardWidth, yOffset);
                ��++;
                if (�� > 6)
                {
                    �� = 1;
                    yOffset -= 61;
                }
            }
        }

        �� = 1;
        yOffset = 0;
        foreach (PlantStruct ֲ�� in PlantStructManager.PlantStructDatabase)
        {
            if ((LevelManagerStatic.IsLevelCompleted(ֲ��.GetLevel) || ֲ��.GetLevel == -1) && ֲ��.envType != EnvironmentType.Other)
            {
                GameObject ��ɫ��Ƭ = Instantiate(��Ƭ, Pages[0].transform);
                Vector3 vector3 = ��ɫ��Ƭ.transform.position;
                vector3.z += 1;
                ��ɫ��Ƭ.transform.position = vector3;
                GameObject newCard = Instantiate(��Ƭ, Pages[0].transform);

                ��ɫ��Ƭ.GetComponent<Image>().color = Color.gray;
                ��ɫ��Ƭ.GetComponent<CardClickListener>().PlantImage.color = Color.gray;
                ��ɫ��Ƭ.GetComponent<Button>().enabled = false;
                waitingCards.Add(newCard); // ��ӵ���ѡ��Ƭ�б�
                RectTransform newCardTransform = newCard.GetComponent<RectTransform>();
                newCardTransform.anchoredPosition = new Vector2((�� - 1) * cardWidth, yOffset);
                ��ɫ��Ƭ.GetComponent<RectTransform>().anchoredPosition = new Vector2((�� - 1) * cardWidth, yOffset);




                ��ɫ��Ƭ.GetComponent<CardClickListener>().Initialize(this, ֲ��);
                newCard.GetComponent<CardClickListener>().Initialize(this, ֲ��);

                ��++;
                if (�� > 6
                    )
                {
                    �� = 1;
                    �� += 1;
                    yOffset = -(�� - 1) * 61;
                }





                ��ɫ��Ƭ.transform.SetParent(Pages[CardPage].transform);
                newCard.transform.SetParent(Pages[CardPage].transform);


                if (�� > 5)
                {
                    �� = 1;
                    yOffset = -(�� - 1) * 61;
                    CardPage++;
                }
                //���ÿ�Ƭ�ĳ�ʼ�������
                for (int i = 0; i < Pages.Count; i++)//��ʼ�ڵ�һҳ
                {
                    if (nowPage == i)
                    {
                        foreach (Transform child in Pages[i].transform)
                        {

                            if (!selectedCards.Contains(child.gameObject))
                            {//�ڿ����ڵĿ����ı伤��״̬
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
        print("�����Ƭ");
        // �����Ƭ�ѱ�ѡ��ȡ��ѡ�񲢷���ԭλ
        if (selectedCards.Contains(clickedCard))
        {
            // �ָ���Ƭ��ԭʼλ��
            RectTransform cardRectTransform = clickedCard.GetComponent<RectTransform>();
            Debug.Log(cardRectTransform);
            cardRectTransform.position = originalPositions[clickedCard]; // ���û�ԭʼ����������

            selectedCards.Remove(clickedCard);
            waitingCards.Add(clickedCard);

            //ȷ��ȡ��ѡ��󣬲�������ҳ��Ŀ�ƪ����δ����״̬
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
            print("���տ�Ƭ");
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

                // ����Ƭ��������������ΪĿ��λ��
                cardRectTransform.position = targetWorldPosition;
                selectedCards.Add(clickedCard);
                waitingCards.Remove(clickedCard);
                print("���ÿ�Ƭ");
            }
        }
    }

    public void ShowPlantInfro(CardClickListener card)
    {
        plantIntroduction.ShowIntroduction(card.plant_Struct.ChineseName, card.plant_Struct.briefIntroduction,
            Resources.Load<GameObject>("Prefabs/Plants/" + card.plant_Struct.plantName));
    }

    private IEnumerator RearrangeSelectedCards()//ʵ�ֿ����ڿ�Ƭ�Ľ�������
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
    { //����ѡ��
        if (!FinishChoosing)
        {
            print("����ѡ��");
            StopCoroutine(MoveBackground());
            StartCoroutine(MoveBackground());
            FinishChoosing = true;
            this.GetComponent<Animator>().SetBool("End", true);
        }


        RoadCards();
    }


    public void RoadCards()//��ѡ�õĿ�Ƭ��Card����ʽ�Ž�������
    {
        GameManagement.levelData.plantCards.Clear();//���֮ǰ��ѡ��
        StaticThingsManagement.�����ϴ�ѡ����.Clear();
        for (int i = 0; i < selectedCards.Count; i++)
        {
            GameManagement.levelData.plantCards.Add(selectedCards[i].
                GetComponent<CardClickListener>().plant_Struct.plantName);
        }
        for (int i = 0; i < selectedCards.Count; i++)
        {
            StaticThingsManagement.�����ϴ�ѡ����.Add(selectedCards[i]);
        }
    }

    public void ��ѡ�ϴο���()
    {

    }
    #region ��ҳ
    public void NextPage()
    {

        //��ҳ,��һҳ     
        nowPage++;
        nowPage = nowPage % Pages.Count;//��ֹ����
        if (nowPage < 0) { nowPage = Pages.Count - 1; }
        for (int i = 0; i < Pages.Count; i++)//��ʼ�ڵ�һҳ
        {
            if (nowPage == i)
            {
                foreach (Transform child in Pages[i].transform)
                {

                    if (!selectedCards.Contains(child.gameObject))
                    {//�ڿ����ڵĿ����ı伤��״̬
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
        //��ҳ,��һҳ     
        nowPage--;
        nowPage = nowPage % Pages.Count;//��ֹ����
        if (nowPage < 0) { nowPage = Pages.Count - 1; }
        for (int i = 0; i < Pages.Count; i++)//��ʼ�ڵ�һҳ
        {
            if (nowPage == i)
            {
                foreach (Transform child in Pages[i].transform)
                {

                    if (!selectedCards.Contains(child.gameObject))
                    {//�ڿ����ڵĿ����ı伤��״̬
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


