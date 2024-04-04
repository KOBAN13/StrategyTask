using System;
using Mob;
using UnityEngine;
using Zenject;

namespace Strategy
{
    public class RotateMob : IRotatable, ITickable
    {
        private bool _isRotate;
        public float RotationSpeed { get; private set; } = 5f;

        private ITransformable _transform;
        
        
        public void Init(ITransformable transform)
        {
            _transform = transform ?? throw new ArgumentNullException();
        }
        
        public void Rotate()
        {
            _transform.Transform.Rotate(Vector3.up, RotationSpeed * Time.deltaTime);
        }
        
        public void StartState() => _isRotate = true;

        public void StopState() => _isRotate = false;
        
        public void Tick()
        {
            if(_isRotate == false) return;
            
            Rotate();
        }
    }
}