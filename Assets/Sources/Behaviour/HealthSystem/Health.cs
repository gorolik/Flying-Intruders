using System;
using UnityEngine;

namespace Sources.Behaviour.HealthSystem
{
    public class Health : MonoBehaviour, IHealth
    {
        private float _maxValue;
        private float _currentValue;

        public float MaxValue
        {
            get => _maxValue;
            set
            {
                _maxValue = value;
                
                if (CurrentValue > value)
                    CurrentValue = value;
            }
        }

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

        public void Init(float health)
        {
            MaxValue = health;
            CurrentValue = health;
        }

        public void TakeDamage(float value)
        {
            if (CurrentValue < 0)
                throw new ArgumentOutOfRangeException(value.ToString());

            CurrentValue -= value;
        }
    }
}