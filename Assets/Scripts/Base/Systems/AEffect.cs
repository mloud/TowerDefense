using Base.Core;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace OneDay.TowerDefense.Base.Systems
{
    public abstract class AEffect : MonoBehaviour
    {
        public abstract UniTask Play(Parameter parameter);
    }
}