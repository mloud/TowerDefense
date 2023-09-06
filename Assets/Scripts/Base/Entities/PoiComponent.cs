using System.Collections.Generic;
using Base.Core;
using UnityEngine;

namespace OneDay.TowerDefense.Base.Entities
{
    public class PoiComponent : EntityComponent
    {
        private Dictionary<string, Transform> Cache { get; set; } = new();

        public Transform GetPoi(string poiName)
        {
            if (Cache.TryGetValue(poiName, out var tr))
            {
                return tr;
            }
  
            tr = gameObject.transform.FindDeep(poiName);
            if (tr != null)
            {
                Cache.Add(poiName, tr);
            }

            return tr;
        }
    }
}