using System.Collections.Generic;
using UnityEngine;

namespace CoreRuntime.Garbage
{
    [CreateAssetMenu(fileName = "GarbageSpawnerSSO", menuName = "AquaKitty/GarbageSpawnerSSO")]
    public class GarbageSpawnerSSO : ScriptableObject
    {
        public List<GameObject> GarbagePrefabs;
        public int SpawnNumber;
        public List<int> SpawnPointIndices;
    }
}