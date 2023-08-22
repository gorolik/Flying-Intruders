using UnityEngine;

namespace Sources.Behaviour.Weapon.Animators
{
    [RequireComponent(typeof(Animator))]
    public class CrossbowAnimator : MonoBehaviour
    {
        [SerializeField] private WeaponShooter _shooter;
        
        public static readonly int ShotHash = Animator.StringToHash("Shot");
        private static readonly int ReloadSpeedHash = Animator.StringToHash("ReloadSpeed");

        private Animator _animator;

        private void Awake() =>
            _animator = GetComponent<Animator>();
        
        private void OnEnable() =>
            _shooter.Shot += OnShot;

        private void OnDisable() =>
            _shooter.Shot += OnShot;

        private void OnShot()
        {
            _animator.SetFloat(ReloadSpeedHash, 1 / _shooter.CurrentCooldown);
            _animator.SetTrigger(ShotHash);
        }
    }
}
