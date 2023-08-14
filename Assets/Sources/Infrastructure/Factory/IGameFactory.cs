using System.Collections.Generic;
using Sources.Behaviour.Projectile;
using Sources.Infrastructure.DI;
using Sources.Infrastructure.PersistentProgress;
using Sources.Services.Difficult;
using Sources.StaticData.Enemy;
using Sources.StaticData.Loot;
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
        GameObject CreateWeapon(WeaponType type, Vector2 position);
        void CreateWeaponUpgrader(WeaponType startType, Vector2 weaponSpawnPosition);
        void CreateProjectile(ProjectileProperties properties, Vector2 position, Vector2 startDirection);
        GameObject CreateEnemy(EnemyType type, Transform parent, Vector2 position, float difficultValue);
        void CreateEnemyLoot(GameObject enemy, LootType lootType);
        void CreateEnemySpawner(IDifficultService difficult);
        void CleanUp();
    }
}