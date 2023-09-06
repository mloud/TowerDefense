using System;
using OneDay.TowerDefense.Base.Entities;
using UnityEngine;

namespace OneDay.TowerDefense.Custom
{
    public class HealthComponent : EntityComponent
    {
        public static Action<Entity, float> OnHit;
        public static Action<Entity> OnDeath;
        
        [SerializeField] private HealthBar healthBar;
        [SerializeField] private float maxHealth;
    
        public float CurrentHealth { get; private set; }

        private void OnEnable()
        {
            CurrentHealth = maxHealth;
            healthBar.gameObject.SetActive(true);
            healthBar.Set(CurrentHealth/maxHealth);
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.H))
            {
                DealDamage(5);
            }
        }
        
        public void DealDamage(float damage)
        {
            CurrentHealth -= damage;
            healthBar.Set(CurrentHealth/maxHealth);
            if (CurrentHealth <= 0)
            {
                CurrentHealth = 0;
                Die();
            }
            else
            {
                OnHit?.Invoke(Owner, damage);
            }
        }

        private void Die()
        {
            healthBar.gameObject.SetActive(false);
            OnDeath?.Invoke(Owner);
        }
    }
}
