using Sources.Infrastructure.PersistentProgress;
using TMPro;
using UnityEngine;

namespace Sources.Behaviour
{
    public class BestScore : MonoBehaviour, ISavedProgressUpdater
    {
        [SerializeField] private TMP_Text _display;
        
        private int _value;

        public void LoadProgress(PlayerProgress progress)
        {
            _value = progress.BestScore;

            _display.text = _value.ToString();
        }

        public void UpdateProgress(PlayerProgress progress)
        {
            progress.BestScore = _value;
        }
    }
}