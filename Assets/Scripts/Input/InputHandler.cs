using System.Collections.Generic;
using System.Linq;
using Cysharp.Threading.Tasks;
using Mob;
using Strategy;
using UniRx;
using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;

public class InputHandler : MonoBehaviour, IInitInput
{
    [field: SerializeField] public List<Transform> Points { get; private set; }
    private InputSystem _inputSystem;
    private IMobParameters _mobParameters;
    private ITransformable _transformable;
    private DiContainer _container;
    private bool _isDependenciesInitialized;
    private bool _isJumpComplete;

    private IJumpable _jump;
    private CompositeDisposable _compositeDisposable = new();

    [Inject]
    public void Construct(InputSystem inputSystem, DiContainer container)
    {
        _inputSystem = inputSystem;
        _container = container;
        
        _jump = _container.Resolve<JumpMob>();
    }

    public void Init(ITransformable transformable, IMobParameters parameters)
    {
        _transformable = transformable;
        _mobParameters = parameters;

        _isDependenciesInitialized = true;
    }
    
    private void OnEnable()
    {
        _inputSystem.Enable();
        _inputSystem.MainInput.Follow.performed += Move;
        _inputSystem.MainInput.Rotate.performed += Rotate;
        _inputSystem.MainInput.Jump.performed += Jump;
        _jump.IsJumpComplete
            .Subscribe(x => _isJumpComplete = x)
            .AddTo(_compositeDisposable);
        _isJumpComplete = true;
    }

    private void OnDisable()
    {
        _inputSystem.MainInput.Follow.performed -= Move;
        _inputSystem.MainInput.Rotate.performed -= Rotate;
        _inputSystem.MainInput.Jump.performed -= Jump;
        _inputSystem.Disable();
        _compositeDisposable.Clear();
        _compositeDisposable.Dispose();
    }

    private async UniTaskVoid WaitDependenciesInitialized()
    {
        while (!_isDependenciesInitialized)
        {
            await UniTask.Yield();
        }
    }

    private void Move(InputAction.CallbackContext obj)
    {
        WaitDependenciesInitialized().Forget();

        if(!_isJumpComplete) return;
        
        var follow = _container.Resolve<PointMoveMob>();
        follow.Init(Points.Select(x => x.position), _transformable);
        _mobParameters.SetState(follow);
    }

    private void Rotate(InputAction.CallbackContext obj)
    {
        WaitDependenciesInitialized().Forget();
        if(!_isJumpComplete) return;
        
        var rotate = _container.Resolve<RotateMob>();
        rotate.Init(_transformable);
        _mobParameters.SetState(rotate);
    }

    private void Jump(InputAction.CallbackContext obj)
    {
        WaitDependenciesInitialized().Forget();
        if(!_isJumpComplete) return;
        
        _jump.Init(_transformable);
        _mobParameters.SetState(_jump);
        _jump.Jump();
    }
}