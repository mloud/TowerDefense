using UnityEngine;
using UnityEngine.UI;


namespace OneDay.TowerDefense.Custom
{
    public class HealthBar : MonoBehaviour
    {
        [SerializeField] private Image image;

        private float CurrentValue { get; set; }

        public void Set(float value01)
        {
            CurrentValue = value01;
        }
      
        private void Update()
        {
            image.fillAmount = Mathf.Lerp(image.fillAmount, CurrentValue, Time.deltaTime * 10);
        }
    }
}
