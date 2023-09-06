using OneDay.TowerDefense.Custom;
using TMPro;
using UnityEngine;

namespace OneDay.TowerDefense.Base.Ui
{
    public class HealthWidget : MonoBehaviour
    {
        [SerializeField] private TMP_Text label;
        private void OnEnable() =>
            LevelManager.GetInstance().Health.RegisterChangeListener(OnHealthChanged, true);
       
        private void OnDisable() =>
            LevelManager.GetInstance()?.Health.UnregisterChangeListener(OnHealthChanged);
        
        private void OnHealthChanged(int health) =>
            label.text = health.ToString();
    }
}