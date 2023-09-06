using OneDay.TowerDefense.Base.Systems;
using UnityEngine;

namespace OneDay.TowerDefense.Custom.Data
{
    [CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/TurretDefinition", order = 1)]
    public class TurretDefinition : ScriptableObject, ITableItem
    {
        [Space]
        [Header("Shop")]
        public int CostInShop;
        public Sprite Thumbnail;
        public GameObject Prefab;
        
        [Space]
        [Header("Stats")]
        public float Damage;
        public float AttackInterval;
        public float LoadTimeInterval;
        
        [Space]
        [Header("Upgrade")]       
        public float DamageIncrement;
        public float AttackDelayReduction;
        public int InitialUpgradeCost;
        public int UpradeCostIcrement;

        public float GetDamage(int level) =>
            Damage + (level - 1) * DamageIncrement;

        public float GetAttackTime(int level) =>
            Mathf.Max(0, AttackInterval - (level - 1) * AttackDelayReduction);

        public int GetUpgradeCost(int fromLevel) =>
            fromLevel == 1 ? InitialUpgradeCost : UpradeCostIcrement;
    }
}