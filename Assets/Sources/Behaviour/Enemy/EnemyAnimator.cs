using UnityEngine;

namespace Sources.Behaviour.Enemy
{
    [RequireComponent(typeof(Animator))]
    public class EnemyAnimator : MonoBehaviour
    {
        public static readonly int SpeedHash = Animator.StringToHash("Speed");
        public static readonly int DieHash = Animator.StringToHash("Die");
        public static readonly int FlewHash = Animator.StringToHash("Flew");

        private Animator _animator;

        private void Start()
        {
            _animator = GetComponent<Animator>();
        }

        public void Die() => 
            _animator.SetTrigger(DieHash);

        public void Flew() =>
            _animator.SetTrigger(FlewHash);
    }
}