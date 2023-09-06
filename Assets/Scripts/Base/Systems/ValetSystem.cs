using System;
using System.Collections.Generic;
using UnityEngine;

namespace OneDay.TowerDefense.Base.Systems
{
    public class ValetSystem : ASystem
    {
        [SerializeField] private int CoinsAsStart = 50;
        public const string Coins = "Coins";

        private Dictionary<string, int> Currencies { get; set; }
        private Dictionary<string, Action<int>> CurrenciesListeners { get; set; }

        
        public void RegisterForCurrencyChange(string currency, Action<int> changeAction, bool callImmediately = false)
        {
            if (!CurrenciesListeners.ContainsKey(currency))
            {
                CurrenciesListeners.Add(currency, changeAction);
            }
            else
            {
                CurrenciesListeners[currency] += changeAction;
            }

            if (callImmediately)
            {
                changeAction(Currencies[currency]);
            }
        }

        public void UnregisterFromCurrencyChange(string currency, Action<int> changeAction)
        {
            CurrenciesListeners[currency] -= changeAction;
        }
        
        protected override void OnInitialize()
        {
            Currencies = new Dictionary<string, int>();
            CurrenciesListeners = new Dictionary<string, Action<int>>();
            Currencies.Add(Coins,PlayerPrefs.HasKey(Coins) ? PlayerPrefs.GetInt(Coins) : CoinsAsStart);
        }

        public bool Spend(string currency, int amount)
        {
            Debug.Assert(Currencies.ContainsKey(currency), $"No such currency {currency} exists");
            if (!CanSpend(currency, amount)) return false;

            Currencies[currency] -= amount;
            if (CurrenciesListeners.TryGetValue(currency, out var listeners))
            {
                listeners.Invoke(Currencies[currency]);
            }
            Save(currency);
            return true;
        }

        public void Add(string currency, int amount)
        {
            Currencies[currency] += amount;
            if (CurrenciesListeners.TryGetValue(currency, out var listeners))
            {
                listeners.Invoke( Currencies[currency]);
            }
            Save(currency);
        }

        public bool CanSpend(string currency, int amount) => 
            Currencies[currency] >= amount;

        public int GetCurrency(string currency) =>
            Currencies[currency];
        private void Save(string currency)
        {
            //PlayerPrefs.DeleteAll();
            //PlayerPrefs.Save();
            //dont save now
            //PlayerPrefs.SetInt(currency, Currencies[currency]);
            //PlayerPrefs.Save();
        }
    }
}