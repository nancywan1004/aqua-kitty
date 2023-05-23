using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GarbageSpawner : Singleton<GarbageSpawner>
{
    public List<Transform> SpawnPoints;
    public List<GameObject> EnemyPrefabs;
    public int spawnNum;

    private float garbageRemained;

    private void Awake()
    {
        spawnGarbageRandom(spawnNum);
        garbageRemained = spawnNum;
    }

    public void spawnGarbageRandom(int spawnNum)
    {
        int garbageCount = spawnNum;
        List<Transform> nonSpawnedPoints = SpawnPoints;
        List<GameObject> nonSpawnedPrefabs = EnemyPrefabs;

        while (garbageCount > 0)
        {
            int randEnemy = Random.Range(0, EnemyPrefabs.Count);
            int randSpawnPoint = Random.Range(0, garbageCount);
            Transform randGarbage = nonSpawnedPoints[randSpawnPoint];
            Instantiate(EnemyPrefabs[randEnemy], randGarbage.position, Quaternion.identity);
            nonSpawnedPoints.Remove(randGarbage);
            if (EnemyPrefabs[randEnemy].transform.childCount > 0 && EnemyPrefabs[randEnemy].transform.GetChild(0).CompareTag("Dialogue")) {
                nonSpawnedPrefabs.Remove(EnemyPrefabs[randEnemy]);
            } 
            garbageCount--;

        }
    }

    public void RemoveGarbage()
    {
        if (garbageRemained > 0)
        {
            garbageRemained--;
        }
    }

    public float GetGarbageRemained()
    {
        return garbageRemained;
    }
}
