using Base.Core;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace OneDay.TowerDefense.Base.Ui
{
    public abstract class AWindow : MonoBehaviour
    {
        public async UniTask Open(Parameter parameter)
        {
            await OnOpen(parameter);
        }

        public async UniTask Close()
        {
            await OnClose();
        }
        
        public async UniTask Setup()
        {
            await OnSetup();
        }

        protected abstract UniTask OnOpen(Parameter parameter);
        protected abstract UniTask OnClose();
        protected abstract UniTask OnSetup();
    }
}