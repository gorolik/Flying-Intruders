using Sources.Behaviour.HealthSystem;
using Sources.Infrastructure.DI;
using Sources.Infrastructure.Factory;
using TMPro;
using UnityEngine;

namespace Sources.Behaviour.UI
{
    public class PlayerHealthUIView : MonoBehaviour
    {
        [SerializeField] private TMP_Text _display;
        [SerializeField] private string _prefix = "Health: ";
    
        private IGameFactory _gameFactory;
        private IHealth _health;

        private void Start()
        {
            GetGameFactory();
            InitHolePoint();
        }

        private void Display(float value) => 
            _display.text = _prefix + value;

        private void InitHolePoint()
        {
            if (_gameFactory.Hole != null)
                GetHole();
            else
                _gameFactory.HoleCreated += GetHole;
        }

        private void GetGameFactory() => 
            _gameFactory = AllServices.Container.Single<IGameFactory>();

        private void GetHole()
        {
            _health = _gameFactory.Hole.GetComponent<IHealth>();
            _health.OnHealthChanged += Display;
        }
    }
}
