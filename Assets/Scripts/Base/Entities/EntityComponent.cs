using UnityEngine;

namespace OneDay.TowerDefense.Base.Entities
{
    public abstract class EntityComponent : MonoBehaviour
    {
        protected Entity Owner {get; private set; }

        private void Awake()
        {
            Owner = GetComponent<Entity>();
            Debug.Assert(Owner != null, "No Entity component found");
        }
    }
}
