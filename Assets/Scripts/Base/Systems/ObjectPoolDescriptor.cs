using UnityEngine;

namespace OneDay.TowerDefense.Base.Systems
{
    [DisallowMultipleComponent]
    public class ObjectPoolDescriptor : MonoBehaviour
    {
        public ObjectPoolerSystem Owner;
    }
}