using System.Collections.Generic;
using Sources.Infrastructure.DI;
using Sources.Infrastructure.PersistentProgress;
using Sources.StaticData;
using UnityEngine;

namespace Sources.Infrastructure.Factory
{
    public interface IGameFactory : IService
    {
        List<ISavedProgressReader> SavedProgressReaders { get; }
        List<ISavedProgressUpdater> SavedProgressUpdaters { get; }
        
        void CreateHole();
        void CreateHud();
        void CreateWeapon(Vector2 position);
        GameObject CreateEnemy(EnemyType type, Transform parent, Vector2 position);
        GameObject CreateProjectile(Vector2 muzzlePointPosition);
        void CleanUp();
    }
}