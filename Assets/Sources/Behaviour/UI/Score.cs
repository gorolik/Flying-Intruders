using System;
using Sources.Behaviour.Enemy;
using TMPro;
using UnityEngine;

namespace Sources.Behaviour.UI
{
    public class Score : MonoBehaviour
    {
        [SerializeField] private TMP_Text _display;
        [SerializeField] private string _prefix = "Score: ";

        private int _value;

        public event Action<int> ValueChanged;

        private void Start() => 
            Display();

        private void OnEnable() => 
            ScoreCollector.OnCollected += AddScore;

        private void OnDisable() => 
            ScoreCollector.OnCollected -= AddScore;

        private void AddScore(int count)
        {
            if (count < 0)
                throw new ArgumentOutOfRangeException(count.ToString());
            
            _value += count;
            ValueChanged?.Invoke(_value);
            Display();
        }

        private void Display() => 
            _display.text = _prefix + _value;
    }
}