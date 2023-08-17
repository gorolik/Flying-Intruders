using System;
using Sources.Behaviour.Projectile;
using Sources.Infrastructure.Factory;
using Sources.Services.Input;
using Sources.StaticData.Weapon.Grade;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Sources.Behaviour.Weapon
{
    public class WeaponShooter : MonoBehaviour
    {
        [SerializeField] private Transform _muzzlePoint;

        private ProjectileProperties _projectileProperties;
        private IInputSurvice _inputSurvice;
        private IGameFactory _gameFactory;
        private float _startCooldown;
        private float _currentCooldown;
        private float _cooldownTimer;
        private float _spread;
        private int _projectilesCount;
        private GradeProperties _gradeProperties;

        public Action Shot;

        public float CurrentCooldown => _currentCooldown;

        public void Construct(IGameFactory gameFactory, IInputSurvice inputSurvice, GradeProperties gradeProperties)
        {
            _gameFactory = gameFactory;
            _inputSurvice = inputSurvice;
            _gradeProperties = gradeProperties;
        }

        public void Init(ProjectileProperties properties, float startCooldown, float spread, int projectilesCount)
        {
            _projectileProperties = properties;
            _startCooldown = startCooldown;
            _currentCooldown = startCooldown;
            _spread = spread;
            _projectilesCount = projectilesCount;
        }

        private void Update()
        {
            if (!IsCooldownUp())
                _cooldownTimer -= Time.deltaTime;

            if (CanShoot())
                Shoot();
        }

        public void Upgrade(int grade)
        {
            if (grade > 5)
                return;

            _currentCooldown = _startCooldown * Mathf.Pow(1 - _gradeProperties.CooldownGradePercent, grade);

            _projectileProperties.Damage *= Mathf.Pow(1 + _gradeProperties.ProjectileDamageGradePercent, grade);
            _projectileProperties.Speed *= Mathf.Pow(1 + _gradeProperties.ProjectileSpeedGradePercent, grade);
        }

        private void Shoot()
        {
            _cooldownTimer = _currentCooldown;

            for (int i = 0; i < _projectilesCount; i++) 
                CreateProjectile();

            Shot?.Invoke();
        }

        private void CreateProjectile() =>
            _gameFactory.CreateProjectile(_projectileProperties, _muzzlePoint.position,
                GetDirection(_muzzlePoint.up));

        private Vector2 GetDirection(Vector2 muzzleDirection)
        {
            float deviation = Random.Range(-_spread, _spread);
            float angle = Vector2.SignedAngle(Vector2.up, muzzleDirection) + deviation;
            Vector2 direction = Quaternion.Euler(0, 0, angle) * Vector3.up;

            return direction;
        }

        private bool CanShoot() =>
            _inputSurvice.IsClicked && IsCooldownUp();

        private bool IsCooldownUp() =>
            _cooldownTimer <= 0;
    }
}