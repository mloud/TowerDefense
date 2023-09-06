using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace OneDay.TowerDefense.Base.Systems
{
    [DefaultExecutionOrder(-100)]
    public abstract class ASystem : MonoBehaviour
    {
        public string Id => id;
        [SerializeField] private int order;
        [SerializeField] private string id;
        [SerializeField] private bool isLocal;
        public static T GetSystem<T>(string id = null) where T:ASystem =>
            (T)Systems.Find(x => x.GetType() == typeof(T) && 
                                 (string.IsNullOrEmpty(id) || x.Id == id) && !x.IsMarkedForRemove);
        
        public static T GetRequiredSystem<T>(string id = null) where T:ASystem
        {
            var system = GetSystem<T>(id);
            if (system == null)
            {
                throw new KeyNotFoundException($"System of type:{typeof(T)} and id:{id} does not exist");
            }

            return system;
        }
        private static List<ASystem> Systems { get; } = new();
        private static List<ASystem> SystemsToRemove { get; } = new();
    
        private bool IsMarkedForRemove { get; set; }
        
        private void Awake()
        {
            if (!isLocal)
            {
                Systems.Add(this);
                Debug.Log($"Registering system od type: {GetType()} and id:{Id}");
            }
            OnInitialize();
            OnInitializeAsync();
        }

        private void Update()
        {
            if (SystemsToRemove.Count > 0)
            {

                SystemsToRemove.ForEach(x => Systems.Remove(x));
                SystemsToRemove.Clear();
            }
        }

        private void OnDestroy()
        {
            if (!isLocal)
            {
                SystemsToRemove.Add(this);
                Debug.Log($"UnRegistering system od type: {GetType()} and id:{Id}");
            }

            IsMarkedForRemove = true;
            OnRelease();
        }

        protected virtual UniTask OnInitializeAsync() =>
            UniTask.CompletedTask;

        protected virtual void OnInitialize()
        {}
        protected virtual void OnRelease()
        {}
    }
}