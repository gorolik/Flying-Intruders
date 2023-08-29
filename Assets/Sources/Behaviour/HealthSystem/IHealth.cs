using System;

namespace Sources.Behaviour.HealthSystem
{
    public interface IHealth : IDamagable
    {
        float CurrentValue { get; set; }
        float MaxValue { get; }
        
        event Action<float> OnHealthChanged;
        void TakeHealth(int value);
    }
}