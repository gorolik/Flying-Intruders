using Sources.Behaviour.Projectile;
using Sources.Infrastructure.DI;
using Sources.Infrastructure.Factory;
using Sources.Services.Input;
using UnityEngine;

namespace Sources.Behaviour.Weapon
{
    public class WeaponShooter : MonoBehaviour
    {
        [SerializeField] private float _cooldown = 1;
        [SerializeField] private Transform _muzzlePoint;
        [SerializeField] private float _projectileSpeed;
        [SerializeField] private float _damage;

        private IInputSurvice _inputSurvice;
        private IGameFactory _gameFactory;
        private float _currentCooldown;

        private void Start()
        {
            GetInputService();
            GetGameFactory();
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

            ProjectileUnit projectile = _gameFactory.CreateProjectile(_muzzlePoint.position).GetComponent<ProjectileUnit>();
            projectile.Init(_muzzlePoint.up, _projectileSpeed, _damage);
        }

        private bool CanShoot() => 
            _inputSurvice.IsClicked && IsCooldownUp();

        private bool IsCooldownUp() => 
            _currentCooldown <= 0;

        private void GetInputService() => 
            _inputSurvice = AllServices.Container.Single<IInputSurvice>();

        private void GetGameFactory() => 
            _gameFactory = AllServices.Container.Single<IGameFactory>();
    }
}