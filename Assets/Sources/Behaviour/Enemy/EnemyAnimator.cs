using Sources.Behaviour.Enemy.Move;
using UnityEngine;
using UnityEngine.Serialization;

namespace Sources.Behaviour.Enemy
{
    [RequireComponent(typeof(Animator))]
    public class EnemyAnimator : MonoBehaviour
    {
        public static readonly int SpeedHash = Animator.StringToHash("Speed");
        public static readonly int DieHash = Animator.StringToHash("Die");
        public static readonly int FlewHash = Animator.StringToHash("Flew");

        private EnemyMoving _mover;
        private Animator _animator;

        public void Construct(EnemyMoving mover) =>
            _mover = mover;
        
        private void Awake() => 
            _animator = GetComponent<Animator>();

        private void Start() => 
            SetMoveSpeed();

        private void SetMoveSpeed() => 
            _animator.SetFloat(SpeedHash, _mover.MoveSpeed);

        public void Die() => 
            _animator.SetTrigger(DieHash);

        public void Flew() =>
            _animator.SetTrigger(FlewHash);
    }
}