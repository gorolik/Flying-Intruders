using Sources.Behaviour.HealthSystem;
using UnityEngine;

namespace Sources.Behaviour.Enemy
{
    public class EnemyDamager : Damager
    {
        private float _damageDistance;
        private float _damage;
        private Transform _hole;
        private bool _damageGived;

        protected override float Damage => _damage;

        public void Construct(Transform hole) =>
            _hole = hole;

        public void Init(float damage, float damageDistance)
        {
            _damage = damage;
            _damageDistance = damageDistance;
        }

        private void FixedUpdate()
        {
            if (CanAttack() && TryDamage(_hole))
                _damageGived = true;
        }

        private bool CanAttack() =>
            _hole != null && !_damageGived && Vector2.Distance(_hole.position, transform.position) <= _damageDistance;
    }
}