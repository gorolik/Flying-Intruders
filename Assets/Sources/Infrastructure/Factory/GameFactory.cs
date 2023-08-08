using System.Collections.Generic;
using Sources.Infrastructure.AssetManagement;
using Sources.Infrastructure.PersistentProgress;
using UnityEngine;

namespace Sources.Infrastructure.Factory
{
    public class GameFactory : IGameFactory
    {
        private readonly IAssets _assets;

        public List<ISavedProgressReader> _savedProgressReaders { get; } = new List<ISavedProgressReader>();
        public List<ISavedProgressUpdater> _savedProgressUpdaters { get; } = new List<ISavedProgressUpdater>();

        public GameFactory(IAssets assetProvider) =>
            _assets = assetProvider;

        public void CreateHud() =>
            CreateGameObject(AssetsPath.HudPath);

        public GameObject CreateWeapon(Vector2 position) =>
            CreateGameObject(AssetsPath.WeaponPath, position);

        public void CleanUp()
        {
            _savedProgressReaders.Clear();
            _savedProgressUpdaters.Clear();
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
                    _savedProgressUpdaters.Add(updater);
                
                _savedProgressReaders.Add(component);
            }
        }
    }
}