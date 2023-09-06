using Cysharp.Threading.Tasks;
using OneDay.TowerDefense.Base.Systems;
using UnityEngine;

namespace Base.Core
{
    public static class GameObjectExtensions
    {
        public static Transform FindDeep(this Transform tr, string name)
        {
            for (int i = 0; i < tr.childCount; i++)
            {
                if (tr.GetChild(i).name == name)
                {
                    return tr.GetChild(i);
                }

                var resultInChild = FindDeep(tr.GetChild(i), name);
                if (resultInChild != null)
                {
                    return resultInChild;
                }
            }

            return null;
        }

        public static T GetRequiredComponent<T>(this GameObject go) where T:Component
        {
            var component = go.GetComponent<T>();
            if (component == null)
            {
                throw new MissingComponentException<T>(go);
            }
            return component;
        }
        
        public static void ReturnToPool(this GameObject go) =>
            go.GetComponent<ObjectPoolDescriptor>().Owner.ReturnInstanceToPool(go);

        public static void ReturnToPoolWithDelay(this GameObject go, float delaySec) =>
            go.GetComponent<ObjectPoolDescriptor>().Owner.ReturnInstanceToPoolWthDelay(go, delaySec).Forget();
    }
}