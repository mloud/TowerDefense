using System.Collections.Generic;
using System.Linq;
using UnityEngine;


namespace OneDay.TowerDefense.Base.Systems
{
    public class WaypointSystem : ASystem
    {
        public static WaypointSystem First => Instances.FirstOrDefault();
        private static List<WaypointSystem> Instances { get; } = new();
        public int WaypointsCount => points.Count;
        
       
        [SerializeField] private List<Vector3> points;
        
        protected override void OnInitialize()
        {
            Instances.Add(this);
        }
        
        protected override void OnRelease()
        {
            Instances.Remove(this);
        }

        public void AddWaypoint(Vector3 position,  bool isWorld)
        {
            points.Add(isWorld ? position - transform.position : position);
        }
        
        public Vector3 GetWaypointPosition(int index)
        {
            Debug.Assert(index < points.Count, $"Index {index} is out of range");
            return points[index] + transform.position;
        }

        public void SetWaypointPosition(int index, Vector3 position, bool isWorld)
        {
            Debug.Assert(index < points.Count, $"Index {index} is out of range");
            points[index] = isWorld ? position - transform.position : position;
        }

        private void OnDrawGizmos()
        {
            // spheres around points
            for (int i = 0; i < WaypointsCount; i++)
            {
                Gizmos.DrawWireSphere(GetWaypointPosition(i), 0.5f);
            }
            //lines
            for (int i = 0; i < WaypointsCount - 1; i++)
            {
                Gizmos.DrawLine(GetWaypointPosition(i), GetWaypointPosition(i + 1));
            }
        }
    }
}
