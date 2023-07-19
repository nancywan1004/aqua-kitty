using System;
using System.Collections.Generic;
using UnityEngine;

namespace CoreSystem.Spawner
{
    [Serializable]
    public class SpawnerScriptableObject : ScriptableObject
    {
        public List<int> SpawnIndices;
        public List<GameObject> Prefabs;
        public int SpawnNum;
    }
}