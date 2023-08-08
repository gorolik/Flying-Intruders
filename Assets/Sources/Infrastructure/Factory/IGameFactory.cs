using System.Collections.Generic;
using Sources.Infrastructure.PersistentProgress;
using Sources.Infrastructure.Services;
using UnityEngine;

namespace Sources.Infrastructure.Factory
{
    public interface IGameFactory : IService
    {
        List<ISavedProgressReader> _savedProgressReaders { get; }

        List<ISavedProgressUpdater> _savedProgressUpdaters { get; }
        
        void CreateHud();
        GameObject CreateWeapon(Vector2 position);
        void CleanUp();
    }
}