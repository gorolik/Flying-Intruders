using System;
using Sources.Behaviour.Enemy.Move;
using Sources.Behaviour.HealthSystem;
using UnityEngine;

namespace Sources.Behaviour.Enemy
{
    public class EnemyDie : MonoBehaviour
    {
        [SerializeField] private Health _health;
        [SerializeField] private EnemyAnimator _animator;
        [SerializeField] private Collider2D _collider;
        [SerializeField] private AudioSource _audioSource;
        [SerializeField] private AudioClip _dieSound;

        private const float _destroyDelay = 3;

        private EnemyMoving _mover;
        private bool _isDied;

        public event Action OnDie;

        public void Construct(EnemyMoving mover) =>
            _mover = mover;
        
        private void OnEnable() => 
            _health.OnHealthChanged += TryDie;

        private void TryDie(float healthValue)
        {
            if(ShouldDie(healthValue))
                Die();
        }

        private bool ShouldDie(float healthValue) => 
            healthValue <= 0 && !_isDied;

        private void Die()
        {
            _isDied = true;

            _mover.enabled = false;
            _collider.enabled = false;
            
            OnDie?.Invoke();
            
            _animator.Die();
            _audioSource.PlayOneShot(_dieSound);
            
            Destroy(gameObject, _destroyDelay);
        }
    }
}