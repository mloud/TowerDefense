using UnityEngine;

namespace OneDay.TowerDefense.Base.Entities
{
    public class LookAtComponent : EntityComponent
    {
        [SerializeField] private Transform targetTransform;
        private void Update()
        {
            var detectedTarget = Owner.GetEntityComponent<DetectorComponent>().FirstTarget();
            if (detectedTarget != null)
            {
                var targetDirection = detectedTarget.transform.position - transform.position;
                var currentDirection = targetTransform.up;
                var angle = Vector3.SignedAngle(currentDirection, targetDirection, Vector3.forward);
                targetTransform.Rotate(0,0,angle);
            }
        }
    }
}