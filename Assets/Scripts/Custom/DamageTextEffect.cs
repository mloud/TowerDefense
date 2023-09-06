using System;
using Base.Core;
using Cysharp.Threading.Tasks;
using OneDay.TowerDefense.Base.Systems;
using TMPro;
using UnityEngine;

namespace OneDay.TowerDefense.Custom
{
    public class DamageTextEffect : AEffect
    {
        [SerializeField] private TMP_Text label;
        public override async UniTask Play(Parameter parameter)
        {
            float damage = parameter.Get<float>("damage");
            label.text = damage.ToString();
            await UniTask.Delay(TimeSpan.FromSeconds(1));
        }
    }
}