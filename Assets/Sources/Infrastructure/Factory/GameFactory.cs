using System;
using System.Collections.Generic;
using Sources.Infrastructure.AssetManagement;
using Sources.Infrastructure.PersistentProgress;
using UnityEngine;

namespace Sources.Infrastructure.Factory
{
    public class GameFactory : IGameFactory
    {
        private readonly IAssets _assets;

        public List<ISavedProgressReader> SavedProgressReaders { get; } = new List<ISavedProgressReader>();
        public List<ISavedProgressUpdater> SavedProgressUpdaters { get; } = new List<ISavedProgressUpdater>();
        public Transform Hole { get; set; }
        public event Action HoleCreated;

        public GameFactory(IAssets assetProvider) =>
            _assets = assetProvider;

        public void CreateHud() =>
            CreateGameObject(AssetsPath.HudPath);

        public void CreateWeapon(Vector2 position) =>
            CreateGameObject(AssetsPath.WeaponPath, position);

        public GameObject CreateProjectile(Vector2 position) => 
            CreateGameObject(AssetsPath.ProjectilePath, position);

        public GameObject CreateEnemy(Vector2 position) => 
            CreateGameObject(AssetsPath.FlyEnemyPath, position);

        public void CreateHole()
        {
            Hole = CreateGameObject(AssetsPath.HolePath, Vector2.zero).transform;
            HoleCreated?.Invoke();
        }

        public void CleanUp()
        {
            SavedProgressReaders.Clear();
            SavedProgressUpdaters.Clear();
        }
        
        private GameObject CreateGameObject(string path, Vector2 position)
        {
            GameObject gameObject = _assets.Instantiate(path, position); 
            RegisterObject(gameObject);
            
            return gameObject;
        }

        private GameObject CreateGameObject(string path) 
            => CreateGameObject(path, Vector2.zero);

        private void RegisterObject(GameObject gameObject)
        {
            foreach (var component in gameObject.GetComponentsInChildren<ISavedProgressReader>())
            {
                if(component is ISavedProgressUpdater updater)
                    SavedProgressUpdaters.Add(updater);
                
                SavedProgressReaders.Add(component);
            }
        }
    }
}