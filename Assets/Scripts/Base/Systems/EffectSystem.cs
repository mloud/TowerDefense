using System.Collections.Generic;
using Base.Core;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace OneDay.TowerDefense.Base.Systems
{
    public class EffectSystem : ASystem
    {
        [System.Serializable]
        public class EffectToPoolerLink
        {
            public string EffectId;
            public string ObjectPoolerId;
            public ObjectPoolerSystem ObjectPoolerSystem;
        }

        [SerializeField] private List<EffectToPoolerLink> objectPoolers;

        public async UniTask PlayEffect(string id, Parameter parameter, Vector3 position)
        {
            var effectToPoolerLink = objectPoolers.Find(x => x.EffectId == id);
            Debug.Assert(effectToPoolerLink != null, $"no object pooler found for effect with id {id}");

            var objectPool = effectToPoolerLink.ObjectPoolerSystem ? effectToPoolerLink.ObjectPoolerSystem : GetSystem<ObjectPoolerSystem>(effectToPoolerLink.ObjectPoolerId);
            var effectInstance = objectPool.GetInstanceFromPool();
            effectInstance.transform.position = position;
            effectInstance.SetActive(true);
            await effectInstance.GetComponent<AEffect>().Play(parameter);
            objectPool.ReturnInstanceToPool(effectInstance.gameObject);
        }
    }
}