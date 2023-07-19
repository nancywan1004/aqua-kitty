using System.Collections.Generic;
using CoreRuntime.Helpers;
using CoreSystem.Spawner;
using UnityEngine;

public class HelperSpawner : Singleton<HelperSpawner>, IBaseSpawner
{
    [SerializeField] private List<HelperSpawnerSettingsSO> _helperSpawnerSettingsSOList;
    private HelperSpawnerSettingsSO _currentSpawnerSetting;

    public void SpawnRandom(int spawnNum)
    {
        int helperCount = spawnNum;
        List<Transform> nonSpawnedPoints = new List<Transform>();
        for (int i = 0; i < transform.childCount; i++)
        {
            if (_currentSpawnerSetting.SpawnIndices.Contains(i))
            {
                nonSpawnedPoints.Add(transform.GetChild(i));
            }
        }
        while (helperCount > 0)
        {
            int randEnemy = Random.Range(0, _currentSpawnerSetting.Prefabs.Count);
            int randSpawnPoint = Random.Range(0, helperCount);
            Transform randHelper = nonSpawnedPoints[randSpawnPoint];
            Instantiate(_currentSpawnerSetting.Prefabs[randEnemy], randHelper.position, Quaternion.identity);
            nonSpawnedPoints.Remove(randHelper);
            helperCount--;

        }
    }

    public void ConfigureSpawnerSetting(int level)
    {
        _currentSpawnerSetting = _helperSpawnerSettingsSOList[level-1];
        if (level > 1)
        {
            ClearHelpers();
        }
        SpawnRandom(_currentSpawnerSetting.SpawnNum);
    }

    public void ClearHelpers()
    {
        foreach (var helper in FindObjectsOfType<HelperController>())
        {
            GameObject.Destroy(helper.gameObject);
        }
    }
}
