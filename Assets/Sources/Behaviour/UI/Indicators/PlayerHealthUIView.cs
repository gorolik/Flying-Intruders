using Sources.Behaviour.HealthSystem;
using UnityEngine;

namespace Sources.Behaviour.UI.Indicators
{
    public class PlayerHealthUIView : IndicatorDisplayer
    {
        private IHealth _health;

        public void Construct(IHealth health)
        {
            _health = health;
            _health.OnHealthChanged += ConvertedDisplay;
            
            ConvertedDisplay(_health.CurrentValue);
        }

        private void OnDestroy() => 
            _health.OnHealthChanged -= ConvertedDisplay;

        private void ConvertedDisplay(float health) =>
            Display(Mathf.RoundToInt(health));
    }
}
