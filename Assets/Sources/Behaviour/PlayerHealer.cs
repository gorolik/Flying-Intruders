using Sources.Behaviour.HealthSystem;
using Sources.Behaviour.Loot;
using Sources.StaticData.Loot;
using UnityEngine;

namespace Sources.Behaviour
{
    public class PlayerHealer : MonoBehaviour
    {
        [SerializeField] private Health _health;
        [SerializeField] private int _healthPoints;
        
        private void OnEnable() => 
            Item.ItemCollected += OnItemCollected;

        private void OnDisable() => 
            Item.ItemCollected -= OnItemCollected;

        private void OnItemCollected(LootType type)
        {
            if (type == LootType.HealthKit)
                _health.TakeHealth(_healthPoints);
        }
    }
}