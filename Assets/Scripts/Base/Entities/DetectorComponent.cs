using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace OneDay.TowerDefense.Base.Entities
{
    public class DetectorComponent : EntityComponent
    {
        [SerializeField] private List<GameObject> detectedObjects;
        [SerializeField] private string detectedTag;

        public GameObject FirstTarget() => detectedObjects.FirstOrDefault();
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag(detectedTag))
            {
                detectedObjects.Add(other.gameObject);
            }
        }
        
        private void OnTriggerExit2D(Collider2D other)
        {
            if (other.CompareTag(detectedTag))
            {
                detectedObjects.Remove(other.gameObject);
            }
        }
    }
}