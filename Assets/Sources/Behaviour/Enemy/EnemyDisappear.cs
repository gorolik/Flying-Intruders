using UnityEngine;

namespace Sources.Behaviour.Enemy
{
    public class EnemyDisappear : MonoBehaviour
    {
        [SerializeField] private EnemyDamager _damager;
        [SerializeField] private MovingToHole _mover;
        [SerializeField] private EnemyAnimator _animator;
        [SerializeField] private Collider2D _collider;

        private bool _disappeared;

        public bool Disappeared => _disappeared;

        private void OnEnable() => 
            _damager.DamageGived += OnDamageGived;

        private void OnDisable() => 
            _damager.DamageGived -= OnDamageGived;

        private void OnDamageGived()
        {
            if(!_disappeared)
                Disappear();
        }

        private void Disappear()
        {
            _disappeared = true;

            _collider.enabled = false;
            _mover.enabled = false;
            _animator.Flew();
        }
    }
}
