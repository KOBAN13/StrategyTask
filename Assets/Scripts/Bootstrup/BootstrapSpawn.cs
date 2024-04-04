using Configs;
using Factory;
using UnityEngine;
using Zenject;
using Mob;

namespace Bootstrap
{
    public class BootstrapSpawn : MonoBehaviour
    {
        [field: SerializeField] public MobNameLoad MobName { get; private set; }

        private FactoryMob _factory;
        private InstantiateMob _instantiateMob;
        private IInitInput _initInput;

        [Inject]
        public void Construct(InstantiateMob instantiateMob, IInitInput initInput)
        {
            _instantiateMob = instantiateMob;
            _factory = new FactoryMob(MobName.MobName);

            _initInput = initInput;
            CreateMob();
        }

        private async void CreateMob()
        {
            var mob = await _factory.GetMob();
            var mobWithComponent = _instantiateMob.InstantiateForComponent<Mob.Mob>(mob);
            
            _initInput.Init(mobWithComponent, mobWithComponent);
        }
    }
}