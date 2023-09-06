using System;
using System.Collections;
using System.Linq;
using Base.Core;
using UnityEngine;


namespace OneDay.TowerDefense.Base.Systems.Spawner
{
    public class SpawnerSystem : ASystem
    {
        public SystemAction<GameObject> GameObjectSpawned { get; } = new();
        public SystemAction<int, WaveDefinition> WaveSpawnedEverything { get; } = new();
        public SystemAction<int, WaveDefinition, int> WaveStarted { get; } = new();

        [Header("Settings")] 
        [SerializeField] private WavesDefinition waves;
        [SerializeField] private bool startSpawningAutomatically;
        [SerializeField] private bool spawnNextWaveAutomatically;
        
        [Header("Spawned objects container")] 
        [SerializeField] private Transform spawnObjectContainer;

        private int WaveIndex { get; set; }
        private int SpawnObjectsCount { get; set; }
        private bool WaitingForTheNextWave { get; set; }
        private Coroutine SpawnCoroutine { get; set; }
        
        private void Start()
        {
            if (startSpawningAutomatically)
            {
                StartCoroutine(Run());
            }
        }

        public void TriggerNextWave() => WaitingForTheNextWave = false;

        public IEnumerator Run()
        {
            SpawnCoroutine= StartCoroutine(RunInternal());
            yield return SpawnCoroutine;
        }
        
        private IEnumerator RunInternal()
        {
            WaveIndex = 0;
            
            while (WaveIndex < waves.Waves.Count)
            {
                SpawnObjectsCount = 0;
                yield return new WaitForSeconds(GetWave(WaveIndex).DelayBefore);
                WaveStarted.TriggerUpdate(WaveIndex, GetWave(WaveIndex), waves.Waves.Count);
                while (SpawnObjectsCount < GetWave(WaveIndex).TotalSpawnObjects)
                {
                    Spawn();
                    yield return new WaitForSeconds(GetWave(WaveIndex).DelayBetweenSpawn);
                }
                WaveSpawnedEverything.TriggerUpdate(WaveIndex, GetWave(WaveIndex));
                
                WaitingForTheNextWave = !spawnNextWaveAutomatically;

                yield return new WaitUntil(() => !WaitingForTheNextWave);
              
                WaveIndex++;
            }
        }

        public void Stop()
        {
            if (SpawnCoroutine != null)
            {
                StopCoroutine(SpawnCoroutine);
                SpawnCoroutine = null;
            }
        }

        private WaveDefinition GetWave(int index)
        {
            Debug.Assert(index < waves.Waves.Count);
            return waves.Waves[index];
        }
      
        private void Spawn()
        {
            var rndIndex = RandomUtils.GetRandomIndexUsingWeight(
                GetWave(WaveIndex).SpawnedObjects.Select(x => x.Weight).ToList());
            var rndPool = GetWave(WaveIndex).SpawnedObjects[rndIndex];
            var instance = GetRequiredSystem<ObjectPoolerSystem>(rndPool.ObjectName).GetInstanceFromPool();
            instance.transform.SetParent(spawnObjectContainer);
            instance.SetActive(true);
            GameObjectSpawned.TriggerUpdate(instance);
            SpawnObjectsCount++;
        }
    }
}
