using UnityEngine;
using UnityEngine.UI;

namespace Sources.Behaviour.UI.Indicators
{
    [RequireComponent(typeof(Image))]
    [RequireComponent(typeof(IndicatorAnimator))]
    public class Indicator : MonoBehaviour
    {
        private Image _image;
        private IndicatorAnimator _animator;
        private Color _highlighColor;
        private Color _fadeColor;

        public bool Inited => _image != null;

        public void Init(Color highlighColor, Color fadeColor)
        {
            _fadeColor = fadeColor;
            _highlighColor = highlighColor;

            _image = GetComponent<Image>();
            _animator = GetComponent<IndicatorAnimator>();
            _animator.Init();
        }
        
        public void Highlight()
        {
            _image.color = _highlighColor;
            _animator.Highlight();   
        }

        public void Fade()
        {
            _image.color = _fadeColor;
            _animator.Fade();
        }
    }
}