using UnityEngine;

namespace Base.Core
{

    public class MonoSingleton<T> : MonoBehaviour where T: MonoBehaviour
    {
        public static T GetInstance() => Instance;
        private static T Instance { get; set; }

        protected virtual void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(Instance);
            }

            Instance = this as T;
        }
        
        protected virtual void OnDestroy()
        {
            Instance = null;
        }
    }
}