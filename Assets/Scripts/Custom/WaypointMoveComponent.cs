using System;
using OneDay.TowerDefense.Base.Entities;
using OneDay.TowerDefense.Base.Systems;
using UnityEngine;

namespace OneDay.TowerDefense.Custom
{
    public class WaypointMoveComponent : AMoveComponent
    {
        [Header("Required BodyComponent  reference")]
        [SerializeField] private BodyComponent bodyComponent;
        
        public static Action<Entity> LastWaypointReached;

        [SerializeField] private float moveSpeed;
        private bool IsMovingPaused { get; set; }
        private int CurrentWaypoint { get; set; }
        private const float MinDistanceTreshold = 0.05f;

        private void Start()
        {
            CurrentWaypoint = 0;
            if (CurrentWaypoint < WaypointSystem.First.WaypointsCount)
            {
                transform.position = WaypointSystem.First.GetWaypointPosition(CurrentWaypoint);
            }
        }

        private void Update()
        {
            if (CurrentWaypoint < 0)
                return;
            
            UpdateMove();
            UpdateRotation();
        }

        private void UpdateRotation()
        {
            if (IsMovingPaused)
                return;
            
            if (CurrentWaypoint >= WaypointSystem.First.WaypointsCount)
                return;
            var waypointPosition = WaypointSystem.First.GetWaypointPosition(CurrentWaypoint);
            bodyComponent.SetHorizontalOrientation(waypointPosition.x > transform.position.x);
        }

        private void UpdateMove()
        {
            if (IsMovingPaused)
                return;
            
            if (CurrentWaypoint >= WaypointSystem.First.WaypointsCount)
                return;

            var waypointPosition = WaypointSystem.First.GetWaypointPosition(CurrentWaypoint);

            transform.position = Vector3.MoveTowards(
                transform.position, 
                waypointPosition,
                Time.deltaTime * moveSpeed);
            
          
            if ((transform.position - waypointPosition).sqrMagnitude < MinDistanceTreshold * MinDistanceTreshold)
            {
                CurrentWaypoint++;
                // we are at the end
                if (CurrentWaypoint >= WaypointSystem.First.WaypointsCount)
                {
                    LastWaypointReached?.Invoke(Owner);
                }
            }
        }

        public override void SetMovingPaused(bool paused)
        {
            IsMovingPaused = paused;
        }
    }
}