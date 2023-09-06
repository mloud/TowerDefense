using OneDay.TowerDefense.Base.Entities;
using OneDay.TowerDefense.Base.Systems;
using OneDay.TowerDefense.Custom.Data;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace OneDay.TowerDefense.Custom
{
    public class TurretUpgradeComponent : EntityComponent
    {
        [SerializeField] private ProjectileLauncher projectileLauncher;
        [SerializeField] private Button upgradeButton;
        [SerializeField] private TMP_Text upgradeCostLabel;
        [SerializeField] private TMP_Text levelLabel;
    
        public int Level { get; private set; }
        
        private void OnEnable()
        {
            Level = 1;
            ASystem.GetSystem<ValetSystem>().RegisterForCurrencyChange(ValetSystem.Coins, OnCoinsChanged);
            upgradeButton.onClick.AddListener(OnUpgrade);
        }

        private void OnDisable()
        {
            ASystem.GetSystem<ValetSystem>()?.UnregisterFromCurrencyChange(ValetSystem.Coins, OnCoinsChanged);
            upgradeButton.onClick.RemoveListener(OnUpgrade);
        }

        private void Start()
        {
            projectileLauncher.SetDamage(Owner.GetEntityComponent<TurretDefinitionComponent>()
                .TurretDefinition.GetDamage(Level));
            projectileLauncher.SetAttackInterval(Owner.GetEntityComponent<TurretDefinitionComponent>()
                .TurretDefinition.GetAttackTime(Level));

            RefreshButton();
        }
        private void OnUpgrade()
        {
            Upgrade();
        }

        private void OnCoinsChanged(int coins)
        {
            RefreshButton();
        }
        public void Upgrade()
        {
            var turretDefinition = Owner.GetEntityComponent<TurretDefinitionComponent>().TurretDefinition;
            int upgradeCost = turretDefinition.GetUpgradeCost(Level);
            
            if (ASystem.GetSystem<ValetSystem>().CanSpend(ValetSystem.Coins, upgradeCost))
            {
                Level++;
                projectileLauncher.SetDamage(turretDefinition.GetDamage(Level));
                projectileLauncher.SetAttackInterval(turretDefinition.GetAttackTime(Level));
               
                ASystem.GetSystem<ValetSystem>().Spend(ValetSystem.Coins, upgradeCost);
                RefreshButton();
            }
        }

        private void RefreshButton()
        {
            int upgradeCost = Owner.GetEntityComponent<TurretDefinitionComponent>()
                .TurretDefinition.GetUpgradeCost(Level);
            upgradeCostLabel.text = upgradeCost.ToString();
            levelLabel.text = Level.ToString();
            upgradeButton.gameObject.SetActive(ASystem.GetSystem<ValetSystem>().CanSpend(ValetSystem.Coins, upgradeCost));
        }
    }
}
