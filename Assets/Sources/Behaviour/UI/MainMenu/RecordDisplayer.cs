using Sources.Infrastructure.PersistentProgress;
using TMPro;
using UnityEngine;

namespace Sources.Behaviour.UI.MainMenu
{
    public class RecordDisplayer : MonoBehaviour
    {
        [SerializeField] private TMP_Text _display;
        [SerializeField] private string _prefix = "Your best score: ";

        public void Init(int bestScore) => 
            _display.text = _prefix + bestScore;
    }
}