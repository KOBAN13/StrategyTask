using Factory;
using Strategy;
using UnityEngine;
using Zenject;

namespace Installers
{
    public class Installer : MonoInstaller
    {
        [field: SerializeField] public InputHandler InputHandler { get; private set; }
        public override void InstallBindings()
        {
            BindInstantiate();
            BindInput();
            BindStrategy();
            BindInputHandler();
        }

        private void BindInstantiate() => Bind<InstantiateMob>();

        private void BindInput() => Bind<InputSystem>();

        private void BindInputHandler() => Container
            .BindInterfacesAndSelfTo<InputHandler>()
            .FromInstance(InputHandler)
            .AsSingle().NonLazy();

        private void BindStrategy()
        {
            Bind<JumpMob>();
            Bind<RotateMob>();
            Bind<PointMoveMob>();
        }

        private void Bind<T>()
        {
            Container
                .BindInterfacesAndSelfTo<T>()
                .AsSingle()
                .NonLazy();
        }

        private void BindAsTransient<T>()
        {
            Container
                .BindInterfacesAndSelfTo<T>()
                .AsTransient()
                .NonLazy();
        }
    }
}