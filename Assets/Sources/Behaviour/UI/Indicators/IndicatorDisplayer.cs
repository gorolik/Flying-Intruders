using UnityEngine;

namespace Sources.Behaviour.UI.Indicators
{
    public class IndicatorDisplayer : MonoBehaviour
    {
        [SerializeField] private Indicator[] _indicators;
        [SerializeField] private Color _enabledIndicatorColor = Color.white;
        [SerializeField] private Color _disabledIndicatorColor = Color.white;

        private bool _indicatorsInited;
        private int _previousValue;
        
        protected void Display(int value)
        {
            if (!_indicatorsInited)
                InitIndicators();
            
            if(value == _previousValue)
                return;

            if (value > _previousValue)
                for (int i = _previousValue; i < value; i++)
                    _indicators[i].Highlight();
            
            else if (value < _previousValue)
                for (int i = _previousValue - 1; i >= value; i--)
                    _indicators[i].Fade();

            _previousValue = value;
        }

        private void InitIndicators()
        {
            foreach (Indicator indicator in _indicators)
            {
                indicator.Init(_enabledIndicatorColor, _disabledIndicatorColor);
                indicator.Fade();
            }

            _indicatorsInited = true;
        }
    }
}