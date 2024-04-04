using System;
using Cysharp.Threading.Tasks;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Factory
{
    public class FactoryMob
    {
        private string _mobNameLoad;
        private Object _mob;

        public FactoryMob(string mobNameLoad)
        {
            _mobNameLoad = mobNameLoad ?? throw new ArgumentNullException();
            LoadMob();
        }
        
        private async void LoadMob() => _mob = await Loader.LoadResources<GameObject>(_mobNameLoad);

        public async UniTask<Object> GetMob()
        {
            while (!Loader.IsLoad)
            {
                await UniTask.Yield();
            }

            return _mob;
        }
    }
}