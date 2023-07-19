using System.Collections.Generic;
using CoreRuntime.Garbage;
using CoreSystem.Spawner;
using UnityEngine;

public class GarbageSpawner : Singleton<GarbageSpawner>, IBaseSpawner
{
    [SerializeField] private List<GarbageSpawnerSettingsSO> _garbageSpawnerSettingSOList;
    private GarbageSpawnerSettingsSO _currentSpawnerSetting;

    private float garbageRemained;

    public void SpawnRandom(int spawnNum)
    {
        int garbageCount = spawnNum;
        List<Transform> nonSpawnedPoints = new List<Transform>();
        for (int i = 0; i < transform.childCount; i++)
        {
            if (_currentSpawnerSetting.SpawnIndices.Contains(i))
            {
                nonSpawnedPoints.Add(transform.GetChild(i));
            }
        }

        while (garbageCount > 0)
        {
            int randGarbageIndex = Random.Range(0, _currentSpawnerSetting.Prefabs.Count);
            int randSpawnPointIndex = Random.Range(0, garbageCount);
            Transform randGarbage = nonSpawnedPoints[randSpawnPointIndex];
            Instantiate(_currentSpawnerSetting.Prefabs[randGarbageIndex], randGarbage.position, Quaternion.identity);
            nonSpawnedPoints.Remove(randGarbage);
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

    public void ConfigureSpawnerSetting(int level)
    {
        _currentSpawnerSetting = _garbageSpawnerSettingSOList[level-1];
        SpawnRandom(_currentSpawnerSetting.SpawnNum);
        garbageRemained = _currentSpawnerSetting.SpawnNum;
    }
}
