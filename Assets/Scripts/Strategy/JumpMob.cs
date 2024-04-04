using System;
using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using Mob;
using UniRx;
using UnityEngine;
namespace Strategy
{
    public class JumpMob : IJumpable
    {
        public ReactiveProperty<bool> IsJumpComplete { get; private set; } = new();
        private bool _isJump;
        private Vector3 _currentPosition;
        private ITransformable _transform;
        private CancellationTokenSource _token = new();

        public void Init(ITransformable transform)
        {
            _transform = transform ?? throw new ArgumentNullException();
        }
        
        public async void Jump()
        {
            IsJumpComplete.Value = false;
            if (!_isJump) return;
            _currentPosition = _transform.Transform.position;
            await AnimationJump(_token);
            IsJumpComplete.Value = true;
        }

        private async UniTask AnimationJump(CancellationTokenSource tokenSource)
        {
            await _transform.Transform.DOMove(
                    new Vector3(_currentPosition.x, 3f, _currentPosition.z), 2f)
                .SetEase(Ease.InOutBounce)
                .WithCancellation(tokenSource.Token);
            await _transform.Transform.DOMove(
                    _currentPosition, 2f)
                .SetEase(Ease.InOutBounce).WithCancellation(tokenSource.Token);
        }

        public void StartState() => _isJump = true;

        public void StopState() => _isJump = false;


        ~JumpMob()
        {
            _token.Cancel();
            _token.Dispose();
        }
    }
}