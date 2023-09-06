using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;

namespace OneDay.TowerDefense.Tweens
{
    public class HitTween : ATweenPlayable, IPlayable
    {
        public override async UniTask Play()
        {
            await transform.DOLocalMove( Vector3.up * 1, 1.0f)
                .From(Vector3.zero)
                .AsyncWaitForCompletion();
        }
    }
}