using Sources.Behaviour.Enemy.Move;
using Sources.Behaviour.HealthSystem;
using UnityEngine;

namespace Sources.Behaviour.Enemy
{
    public class EnemyDie : DieObserver
    {
        [SerializeField] private EnemyAnimator _animator;
        [SerializeField] private Collider2D _collider;
        [SerializeField] private AudioSource _audioSource;
        [SerializeField] private AudioClip _dieSound;

        private const float _destroyDelay = 3;

        private EnemyMoving _mover;
        
        public void Construct(EnemyMoving mover) =>
            _mover = mover;
        
        protected override void Die()
        {
            _mover.enabled = false;
            _collider.enabled = false;
            
            _animator.Die();
            _audioSource.PlayOneShot(_dieSound);
            
            Destroy(gameObject, _destroyDelay);
        }
    }
}