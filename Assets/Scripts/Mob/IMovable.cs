using System.Collections.Generic;
using UnityEngine;

namespace Mob
{
    public interface IMovable : ICurrentState
    {
        float Speed { get; }
        void Move();
    }
}