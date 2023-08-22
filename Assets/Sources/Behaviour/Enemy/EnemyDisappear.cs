using Sources.Behaviour.Enemy.Move;
using UnityEngine;

namespace Sources.Behaviour.Enemy
{
    public class EnemyDisappear : MonoBehaviour
    {
        [SerializeField] private EnemyDamager _damager;
        [SerializeField] private EnemyAnimator _animator;
        [SerializeField] private Collider2D _collider;
        [SerializeField] private float _destroyDelay = 2;

        private EnemyMoving _mover;
        private bool _disappeared;

        public void Construct(EnemyMoving mover) =>
            _mover = mover;
        
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
            
            Destroy(gameObject, _destroyDelay);
        }
    }
}
