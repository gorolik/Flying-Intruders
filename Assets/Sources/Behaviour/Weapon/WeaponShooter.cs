using Sources.Behaviour.Projectile;
using Sources.Infrastructure.DI;
using Sources.Services.Input;
using UnityEngine;

namespace Sources.Behaviour.Weapon
{
    public class WeaponShooter : MonoBehaviour
    {
        [SerializeField] private ProjectileFactory _projectileFactory;
        [SerializeField] private Transform _muzzlePoint;

        private ProjectileProperties _projectileProperties;
        private IInputSurvice _inputSurvice;
        private float _cooldown;
        private float _currentCooldown;

        public void Init(ProjectileProperties properties, float cooldown)
        {
            _projectileProperties = properties;
            _cooldown = cooldown;
        }

        private void Start() => 
            GetInputService();

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
            _projectileFactory.CreateProjectile(_projectileProperties, _muzzlePoint.position, _muzzlePoint.up);

        private bool CanShoot() => 
            _inputSurvice.IsClicked && IsCooldownUp();

        private bool IsCooldownUp() => 
            _currentCooldown <= 0;

        private void GetInputService() => 
            _inputSurvice = AllServices.Container.Single<IInputSurvice>();
    }
}