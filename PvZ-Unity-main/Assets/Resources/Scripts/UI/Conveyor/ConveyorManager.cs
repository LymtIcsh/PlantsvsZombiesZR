using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConveyorManager : MonoBehaviour
{
    public GameObject cardPrefab;  // 卡片的Prefab
    public RectTransform spawnPoint;   // 卡片生成位置
    public RectTransform targetPoint;  // 目标位置
    public float spawnInterval = 8f;  // 生成间隔时间
    public float moveSpeed = 2f;  // 移动速度
    public float cardWidth = 42f;  // 卡片的宽度

    private List<GameObject> activeCards = new List<GameObject>();  // 当前活动卡片列表
    private List<Coroutine> activeCoroutines = new List<Coroutine>();  // 存储每个卡片的移动协程

    private void Start()
    {
        // 启动生成卡片的协程
        StartCoroutine(SpawnCards());
    }

    private IEnumerator SpawnCards()
    {
        while (true)
        {
            yield return new WaitForSeconds(spawnInterval);

            // 检查卡片数量是否小于10
            if (activeCards.Count >= 11)
                continue;

            // 检查卡片是否可以生成
            Vector3 spawnPosition = spawnPoint.position;

            List<string> plantCards = GameManagement.levelData.plantCards;

            if (plantCards != null && plantCards.Count > 0)
            {
                int randIndex = (plantCards.Count == 1) ? 0 : Random.Range(0, plantCards.Count);
                string randomCard = plantCards[randIndex];

                Debug.Log("选中的卡片是: " + randomCard);

                // 实例化新的卡片并将其作为UI的子物体
                GameObject newCard = Instantiate(cardPrefab, spawnPoint);
                newCard.transform.localPosition = Vector3.zero;

                newCard.GetComponent<Card>().PlantStruct = PlantStructManager.GetPlantStructByName(randomCard);
                newCard.GetComponent<Card>().ConveyorInitialize(gameObject.GetComponent<ConveyorManager>());
                activeCards.Add(newCard);

                // 启动卡片移动到目标位置并记录协程
                Coroutine moveCoroutine = StartCoroutine(MoveCard(newCard, targetPoint.position));
                activeCoroutines.Add(moveCoroutine);
            }
            else
            {
                Debug.LogWarning("plantCards 列表为空或为 null，无法选取卡片！");
            }


            
        }
    }

    private IEnumerator MoveCard(GameObject card, Vector3 target)
    {
        // 确保卡片仍然存在
        if (card == null) yield break;

        // 移动卡片直到到达目标位置
        while (Vector3.Distance(card.transform.position, target) > 0.1f)
        {
            if (card == null) yield break;

            // 检查左侧是否有卡片
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

            // 如果没有阻挡则移动
            if (shouldMove)
            {
                card.transform.position = Vector3.MoveTowards(card.transform.position, target, moveSpeed * Time.deltaTime);
            }

            yield return null;
        }
    }

    // 删除卡片并重排
    public void RemoveCard(GameObject card)
    {
        // 停止卡片的移动协程
        for (int i = 0; i < activeCards.Count; i++)
        {
            if (activeCards[i] == card)
            {
                StopCoroutine(activeCoroutines[i]);  // 停止该卡片的协程
                activeCoroutines.RemoveAt(i);  // 从协程列表中移除
                break;
            }
        }

        // 删除卡片并移除它
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
