using Sources.Behaviour.Enemy;
using UnityEngine;

namespace Sources.Behaviour.Loot
{
    public class EnemyLootContainer : MonoBehaviour
    {
        [SerializeField] private EnemyDie _enemyDie;

        private Item _item;

        private void OnEnable() =>
            _enemyDie.OnDie += OnDie;

        private void OnDisable() =>
            _enemyDie.OnDie -= OnDie;

        private void OnDie()
        {
            if(_item == null)
                return;
            
            _item.Release();
        }

        public void Init(Item item)
        {
            _item = item;
            item.Contain(transform);
        }
    }
}