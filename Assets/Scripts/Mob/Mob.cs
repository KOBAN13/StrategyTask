using UnityEngine;

namespace Mob
{
    public class Mob : MonoBehaviour, IMobParameters, ITransformable
    {
        public Transform Transform => transform;
        public ICurrentState State { get; private set; }

        public void SetState(ICurrentState state)
        {
            State?.StopState();
            State = state;
            State.StartState();
        }
    }
}