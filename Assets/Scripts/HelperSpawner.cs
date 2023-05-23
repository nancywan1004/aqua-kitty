using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HelperSpawner : Singleton<HelperSpawner>
{
    public List<Transform> SpawnPoints;
    public List<GameObject> HelperPrefabs;
    public int spawnNum;

    // Start is called before the first frame update
    private void Awake()
    {
        spawnHelperRandom(spawnNum);
    }

    public void spawnHelperRandom(int spawnNum)
    {
        int garbageCount = spawnNum;
        List<Transform> nonSpawnedPoints = SpawnPoints;
        List<GameObject> nonSpawnedPrefabs = HelperPrefabs;

        while (garbageCount > 0)
        {
            int randEnemy = Random.Range(0, HelperPrefabs.Count);
            int randSpawnPoint = Random.Range(0, garbageCount);
            Transform randGarbage = nonSpawnedPoints[randSpawnPoint];
            Instantiate(HelperPrefabs[randEnemy], randGarbage.position, Quaternion.identity);
            nonSpawnedPoints.Remove(randGarbage);
            if (HelperPrefabs[randEnemy].transform.childCount > 0 && HelperPrefabs[randEnemy].transform.GetChild(0).CompareTag("Dialogue"))
            {
                nonSpawnedPrefabs.Remove(HelperPrefabs[randEnemy]);
            }
            garbageCount--;

        }
    }
}
