using System.Collections.Generic;
using UnityEngine;

namespace OneDay.TowerDefense.Base.Systems.Spawner
{
    [CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/WavesDefinition", order = 1)]
    public class WavesDefinition : ScriptableObject
    {
        public List<WaveDefinition> Waves;
    }
}