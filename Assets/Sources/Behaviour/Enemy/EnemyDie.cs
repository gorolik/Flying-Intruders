using System;
using Sources.Behaviour.HealthSystem;
using Unity.VisualScripting;
using UnityEngine;

namespace Sources.Behaviour.Enemy
{
    public class EnemyDie : MonoBehaviour
    {
        [SerializeField] private Health _health;
        [SerializeField] private EnemyAnimator _animator;
        [SerializeField] private MovingToHole _move;
        [SerializeField] private Collider2D _collider;

        private const float _destroyDelay = 5;
        
        private bool _isDied;

        public event Action OnDie;

        private void OnEnable()
        {
            _health.OnHealthChanged += TryDie;
        }

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

            _move.enabled = false;
            _collider.enabled = false;
            
            OnDie?.Invoke();
            
            _animator.Die();
            Destroy(gameObject, _destroyDelay);
        }
    }
}