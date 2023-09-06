using System;
using OneDay.TowerDefense.Base.Systems;
using OneDay.TowerDefense.Custom.Data;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace OneDay.TowerDefense.Custom.Ui
{
    public class ShopItemWidget : MonoBehaviour
    {
        [SerializeField] private Image icon;
        [SerializeField] private TMP_Text costText;
        [SerializeField] private Button button;
     
        private TurretDefinition TurretDefinition { get; set; }
        public void Set(TurretDefinition turretDefinition, Action<TurretDefinition> clickedAction)
        {
            TurretDefinition = turretDefinition;
            button.onClick.RemoveAllListeners();
            button.onClick.AddListener(()=> clickedAction(turretDefinition));
            costText.text = turretDefinition.CostInShop.ToString();
            icon.sprite = turretDefinition.Thumbnail;
            ASystem.GetSystem<ValetSystem>().RegisterForCurrencyChange(ValetSystem.Coins, Refresh, true);
        }

        private void Refresh(int coins)
        {
            button.interactable = ASystem.GetSystem<ValetSystem>().CanSpend(ValetSystem.Coins, TurretDefinition.CostInShop);
        }

        private void OnDestroy()
        {
            ASystem.GetSystem<ValetSystem>()?.UnregisterFromCurrencyChange(ValetSystem.Coins, Refresh);
        }
    }
}