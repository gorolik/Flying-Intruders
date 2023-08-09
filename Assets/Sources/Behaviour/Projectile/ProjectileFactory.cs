using Sources.Infrastructure.DI;
using Sources.Infrastructure.Factory;
using UnityEngine;

namespace Sources.Behaviour.Projectile
{
    public class ProjectileFactory : MonoBehaviour
    {
        private IGameFactory _gameFactory;

        private void Start()
        {
            GetGameFactory();
        }

        public void CreateProjectile(ProjectileProperties propertyes, Vector2 position, Vector2 direction)
        {
            ProjectileUnit projectile = _gameFactory.CreateProjectile(position).GetComponent<ProjectileUnit>();
            projectile.Init(propertyes, direction);
        }

        private void GetGameFactory() => 
            _gameFactory = AllServices.Container.Single<IGameFactory>();
    }
}