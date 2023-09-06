using System;
using Base.Core;
using Cysharp.Threading.Tasks;
using OneDay.TowerDefense.Base.Entities;
using OneDay.TowerDefense.Base.Systems;
using UnityEngine;

namespace OneDay.TowerDefense.Custom
{
    public class AnimationComponent : AAnimationComponent
    {
        private static readonly int Hit = Animator.StringToHash("hit");

        private void Start()
        {
            if (animator == null)
            {
            animator = GetComponentInChildren<Animator>();
            Debug.Assert(animator != null);
            }
        }

        private void OnEnable()
        {
            HealthComponent.OnHit += OnHit;
            HealthComponent.OnDeath += OnDeath;
        }

        private void OnDisable()
        {
            HealthComponent.OnHit -= OnHit;
            HealthComponent.OnDeath -= OnDeath;
        }

        private void OnHit(Entity entity, float damage)
        {
            if (Owner == entity)
            {
                PlayHitAsync(damage).Forget();
            }
        }

        private void OnDeath(Entity entity)
        {
            if (Owner == entity)
            {
                PlayDeathAsync().Forget();
            }
        }

        private async UniTask PlayHitAsync(float damage)
        {
            Owner.GetEntityComponent<WaypointMoveComponent>().SetMovingPaused(true);
            animator.SetTrigger(Hit);
            ASystem.GetSystem<EffectSystem>()
                .PlayEffect(
                    "damage", 
                    Parameter.Create("damage", damage), 
                    Owner.GetEntityComponent<PoiComponent>().GetPoi("DamageTextPoi").position)
                .Forget();
            await UniTask.Delay(TimeSpan.FromSeconds(GetCurrentAnimationLenght() + 0.3f));
            Owner.GetEntityComponent<WaypointMoveComponent>().SetMovingPaused(false);
        }
        
        private async UniTask PlayDeathAsync()
        {
            Owner.GetEntityComponent<WaypointMoveComponent>().SetMovingPaused(true);
            Owner.gameObject.SetActive(false);
            ASystem.GetSystem<EffectSystem>()
                .PlayEffect(
                    "death", 
                    null, 
                    Owner.GetEntityComponent<BodyComponent>().GetPosition())
                .Forget();

            Owner.GetEntityComponent<WaypointMoveComponent>().SetMovingPaused(false);
        }
    }
}