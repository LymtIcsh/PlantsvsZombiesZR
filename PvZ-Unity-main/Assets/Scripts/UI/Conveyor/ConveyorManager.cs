using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConveyorManager : MonoBehaviour
{
    public GameObject cardPrefab;  // ��Ƭ��Prefab
    public RectTransform spawnPoint;   // ��Ƭ����λ��
    public RectTransform targetPoint;  // Ŀ��λ��
    public float spawnInterval = 8f;  // ���ɼ��ʱ��
    public float moveSpeed = 2f;  // �ƶ��ٶ�
    public float cardWidth = 42f;  // ��Ƭ�Ŀ��

    private List<GameObject> activeCards = new List<GameObject>();  // ��ǰ���Ƭ�б�
    private List<Coroutine> activeCoroutines = new List<Coroutine>();  // �洢ÿ����Ƭ���ƶ�Э��

    private void Start()
    {
        // �������ɿ�Ƭ��Э��
        StartCoroutine(SpawnCards());
    }

    private IEnumerator SpawnCards()
    {
        while (true)
        {
            yield return new WaitForSeconds(spawnInterval);

            // ��鿨Ƭ�����Ƿ�С��10
            if (activeCards.Count >= 11)
                continue;

            // ��鿨Ƭ�Ƿ��������
            Vector3 spawnPosition = spawnPoint.position;

            List<string> plantCards = GameManagement.levelData.plantCards;

            if (plantCards != null && plantCards.Count > 0)
            {
                int randIndex = (plantCards.Count == 1) ? 0 : Random.Range(0, plantCards.Count);
                string randomCard = plantCards[randIndex];

                Debug.Log("ѡ�еĿ�Ƭ��: " + randomCard);

                // ʵ�����µĿ�Ƭ��������ΪUI��������
                GameObject newCard = Instantiate(cardPrefab, spawnPoint);
                newCard.transform.localPosition = Vector3.zero;

                newCard.GetComponent<Card>().PlantStruct = PlantStructManager.GetPlantStructByName(randomCard);
                newCard.GetComponent<Card>().ConveyorInitialize(gameObject.GetComponent<ConveyorManager>());
                activeCards.Add(newCard);

                // ������Ƭ�ƶ���Ŀ��λ�ò���¼Э��
                Coroutine moveCoroutine = StartCoroutine(MoveCard(newCard, targetPoint.position));
                activeCoroutines.Add(moveCoroutine);
            }
            else
            {
                Debug.LogWarning("plantCards �б�Ϊ�ջ�Ϊ null���޷�ѡȡ��Ƭ��");
            }


            
        }
    }

    private IEnumerator MoveCard(GameObject card, Vector3 target)
    {
        // ȷ����Ƭ��Ȼ����
        if (card == null) yield break;

        // �ƶ���Ƭֱ������Ŀ��λ��
        while (Vector3.Distance(card.transform.position, target) > 0.1f)
        {
            if (card == null) yield break;

            // �������Ƿ��п�Ƭ
            bool shouldMove = true;
            foreach (GameObject otherCard in activeCards)
            {
                if (otherCard != card &&
                    otherCard.transform.position.x < card.transform.position.x &&
                    Vector3.Distance(otherCard.transform.position, card.transform.position) < 0.625f)
                {
                    shouldMove = false;
                    break;
                }
            }

            // ���û���赲���ƶ�
            if (shouldMove)
            {
                card.transform.position = Vector3.MoveTowards(card.transform.position, target, moveSpeed * Time.deltaTime);
            }

            yield return null;
        }
    }

    // ɾ����Ƭ������
    public void RemoveCard(GameObject card)
    {
        // ֹͣ��Ƭ���ƶ�Э��
        for (int i = 0; i < activeCards.Count; i++)
        {
            if (activeCards[i] == card)
            {
                StopCoroutine(activeCoroutines[i]);  // ֹͣ�ÿ�Ƭ��Э��
                activeCoroutines.RemoveAt(i);  // ��Э���б����Ƴ�
                break;
            }
        }

        // ɾ����Ƭ���Ƴ���
        activeCards.Remove(card);
        Destroy(card);
        Debug.Log("Card has been used and removed.");

        // Rearrange the remaining cards
        //RearrangeCards();
    }

    // Rearrange cards if there's space
    private void RearrangeCards()
    {
        for (int i = 0; i < activeCards.Count; i++)
        {
            float newXPosition = spawnPoint.position.x + (i * cardWidth);
            activeCards[i].transform.position = new Vector3(newXPosition, targetPoint.position.y, targetPoint.position.z);
        }
    }
}
