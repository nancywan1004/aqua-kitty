using System.Collections.Generic;
using System.Linq;
using CoreRuntime.Garbage;
using UnityEngine;

public class GarbageSpawner : Singleton<GarbageSpawner>
{
    [SerializeField] private GarbageSpawnerSSO _garbageSpawnerSetting;

    private float garbageRemained;

    private void Awake()
    {
        SpawnGarbageRandom(_garbageSpawnerSetting.SpawnNumber);
        garbageRemained = _garbageSpawnerSetting.SpawnNumber;
    }

    public void SpawnGarbageRandom(int spawnNum)
    {
        int garbageCount = spawnNum;
        List<Transform> nonSpawnedPoints = new List<Transform>();
        for (int i = 0; i < transform.childCount; i++)
        {
            if (_garbageSpawnerSetting.SpawnPointIndices.Contains(i))
            {
                nonSpawnedPoints.Add(transform.GetChild(i));
            }
        }
        List<GameObject> nonSpawnedPrefabs = _garbageSpawnerSetting.GarbagePrefabs;

        while (garbageCount > 0)
        {
            int randGarbageIndex = Random.Range(0, _garbageSpawnerSetting.GarbagePrefabs.Count);
            int randSpawnPointIndex = Random.Range(0, garbageCount);
            Transform randGarbage = nonSpawnedPoints[randSpawnPointIndex];
            Instantiate(_garbageSpawnerSetting.GarbagePrefabs[randGarbageIndex], randGarbage.position, Quaternion.identity);
            nonSpawnedPoints.Remove(randGarbage);
            if (_garbageSpawnerSetting.GarbagePrefabs[randGarbageIndex].transform.childCount > 0) {
                nonSpawnedPrefabs.Remove(_garbageSpawnerSetting.GarbagePrefabs[randGarbageIndex]);
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

    public void ConfigureSpawnerSetting(GarbageSpawnerSSO levelSetting)
    {
        _garbageSpawnerSetting = levelSetting;
    }
}
