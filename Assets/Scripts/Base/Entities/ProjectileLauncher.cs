using System;
using System.Collections.Generic;
using OneDay.TowerDefense.Base.Systems;
using OneDay.TowerDefense.Custom;
using UnityEngine;

namespace OneDay.TowerDefense.Base.Entities
{
    public class ProjectileLauncher : EntityComponent
    {
        [SerializeField] private ObjectPoolerSystem pool;
        [SerializeField] private List<Transform> spawnPoints;
        [SerializeField] private AudioSource audioSource;
        [SerializeField] private bool useLoading;
        private List<ProjectileComponent> loadedProjectiles;

        private float AttackTimer { get; set; }
        private float LoadTimer { get; set; }
        private float Damage { get; set; }
        private float AttackInterval { get; set; }
        private float LoadInterval { get; set; }
        private void Start()
        {
            loadedProjectiles = new List<ProjectileComponent>();
            for (int i = 0; i < spawnPoints.Count; i++)
            {
                loadedProjectiles.Add(null);
            }
        }

        public void SetDamage(float damage) => Damage = damage;

        public void SetAttackInterval(float attackInterval)
        {
            AttackInterval = attackInterval;
            LoadInterval = useLoading ? Mathf.Min(LoadInterval, AttackInterval) : AttackInterval;
        }
        
        private void LoadProjectiles()
        {
            if (Time.time > LoadTimer)
            {
                for (int i = 0; i < loadedProjectiles.Count; i++)
                {
                    if (loadedProjectiles[i] == null)
                    {
                        loadedProjectiles[i] = pool.GetInstanceFromPool().GetComponent<ProjectileComponent>();
                        loadedProjectiles[i].gameObject.SetActive(true);
                        loadedProjectiles[i].transform.SetParent(spawnPoints[i]);
                        loadedProjectiles[i].Load(this, Damage);
                        loadedProjectiles[i].transform.position = spawnPoints[i].transform.position;
                        loadedProjectiles[i].transform.localRotation = Quaternion.identity;
                    }
                }
            }
        }

        private void Update()
        {
            LoadProjectiles();
            UpdateAttacks();
        }

        private void UpdateAttacks()
        {
            if (Time.time > AttackTimer)
            {
                AttackTimer = Time.time + AttackInterval;
                LoadTimer = Time.time + LoadInterval;
                var target = Owner.GetComponent<DetectorComponent>().FirstTarget();
                if (target == null || Entity.GetEntityComponent<HealthComponent>(target).CurrentHealth <= 0) return;
               

                bool wasFired = false;
                for (int i = 0; i < loadedProjectiles.Count; i++)
                {
                    if (loadedProjectiles[i] != null)
                    {
                        loadedProjectiles[i].Fire(target);
                        loadedProjectiles[i] = null;
                    }
                    wasFired = true;
                }

                if (wasFired)
                {
                    //if (audioSource != null)
                    //    audioSource.Play();
                }
            }
        }
    }
}