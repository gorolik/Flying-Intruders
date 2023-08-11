using Sources.Infrastructure.DI;
using Sources.Infrastructure.PersistentProgress;
using TMPro;
using UnityEngine;

namespace Sources.Behaviour.UI
{
    public class BestScore : MonoBehaviour, ISavedProgressUpdater
    {
        [SerializeField] private Score _score;
        [SerializeField] private TMP_Text _display;
        [SerializeField] private string _displayPrefix = "Best score: ";

        private ISaveLoadService _progressService;
        private int _value;

        private void Awake()
        {
            _progressService = AllServices.Container.Single<ISaveLoadService>();
        }

        private void OnEnable() => 
            _score.ValueChanged += OnScoreChanged;

        private void OnDisable() => 
            _score.ValueChanged -= OnScoreChanged;

        private void OnScoreChanged(int score)
        {
            if (score > _value)
            {
                _value = score;
                _progressService.SaveProgress();
                DisplayValue();
            }
        }

        public void LoadProgress(PlayerProgress progress)
        {
            _value = progress.BestScore;

            DisplayValue();
        }

        public void UpdateProgress(PlayerProgress progress)
        {
            progress.BestScore = _value;
        }

        private string DisplayValue()
        {
            return _display.text = _displayPrefix + _value.ToString();
        }
    }
}