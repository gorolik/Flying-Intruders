using UnityEngine;

namespace Sources.Behaviour.Projectile
{
    public class ProjectileUnit : MonoBehaviour
    {
        [SerializeField] private ProjectileMover _mover;
        [SerializeField] private ProjectileDamager _damager;

        public void Init(Vector2 moveDirection, float projectileSpeed, float damage)
        {
            _mover.Init(moveDirection, projectileSpeed);
            _damager.Init(damage);
        }

        private void OnEnable() => 
            _mover.OnCollided += OnCollided;

        private void OnDisable() => 
            _mover.OnCollided -= OnCollided;

        private void OnCollided(RaycastHit2D hit) => 
            _damager.TryDamage(hit);
    }
}