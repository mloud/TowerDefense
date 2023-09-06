using UnityEngine;

namespace OneDay.TowerDefense.Base.Entities
{
    public class AAnimationComponent : EntityComponent
    {
        [SerializeField] protected Animator animator;

        protected float GetCurrentAnimationLenght() =>
            animator.GetCurrentAnimatorStateInfo(0).length;
    }
}