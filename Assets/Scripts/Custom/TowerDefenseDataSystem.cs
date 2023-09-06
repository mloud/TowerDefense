using OneDay.TowerDefense.Base.Systems;
using OneDay.TowerDefense.Custom.Data;
using UnityEngine;

namespace OneDay.TowerDefense.Custom
{
    public class TowerDefenseDataSystem : DataSystem
    {
        [SerializeField] private TurretsTable turretsTable;
        protected override void OnInitialize()
        {
              AddTable(turretsTable);            
        }
    }
}