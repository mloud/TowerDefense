using System.Collections.Generic;
using UnityEngine;

namespace OneDay.TowerDefense.Base.Entities
{
    public class Entity : MonoBehaviour
    {
        public static List<Entity> List { get; private set; } = new();
        private void OnEnable() =>
            List.Add(this);    
       
        private void OnDisable() =>
            List.Remove(this);    
      
        public T GetEntityComponent<T>() where T : EntityComponent =>
            GetComponent<T>();

        public static Entity GetEntity(GameObject gameObject) =>
            gameObject.GetComponent<Entity>();

        public static T GetEntityComponent<T>(GameObject gameObject) where T : EntityComponent =>
            GetEntity(gameObject)?.GetEntityComponent<T>();
    }   
}
