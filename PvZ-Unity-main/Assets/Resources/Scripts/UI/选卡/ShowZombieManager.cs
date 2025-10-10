using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowZombieManager : MonoBehaviour
{
    public GameObject zombieManager;
    public GameObject[] zombies;
    private Dictionary<string, int> zombiesName = new Dictionary<string, int>();
    public TimeNodes timeNodes;
    public GameObject generateArea;
    List<GameObject> spawnedZombies = new List<GameObject>();

    void Start()
    {
        spawnedZombies.Clear();

        string info = Resources.Load<TextAsset>("Json/ZombieData/Level" + BeginManagement.level).text;
        timeNodes = JsonUtility.FromJson<TimeNodes>(info);
        zombies = Resources.LoadAll<GameObject>("Prefabs/Zombies");

        for (int i = 0; i < zombies.Length; i++)
        {
            zombiesName.Add(zombies[i].name, i);
        }

        SpawnZombiesInArea(10);
    }

    public void SpawnZombiesInArea(int numZombies)
    {
        Dictionary<string, int> zombieGenerationCounts = new Dictionary<string, int>();
        List<ShowZombieWeight> zombieWeights = new List<ShowZombieWeight>();

        foreach (var zombieWeight in timeNodes.zombieWeights)
        {
            ShowZombieWeight showZombieWeight = new ShowZombieWeight
            {
                name = zombieWeight.name,
                weight = zombieWeight.weight
            };
            zombieWeights.Add(showZombieWeight);
        }

        if (zombieWeights.Count == 1)
        {
            zombieGenerationCounts[zombieWeights[0].name] = numZombies;
            GenerateZombies(zombieWeights[0], numZombies, spawnedZombies);
            return;
        }

        int totalWeight = 0;
        foreach (var zombieWeight in zombieWeights)
        {
            totalWeight += (int)(zombieWeight.weight * 100);
        }

        foreach (var zombieWeight in zombieWeights)
        {
            zombieGenerationCounts[zombieWeight.name] = 1;
        }

        int remainingZombies = numZombies - zombieWeights.Count;
        int maxIterations = 100;
        int iterationCount = 0;

        while (remainingZombies > 0 && iterationCount < maxIterations)
        {
            foreach (var zombieWeight in zombieWeights)
            {
                float weightRatio = zombieWeight.weight / (float)totalWeight;
                int zombiesForThisType = Mathf.FloorToInt(weightRatio * remainingZombies);

                if (zombieGenerationCounts[zombieWeight.name] <= 1)
                {
                    zombieGenerationCounts[zombieWeight.name] = 1;
                }
                else
                {
                    zombieGenerationCounts[zombieWeight.name] += zombiesForThisType;
                }

                remainingZombies -= zombiesForThisType;

                if (remainingZombies <= 0) break;
            }

            iterationCount++;

            if (iterationCount >= maxIterations)
            {
                break;
            }
        }

        if (remainingZombies > 0)
        {
            foreach (var zombieWeight in zombieWeights)
            {
                zombieGenerationCounts[zombieWeight.name] += remainingZombies;
                break;
            }
        }

        foreach (var zombieWeight in zombieWeights)
        {
            int countToGenerate = zombieGenerationCounts[zombieWeight.name];

            for (int i = 0; i < countToGenerate; i++)
            {
                GameObject zombieToSpawn = zombies[zombiesName[zombieWeight.name]];
                GameObject spawnedZombie = Instantiate(zombieToSpawn);
                Destroy(spawnedZombie.GetComponent<Zombie>());
                spawnedZombie.GetComponent<Collider2D>().enabled = false;
                spawnedZombie.transform.SetParent(generateArea.transform);
                spawnedZombie.transform.localPosition = new Vector3(
                    Random.Range(3.861f, 6.09f),
                    Random.Range(-2.373f, 1.81f),
                    0
                );
                spawnedZombie.GetComponent<Animator>().SetBool("Idle", true);
                spawnedZombies.Add(spawnedZombie);
            }
        }
    }

    private void GenerateZombies(ShowZombieWeight zombieWeight, int count, List<GameObject> spawnedZombies)
    {
        for (int i = 0; i < count; i++)
        {
            GameObject zombieToSpawn = zombies[zombiesName[zombieWeight.name]];
            GameObject spawnedZombie = Instantiate(zombieToSpawn);
            Destroy(spawnedZombie.GetComponent<Zombie>());
            spawnedZombie.GetComponent<Collider2D>().enabled = false;
            spawnedZombie.transform.SetParent(generateArea.transform);
            spawnedZombie.transform.localPosition = new Vector3(
                Random.Range(3.861f, 6.09f),
                Random.Range(-2.373f, 1.81f),
                0
            );
            
            spawnedZombie.GetComponent<Animator>().SetBool("Idle", true);

            spawnedZombies.Add(spawnedZombie);
        }

    }

/// <summary>
/// Çå³ýÕ¹Ê¾½©Ê¬
/// </summary>
    public void ClearDisplayZombies()
    {
        foreach (GameObject zombie in spawnedZombies)
        {
            Destroy(zombie);
        }
    }
}

[System.Serializable]
public class ShowZombieWeight
{
    public string name;
    public float weight;
}
