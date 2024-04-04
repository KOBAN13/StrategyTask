using UnityEngine;

namespace Configs
{
    [CreateAssetMenu(menuName = "Mobs", fileName = "MobsLoadName")]
    public class MobNameLoad : ScriptableObject
    {
        [field: SerializeField] public string MobName { get; private set; }
    }
}