using System.Threading;
using Cysharp.Threading.Tasks;
using UniRx;

namespace Mob
{
    public interface IJumpable : ICurrentState
    {
        void Jump();
        ReactiveProperty<bool> IsJumpComplete { get; }
        void Init(ITransformable transform);
    }
}