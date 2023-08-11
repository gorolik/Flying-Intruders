using Sources.Behaviour.HealthSystem;
using TMPro;
using UnityEngine;

namespace Sources.Behaviour.UI
{
    public class PlayerHealthUIView : MonoBehaviour
    {
        [SerializeField] private TMP_Text _display;
        [SerializeField] private string _prefix = "Health: ";
        
        private IHealth _health;

        public void Construct(IHealth health)
        {
            _health = health;
            _health.OnHealthChanged += Display;
            
            Display(_health.CurrentValue);
        }

        private void OnDestroy() => 
            _health.OnHealthChanged -= Display;

        private void Display(float value) => 
            _display.text = _prefix + value;
    }
}
