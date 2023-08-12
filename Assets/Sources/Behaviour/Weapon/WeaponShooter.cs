using Sources.Behaviour.Projectile;
using Sources.Infrastructure.Factory;
using Sources.Services.Input;
using UnityEngine;

namespace Sources.Behaviour.Weapon
{
    public class WeaponShooter : MonoBehaviour
    {
        [SerializeField] private Transform _muzzlePoint;

        private ProjectileProperties _projectileProperties;
        private IInputSurvice _inputSurvice;
        private IGameFactory _gameFactory;
        private float _cooldown;
        private float _currentCooldown;

        public void Construct(IGameFactory gameFactory, IInputSurvice inputSurvice)
        {
            _gameFactory = gameFactory;
            _inputSurvice = inputSurvice;
        }

        public void Init(ProjectileProperties properties, float cooldown)
        {
            _projectileProperties = properties;
            _cooldown = cooldown;
        }
        
        private void Update()
        {
            if (!IsCooldownUp())
                _currentCooldown -= Time.deltaTime;
            
            if (CanShoot())
                Shoot();
        }

        private void Shoot()
        {
            _currentCooldown = _cooldown;

            CreateProjectile();
        }

        private void CreateProjectile() => 
            _gameFactory.CreateProjectile(_projectileProperties, _muzzlePoint.position, _muzzlePoint.up);

        private bool CanShoot() => 
            _inputSurvice.IsClicked && IsCooldownUp();

        private bool IsCooldownUp() => 
            _currentCooldown <= 0;
    }
}