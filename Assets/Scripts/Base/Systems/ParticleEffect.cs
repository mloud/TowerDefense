using Base.Core;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace OneDay.TowerDefense.Base.Systems
{
    public class ParticleEffect : AEffect
    {
        [SerializeField] private ParticleSystem ps;
        
        private bool IsPlaying { get; set; }
        public override async UniTask Play(Parameter parameter)
        {
            IsPlaying = true;
            var main = ps.main;
            main.stopAction = ParticleSystemStopAction.Callback;
            ps.Play();

            await UniTask.WaitUntil(() => !IsPlaying);
        }

        private void OnParticleSystemStopped()
        {
            IsPlaying = false;
        }
    }
}