using OneDay.TowerDefense.Base.Systems;
using OneDay.TowerDefense.Base.Systems.Spawner;
using TMPro;
using UnityEngine;

namespace OneDay.TowerDefense.Base.Ui
{
    public class WaveWidget : MonoBehaviour
    {
        [SerializeField] private TMP_Text waveLabel;

        private void OnEnable()
        {
            ASystem.GetSystem<SpawnerSystem>("enemy").WaveStarted.RegisterListener(OnWaveStarted);
        }

        private void OnDisable()
        {
            ASystem.GetSystem<SpawnerSystem>("enemy")?.WaveStarted.UnregisterListener(OnWaveStarted);
        }
        private void OnWaveStarted(int waveIndex, WaveDefinition waveDefinition, int totalWavesCount)
        {
            waveLabel.text = $"Wave {waveIndex + 1}/{totalWavesCount}";
        }
    }
}