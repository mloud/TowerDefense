using System.Linq;
using Base.Core;
using Cysharp.Threading.Tasks;
using OneDay.TowerDefense.Base.Entities;
using OneDay.TowerDefense.Base.Systems;
using OneDay.TowerDefense.Custom.Data;
using OneDay.TowerDefense.Custom.Ui;
using UnityEngine;
using UnityEngine.UI;


namespace OneDay.TowerDefense.Custom
{
    public class TurretSlot : MonoBehaviour
    {
        [SerializeField] private GameObject turret;
        [SerializeField] private GameObject icon;
        [SerializeField] private GameObject plusIcon;
        [SerializeField] private Button button;
        
        private void OnEnable()
        {
            ASystem.GetSystem<ValetSystem>()
                .RegisterForCurrencyChange(ValetSystem.Coins, OnCurrencyChanged, true);
        }

        private void OnDisable()
        {
            ASystem.GetSystem<ValetSystem>()?
                .UnregisterFromCurrencyChange(ValetSystem.Coins, OnCurrencyChanged);
        }

        public void OnClick()
        {
            ASystem.GetSystem<UiSystem>().OpenWindow<ShopWindow>(Parameter.Create("slot", this))
                .Forget();
        }

        public void PlaceHere(GameObject prefab, TurretDefinition turretDefinition)
        {
            turret = Instantiate(prefab, transform.position, Quaternion.identity);
            Entity.GetEntityComponent<TurretDefinitionComponent>(turret).TurretDefinition = turretDefinition;
            icon.SetActive(false);
            button.interactable = false;
        }

        private void OnCurrencyChanged(int coins)
        {
            bool canBuyTurret = false;
            if (turret == null)
            {
                var valetSystem = ASystem.GetSystem<ValetSystem>();
                canBuyTurret = ASystem.GetSystem<TowerDefenseDataSystem>()
                    .GetTable<TurretDefinition>("turrets")
                    .GetRows()
                    .Any(x => valetSystem.CanSpend(ValetSystem.Coins, x.CostInShop));
            }
            plusIcon.SetActive(canBuyTurret);
        }
        
        public bool IsFull() => turret != null;

        public void Remove()
        {
            Debug.Assert(IsFull());
            turret = null;
            button.interactable = true;
        }
    }
}