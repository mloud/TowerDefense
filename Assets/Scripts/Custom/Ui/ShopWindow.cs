using Base.Core;
using Cysharp.Threading.Tasks;
using OneDay.TowerDefense.Base.Systems;
using OneDay.TowerDefense.Base.Ui;
using OneDay.TowerDefense.Custom.Data;
using UnityEngine;
using Debug = UnityEngine.Debug;

namespace OneDay.TowerDefense.Custom.Ui
{
    public class ShopWindow : AWindow
    {
        [SerializeField] private ShopItemWidget shopItemWidget;
        [SerializeField] private Transform shopItemContainer;
     
        private TurretSlot TurretSlot { get; set; }
        protected override UniTask OnSetup()
        {
            var turretTable = ASystem.GetSystem<TowerDefenseDataSystem>()
                .GetTable<TurretDefinition>("turrets");

            for (int i = 0; i < turretTable.GetRowsCount(); i++)
            {
                Instantiate(shopItemWidget, shopItemContainer)
                    .Set(turretTable.GetByIndex(i), OnTurretClicked);
            }
            return UniTask.CompletedTask;
        }

        public void OnCloseClicked() => Close().Forget();
        protected override UniTask OnOpen(Parameter parameter)
        {
            TurretSlot = parameter.Get<TurretSlot>("slot");
            gameObject.SetActive(true);
            return UniTask.CompletedTask;
        }

        protected override UniTask OnClose()
        {
            gameObject.SetActive(false);
            return UniTask.CompletedTask;
        }

        private void OnTurretClicked(TurretDefinition turretDefinition)
        {
            Debug.Assert(ASystem.GetSystem<ValetSystem>().CanSpend(ValetSystem.Coins, turretDefinition.CostInShop));
            TurretSlot.PlaceHere(turretDefinition.Prefab, turretDefinition);
            ASystem.GetSystem<ValetSystem>().Spend(ValetSystem.Coins, turretDefinition.CostInShop);
            Close().Forget();
        }
    }
}
