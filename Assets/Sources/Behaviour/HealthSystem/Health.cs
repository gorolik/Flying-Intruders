using System;
using UnityEngine;

namespace Sources.Behaviour.HealthSystem
{
    public class Health : MonoBehaviour, IHealth
    {
        [SerializeField] private float _maxValue = 50;

        public float CurrentValue { get; set; }

        public float MaxValue 
            => _maxValue;

        public event Action<float> OnHealthChanged;

        private void Start()
        {
            CurrentValue = MaxValue;
        }

        public void TakeDamage(float value)
        {
            if (CurrentValue < 0)
                throw new ArgumentOutOfRangeException(value.ToString());

            CurrentValue = Mathf.Clamp(CurrentValue - value, 0, float.MaxValue);

            OnHealthChanged?.Invoke(CurrentValue);
        }
    }
}