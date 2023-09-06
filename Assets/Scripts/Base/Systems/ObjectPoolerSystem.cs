using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Unity.VisualScripting;
using UnityEngine;

namespace OneDay.TowerDefense.Base.Systems
{
    public class ObjectPoolerSystem : ASystem
    {
        [SerializeField] private GameObject prefab;
        [SerializeField] private int poolSize;
        [SerializeField] private Transform poolContainer;
        [SerializeField] private bool poolOnStart;
    
        private bool DestroyContainerAfterRelease { get; set; }
        private List<GameObject> Pool { get; set; }

        protected override void OnInitialize()
        {
            Pool = new List<GameObject>();

            if (poolContainer == null)
            {
                poolContainer = new GameObject($"PoolContainer_{prefab.name}").transform;
                DestroyContainerAfterRelease = true;
            }
            if (poolOnStart)
            {
                FillPool();           
            }
        }
        
        protected override void OnRelease()
        {
            for (int i = 0; i < Pool.Count; i++)
            {
                GameObject.Destroy(Pool[i].gameObject);
            }

            if (poolContainer != null && DestroyContainerAfterRelease)
            {
                Destroy(poolContainer.gameObject);
            }
        }

        public void FillPool()
        {
            int instancesNeeded = poolSize - Pool.Count;
            for (int i = 0; i < instancesNeeded; i++)
            {
                CreateInstance();
            }
        }

        public GameObject GetInstanceFromPool()
        {
            GameObject instance = null;
            foreach (var go in Pool)
            {
                if (go.transform.parent == poolContainer)
                {
                    instance = go;
                    break;
                }
            }

            instance ??= CreateInstance();
            instance.transform.SetParent(null);
            return instance;
        }

        public void ReturnInstanceToPool(GameObject instance)
        {
            Debug.Assert(Pool.Contains(instance), "This instance does not come from this pool");
            instance.transform.SetParent(poolContainer);
            instance.SetActive(false);
            instance.GetComponent<IPoolable>()?.Reset();
        }
        
        public async UniTask ReturnInstanceToPoolWthDelay(GameObject instance, float delay)
        {
            Debug.Assert(Pool.Contains(instance), "This instance does not come from this pool");
            await UniTask.Delay(TimeSpan.FromSeconds(delay));
            instance.transform.SetParent(poolContainer);
            instance.SetActive(false);
        }
     
        private GameObject CreateInstance()
        {
            var instance = Instantiate(prefab, poolContainer);
            instance.AddComponent<ObjectPoolDescriptor>().Owner = this;
            instance.SetActive(false);
            Pool.Add(instance);
            return instance;
        }
    }
}
