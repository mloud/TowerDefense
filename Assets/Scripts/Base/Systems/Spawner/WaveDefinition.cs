using System.Collections.Generic;
using UnityEngine;

namespace OneDay.TowerDefense.Base.Systems.Spawner
{
    [CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/WaveDefinition", order = 1)]
    public class WaveDefinition : ScriptableObject
    {
        public float DelayBefore;
        public int TotalSpawnObjects;
        public float DelayBetweenSpawn;
        public List<ObjectWithWeight> SpawnedObjects;
    }
    
    [System.Serializable]
    public class ObjectWithWeight
    {
        public string ObjectName;
        public float Weight;
    }
}