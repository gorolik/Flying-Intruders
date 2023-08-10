using System;
using Sources.Behaviour.HealthSystem;
using Sources.Infrastructure.DI;
using Sources.Infrastructure.Factory;
using UnityEngine;

namespace Sources.Behaviour.Enemy
{
    public class EnemyDamager : Damager
    {
        [SerializeField] private float _attackDistance;
        [SerializeField] private float _damage;

        private Transform _hole;
        private IGameFactory _gameFactory;
        private bool _damageGived;

        protected override float Damage => _damage;

        public Action DamageGived;

        private void Start()
        {
            GetGameFactory();

            if (_gameFactory.Hole != null)
                GetHole();
            else
                _gameFactory.HoleCreated += GetHole;
        }

        private void FixedUpdate()
        {
            if (CanAttack())
            {
                if (TryDamage(_hole))
                {
                    _damageGived = true;
                    DamageGived?.Invoke();
                }
            }
        }

        private bool CanAttack() => 
            _hole != null && !_damageGived && Vector2.Distance(_hole.position, transform.position) <= _attackDistance;

        private void GetGameFactory() => 
            _gameFactory = AllServices.Container.Single<IGameFactory>();

        private void GetHole() => 
            _hole = _gameFactory.Hole;
    }
}