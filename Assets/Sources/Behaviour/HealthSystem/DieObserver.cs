using System;
using UnityEngine;

namespace Sources.Behaviour.HealthSystem
{
    public abstract class DieObserver : MonoBehaviour
    {
        [SerializeField] private Health _health;
        
        private bool _isDied;
        
        public event Action OnDie;

        private void OnEnable() => 
            _health.OnHealthChanged += TryDie;

        private void OnDisable() => 
            _health.OnHealthChanged -= TryDie;
        
        protected void TryDie(float healthValue)
        {
            if (ShouldDie(healthValue))
            {
                _isDied = true;
                OnDie?.Invoke();
                
                Die();
            }
        }

        protected virtual bool ShouldDie(float healthValue) => 
            healthValue <= 0 && !_isDied;

        protected abstract void Die();
    }
}