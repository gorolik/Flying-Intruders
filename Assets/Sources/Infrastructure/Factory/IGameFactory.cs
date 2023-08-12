using System.Collections.Generic;
using Sources.Behaviour.Projectile;
using Sources.Infrastructure.DI;
using Sources.Infrastructure.PersistentProgress;
using Sources.Services.Difficult;
using Sources.StaticData.Enemy;
using Sources.StaticData.Weapon;
using UnityEngine;

namespace Sources.Infrastructure.Factory
{
    public interface IGameFactory : IService
    {
        List<ISavedProgressReader> SavedProgressReaders { get; }
        List<ISavedProgressUpdater> SavedProgressUpdaters { get; }
        
        void CreateHole();
        void CreateHud();
        void CreateWeapon(WeaponType type, Vector2 position);
        void CreateProjectile(ProjectileProperties properties, Vector2 position, Vector2 startDirection);
        void CreateEnemy(EnemyType type, Transform parent, Vector2 position);
        void CreateEnemySpawner(IDifficultService difficult);
        void CleanUp();
    }
}