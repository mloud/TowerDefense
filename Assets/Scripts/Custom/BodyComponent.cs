using OneDay.TowerDefense.Base.Entities;
using UnityEngine;

namespace OneDay.TowerDefense.Custom
{
    public class BodyComponent : EntityComponent
    {
        [SerializeField] private Transform bodyTransform;

        private Vector3 DefaultBodyScale { get; set; }

        private void Awake()
        {
            DefaultBodyScale = bodyTransform.localScale;
        }
        
        public void SetHorizontalOrientation(bool leftToRight)
        {
            var scale = bodyTransform.localScale;
            scale.x = (leftToRight ? 1 : -1) * DefaultBodyScale.x;
            bodyTransform.localScale = scale;
        }

        public Vector3 GetPosition() => transform.position;
    }
}