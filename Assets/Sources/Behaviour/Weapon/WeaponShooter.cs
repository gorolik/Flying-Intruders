using System;
using Sources.Behaviour.Projectile;
using Sources.Infrastructure.Factory;
using Sources.Services.Input;
using Sources.StaticData.Weapon.Grade;
using UnityEngine;
using UnityEngine.EventSystems;
using static Sources.Behaviour.Extensoins.GameFormulas;
using Random = UnityEngine.Random;

namespace Sources.Behaviour.Weapon
{
    public class WeaponShooter : MonoBehaviour
    {
        [SerializeField] private Transform _muzzlePoint;
        [Header("Audio")] 
        [SerializeField] private AudioSource _audioSource;
        [SerializeField] private AudioClip _shotSound;
        
        private ProjectileProperties _projectileProperties;
        private IInputSurvice _inputSurvice;
        private IGameFactory _gameFactory;
        private EventSystem _eventSystem;
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

        private void Start() => 
            _eventSystem = EventSystem.current;

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

            _currentCooldown = _startCooldown * CalculatePercentIncrease(-_gradeProperties.CooldownGradePercent, grade);
            _spread *= CalculatePercentIncrease(-_gradeProperties.SpreadGradePercent, grade);
            
            _projectileProperties.Damage *= CalculatePercentIncrease(_gradeProperties.ProjectileDamageGradePercent, grade);
            _projectileProperties.Speed *= CalculatePercentIncrease(_gradeProperties.ProjectileSpeedGradePercent, grade);
        }

        private void Shoot()
        {
            _cooldownTimer = _currentCooldown;

            for (int i = 0; i < _projectilesCount; i++) 
                CreateProjectile(i, _projectilesCount);

            Shot?.Invoke();
            
            _audioSource.PlayOneShot(_shotSound);
        }

        private void CreateProjectile(int currentProjectileId, int totalProjectiles) =>
            _gameFactory.CreateProjectile(_projectileProperties, _muzzlePoint.position,
                GetDirection(_muzzlePoint.up, currentProjectileId, totalProjectiles));

        private Vector2 GetDirection(Vector2 muzzleDirection, int currentProjectileId, int totalProjectiles)
        {
            if (totalProjectiles > 1 && currentProjectileId == 0)
                return muzzleDirection;
            
            float deviation = Random.Range(-_spread, _spread);
            float angle = Vector2.SignedAngle(Vector2.up, muzzleDirection) + deviation;
            Vector2 direction = Quaternion.Euler(0, 0, angle) * Vector3.up;

            return direction;
        }

        private bool CanShoot() =>
            _inputSurvice.IsClicked && IsCooldownUp() && !_eventSystem.IsPointerOverGameObject();

        private bool IsCooldownUp() =>
            _cooldownTimer <= 0;
    }
}