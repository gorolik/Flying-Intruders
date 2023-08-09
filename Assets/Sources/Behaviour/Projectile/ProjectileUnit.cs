using UnityEngine;

namespace Sources.Behaviour.Projectile
{
    public class ProjectileUnit : MonoBehaviour
    {
        [SerializeField] private ProjectileMover _mover;
        [SerializeField] private ProjectileDamager _damager;

        public void Init(ProjectileProperties properties, Vector2 direction)
        {
            _mover.Init(direction, properties.Speed);
            _damager.Init(properties.Damage);
        }

        private void OnEnable() => 
            _mover.OnCollided += OnCollided;

        private void OnDisable() => 
            _mover.OnCollided -= OnCollided;

        private void OnCollided(RaycastHit2D hit) => 
            _damager.TryDamage(hit);
    }
}