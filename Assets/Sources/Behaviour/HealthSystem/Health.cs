using System;
using UnityEngine;

namespace Sources.Behaviour.HealthSystem
{
    public class Health : MonoBehaviour, IHealth
    {
        [SerializeField] private float _maxValue = 50;

        private float _currentValue;

        public float MaxValue 
            => _maxValue;

        public float CurrentValue
        {
            get => _currentValue;
            set
            {
                float validatedValue = Mathf.Clamp(value, 0, _maxValue);
                _currentValue = validatedValue;
                
                OnHealthChanged?.Invoke(CurrentValue);
            }
        }

        public event Action<float> OnHealthChanged;

        private void Start() => 
            CurrentValue = MaxValue;

        public void TakeDamage(float value)
        {
            if (CurrentValue < 0)
                throw new ArgumentOutOfRangeException(value.ToString());

            CurrentValue -= value;
        }
    }
}