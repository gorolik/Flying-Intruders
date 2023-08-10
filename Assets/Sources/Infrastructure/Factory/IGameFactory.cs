using System;
using System.Collections.Generic;
using Sources.Infrastructure.DI;
using Sources.Infrastructure.PersistentProgress;
using UnityEngine;

namespace Sources.Infrastructure.Factory
{
    public interface IGameFactory : IService
    {
        List<ISavedProgressReader> SavedProgressReaders { get; }
        List<ISavedProgressUpdater> SavedProgressUpdaters { get; }
        Transform Hole { get; }
        
        event Action HoleCreated;

        void CreateHole();
        void CreateHud();
        void CreateWeapon(Vector2 position);
        GameObject CreateEnemy(Vector2 position);
        GameObject CreateProjectile(Vector2 muzzlePointPosition);
        void CleanUp();
    }
}