using UnityEngine;

namespace Sources.Behaviour.UI.Indicators
{
    [RequireComponent(typeof(Animator))]
    public class IndicatorAnimator : MonoBehaviour
    {
        public static readonly int HighlightHash = Animator.StringToHash("Highlight");
        public static readonly int FadeHash = Animator.StringToHash("Fade");
        
        private Animator _animator;

        public void Init() =>
            _animator = GetComponent<Animator>();

        public void Highlight() =>
            _animator.SetTrigger(HighlightHash);

        public void Fade() =>
            _animator.SetTrigger(FadeHash);
    }
}