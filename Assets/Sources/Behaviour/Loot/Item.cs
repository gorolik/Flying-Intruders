using Sources.StaticData.Loot;
using UnityEngine;

namespace Sources.Behaviour.Loot
{
    public class Item : MonoBehaviour
    {
        public delegate void ItemCollect(LootType type);
        public static ItemCollect ItemCollected;
        
        private LootData _lootData;
        
        public void Init(LootData lootData) => 
            _lootData = lootData;

        public void Contain(Transform parent) =>
            transform.parent = parent;

        public void Release()
        {
            Collected();
            Destroy(gameObject);
        }

        private void Collected() => 
            ItemCollected?.Invoke(_lootData.Type);
    }
}