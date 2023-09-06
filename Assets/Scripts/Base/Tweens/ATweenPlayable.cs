using Cysharp.Threading.Tasks;
using UnityEngine;

namespace OneDay.TowerDefense.Tweens
{
    public abstract class ATweenPlayable : MonoBehaviour, IPlayable
    {
        [SerializeField] private bool playOnEnable;

        private void OnEnable()
        {
            if (playOnEnable)
            {
                Play();
            }
        }

        public abstract UniTask Play();
    }
}