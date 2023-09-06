using Base.Core;
using OneDay.TowerDefense.Base.Systems;
using OneDay.TowerDefense.Custom;
using UnityEngine;

namespace OneDay.TowerDefense.Base.Entities
{
    public class ProjectileComponent : EntityComponent, IPoolable
    {
        [SerializeField] private float moveSpeed = 10;
        private GameObject Target { get; set; }
        private float CurrentDistance { get; set; }
        private const float MaxDistance = 10;
        private const float MinDistanceToTarget = 0.1f;
        private Vector3? LastPosition { get; set; }
        private Vector3 LastTargetPosition { get; set; }

        
        private bool Fired { get; set; }
        private ProjectileLauncher Launcher { get; set; }
        private float Damage { get; set; }
        
        public void Load(ProjectileLauncher launcher, float damage)
        {
            Launcher = launcher;
            Damage = damage;
        }
        
        public void Fire(GameObject target)
        {
            Fired = true;
            LastTargetPosition = target.transform.position;
            Target = target;
            transform.SetParent(null);
        }
        private void Update()
        {
            if (!Fired) return;
            
            UpdateMove();
            UpdateRotation();
            UpdateDistance();
        }

        private void UpdateMove()
        {
          
            var targetPosition = Target != null ? Target.transform.position : LastTargetPosition;
            
            transform.position = Vector3.MoveTowards(
                transform.position,
                targetPosition, moveSpeed * Time.deltaTime);

            var sqrDistance = (transform.position - targetPosition).sqrMagnitude;
            if (sqrDistance < MinDistanceToTarget * MinDistanceToTarget)
            {
           
                if (Target != null)
                {
                    Entity.GetEntityComponent<HealthComponent>(Target)?.DealDamage(Damage);
                }
                gameObject.ReturnToPool();
            }
        }

        private void UpdateDistance()
        {
            if (Target == null)
            {
                if (LastPosition != null)
                {
                    CurrentDistance += (transform.position - LastPosition.Value).magnitude;
                    if (CurrentDistance > MaxDistance)
                    {
                        gameObject.ReturnToPool();
                    }

                    LastPosition = transform.position;
                }
            }
            else
            {
                LastTargetPosition = Target.transform.position;
            }
        }

        private void UpdateRotation()
        {
            var direction = LastTargetPosition - transform.position;
            var angle = Vector3.SignedAngle(transform.up, direction, Vector3.forward);
            transform.Rotate(0,0,angle);
        }

        public void Reset()
        {
            Fired = false;
            Launcher = null;
            LastPosition = null;
            CurrentDistance = 0;
            Target = null;
        }
    }
}