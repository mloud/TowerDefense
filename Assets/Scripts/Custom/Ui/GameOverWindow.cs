using Base.Core;
using Cysharp.Threading.Tasks;
using OneDay.TowerDefense.Base.Ui;
using UnityEngine;
using UnityEngine.SceneManagement;


namespace OneDay.TowerDefense.Custom.Ui
{
    public class GameOverWindow : AWindow
    {
        protected override UniTask OnSetup()
        {
            LevelManager.GetInstance().GameOverAction += ()=>OnOpen(null);
            return UniTask.CompletedTask;
        }

        protected override UniTask OnOpen(Parameter parameter)
        {
            gameObject.SetActive(true);
            return UniTask.CompletedTask;
        }

        protected override UniTask OnClose()
        {
            gameObject.SetActive(false);
            return UniTask.CompletedTask;
        }

        public void OnRepeatClicked()
        {
            SceneManager.LoadScene(0,LoadSceneMode.Single);
        }
    }
}