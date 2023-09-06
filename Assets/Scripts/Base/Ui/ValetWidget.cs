using OneDay.TowerDefense.Base.Systems;
using TMPro;
using UnityEngine;

namespace OneDay.TowerDefense.Base.Ui
{
    public class ValetWidget : MonoBehaviour
    {
        [SerializeField] private TMP_Text coinsLabel;
        private void OnEnable()
        {
            coinsLabel.text = ASystem.GetSystem<ValetSystem>().GetCurrency(ValetSystem.Coins).ToString();
            ASystem.GetSystem<ValetSystem>().RegisterForCurrencyChange(ValetSystem.Coins, OnCoinsChanged);
        }

        private void OnDisable()
        {
            ASystem.GetSystem<ValetSystem>()?.UnregisterFromCurrencyChange(ValetSystem.Coins, OnCoinsChanged);
        }
        
        private void OnCoinsChanged(int coins)
        {
            coinsLabel.text = coins.ToString();
        }
    }
}
