using UnityEngine;
using Zenject;

namespace Factory
{
    public class InstantiateMob
    {
        private DiContainer _container;

        [Inject]
        public void Construct(DiContainer container)
        {
            _container = container;
        }
        
        public T InstantiateForComponent<T>(Object prefab)
        {
            return _container.InstantiatePrefabForComponent<T>(prefab);
        }

        public GameObject InstantiateWithPrefab(Object prefab)
        {
            return _container.InstantiatePrefab(prefab);
        }
    }
}