using System.Collections.Generic;
using OneDay.TowerDefense.Base.Systems;
using UnityEngine;

namespace OneDay.TowerDefense.Custom.Data
{
    [CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/TurretsTable", order = 1)]
    public class TurretsTable : ScriptableObject, ITable<TurretDefinition>
    {
        public string Id => tabelId;
        [SerializeField] private string tabelId;
        [SerializeField] private List<TurretDefinition> turrets;

        public TurretDefinition GetByIndex(int index) => turrets[index];
        public IEnumerable<TurretDefinition> GetRows() => turrets;
        
        public int GetRowsCount() => turrets.Count;
    }
}