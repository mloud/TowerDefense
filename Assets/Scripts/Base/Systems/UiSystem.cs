using Base.Core;
using Cysharp.Threading.Tasks;
using OneDay.TowerDefense.Base.Ui;
using UnityEngine;

namespace OneDay.TowerDefense.Base.Systems
{
    public class UiSystem : ASystem
    {
        [SerializeField] private Transform windowContainer;
        
        protected override async UniTask OnInitializeAsync()
        {
            await UniTask.DelayFrame(1);
            foreach (Transform tr in windowContainer)
            {
                await tr.gameObject.GetRequiredComponent<AWindow>().Setup();
            }    
        }


        public async UniTask OpenWindow(string windowName, Parameter parameter)
        {
            bool found = false;
            foreach (Transform tr in windowContainer)
            {
                if (tr.name == windowName)
                {
                    await tr.GetComponent<AWindow>().Open(parameter);
                    found = true;
                    break;
                }
            }

            if (!found)
            {
                Debug.LogError($"No such window {windowName} exists");
            }
        }

        public async UniTask OpenWindow<T>(Parameter parameter) where T : AWindow
        {
            bool found = false;
            foreach (Transform tr in windowContainer)
            {
                var component = tr.gameObject.GetComponent<T>();
                if (component != null)
                {
                    await component.Open(parameter);
                    found = true;
                    break;
                }
            }

            if (!found)
            {
                Debug.LogError($"No such window of type {typeof(T)} exists");
            }
        }
    }
}