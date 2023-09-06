using System;
using Base.Core;
using OneDay.TowerDefense.Base.Entities;
using OneDay.TowerDefense.Base.Systems;
using OneDay.TowerDefense.Base.Systems.Spawner;
using UnityEngine;

namespace OneDay.TowerDefense.Custom
{
    [DefaultExecutionOrder(-50)]
    public class LevelManager : MonoSingleton<LevelManager>
    {
        public Action GameOverAction { get; set; }
        public SmartProperty<int> Health { get; } = new();
           
        private int EnemyCounter { get; set; }
        private void Start()
        {
            Health.Set(5);
            ASystem.GetSystem<SpawnerSystem>("enemy").GameObjectSpawned.RegisterListener(OnEnemySpawned);
            ASystem.GetSystem<SpawnerSystem>("enemy").WaveStarted.RegisterListener(OnWaveStarted);
            StartCoroutine(ASystem.GetSystem<SpawnerSystem>("enemy").Run());

            WaypointMoveComponent.LastWaypointReached += OnEnemyReachedEnd;
            HealthComponent.OnDeath += OnEnemyDeath;
        }
        protected override void OnDestroy()
        {
            WaypointMoveComponent.LastWaypointReached -= OnEnemyReachedEnd;
            ASystem.GetSystem<SpawnerSystem>("enemy")?.GameObjectSpawned.UnregisterListener(OnEnemySpawned);
            HealthComponent.OnDeath -= OnEnemyDeath;
            base.OnDestroy();
        }

        private void OnEnemyReachedEnd(Entity entity)
        {
            Health.Set(Health.Get() - 1);
            entity.gameObject.ReturnToPool();
            
            if (Health.Get() <= 0)
            {
                GameOverAction?.Invoke();
                Entity.List.ForEach(x=>x.GetEntityComponent<AMoveComponent>().SetMovingPaused(true));
                ASystem.GetSystem<SpawnerSystem>().Stop();   
            }
        }

        private void OnEnemyDeath(Entity entity)
        {
            EnemyCounter--;
            entity.gameObject.ReturnToPoolWithDelay(1.0f);
            ASystem.GetSystem<ValetSystem>().Add(ValetSystem.Coins, 10);
            if (EnemyCounter == 0)
            {
                ASystem.GetSystem<SpawnerSystem>("enemy").TriggerNextWave();
            }
        }
        
        private void OnEnemySpawned(GameObject enemy)
        {
            
        }
        
        private void OnWaveStarted(int waveIndex, WaveDefinition waveDefinition, int totalWaves)
        {
            EnemyCounter = waveDefinition.TotalSpawnObjects;
        }
    }
}