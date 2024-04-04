using System;
using System.Collections.Generic;
using Mob;
using UnityEngine;
using UnityEngine.UIElements;
using Zenject;

namespace Strategy
{
    public class PointMoveMob : IMovable, ITickable
    {
        public float Speed { get; private set; } = 10f;
        private const float MinDistanceToPoint = 0.1f;
        private bool _isStart;
        private ITransformable _transform;
        private Queue<Vector3> _points;
        private Vector3 _currentTarget;

        public void Init(IEnumerable<Vector3> points, ITransformable transform)
        {
            if (points == null) throw new ArgumentNullException();
            _points = new Queue<Vector3>(points);
            
            _transform = transform ?? throw new ArgumentNullException();
        }

        public void Move()
        {
            var direction = _currentTarget - _transform.Transform.position;
            _transform.Transform.Translate(direction.normalized * Speed * Time.deltaTime);
            
            if(direction.magnitude <= MinDistanceToPoint)
                SwitchTarget();
        }
        
        public void Tick()
        {
            if(_isStart == false) return;
            
            Move();
        }

        public void StartState() => _isStart = true;

        public void StopState() => _isStart = false;

        private void SwitchTarget()
        {
            if(_currentTarget != null)
                _points.Enqueue(_currentTarget);

            _currentTarget = _points.Dequeue();
        }
    }
}