using UnityEngine;

namespace Sources.Behaviour.Projectile
{
    public class ProjectileUnit : MonoBehaviour
    {
        [SerializeField] private ProjectileMover _mover;
        [SerializeField] private ProjectileDamager _damager;
        [SerializeField] private AudioSource _audioSource;
        [SerializeField] private AudioClip _hitSound;

        public void Init(ProjectileProperties properties, Vector2 direction)
        {
            _mover.Init(direction, properties.Speed);
            _damager.Init(properties.Damage);
        }

        private void OnEnable() => 
            _mover.OnCollided += OnCollided;

        private void OnDisable() => 
            _mover.OnCollided -= OnCollided;

        private void OnCollided(RaycastHit2D hit)
        {
            if (_damager.TryDamage(hit.transform))
            {
                OnDamageGived();
                Destroy(gameObject, _hitSound.length);
            }
            else
                Destroy(gameObject);
        }

        private void OnDamageGived() => 
            _audioSource.PlayOneShot(_hitSound);
    }
}