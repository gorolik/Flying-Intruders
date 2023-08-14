using UnityEngine;

namespace Sources.StaticData.Loot
{
    [CreateAssetMenu(fileName = "LootData", menuName = "Static Data/Loot Data")]
    public class LootData : ScriptableObject
    {
        [SerializeField] private LootType _type;
        [SerializeField] private GameObject _prefab;

        public LootType Type => _type;
        public GameObject Prefab => _prefab;
    }
}