using System;
using System.Collections.Generic;
using Sources.Behaviour.Enemy;
using Sources.Behaviour.HealthSystem;
using Sources.Behaviour.UI;
using Sources.Infrastructure.AssetManagement;
using Sources.Infrastructure.PersistentProgress;
using Sources.Services.StaticData;
using Sources.StaticData;
using Sources.StaticData.Enemy;
using Unity.Mathematics;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Sources.Infrastructure.Factory
{
    public class GameFactory : IGameFactory
    {
        private readonly IAssets _assets;
        private readonly IStaticDataService _staticData;

        private Transform _hole;
        
        public List<ISavedProgressReader> SavedProgressReaders { get; } = new List<ISavedProgressReader>();
        public List<ISavedProgressUpdater> SavedProgressUpdaters { get; } = new List<ISavedProgressUpdater>();
        
        public event Action HoleCreated;

        public GameFactory(IAssets assetProvider, IStaticDataService staticData)
        {
            _assets = assetProvider;
            _staticData = staticData;
        }

        public void CreateHud()
        {
            GameObject hud = CreateGameObject(AssetsPath.HudPath);

            PlayerHealthUIView playerHealthUIView = hud.GetComponentInChildren<PlayerHealthUIView>();
            playerHealthUIView.Construct(_hole.GetComponent<IHealth>());
        }

        public void CreateWeapon(Vector2 position) =>
            CreateGameObject(AssetsPath.WeaponPath, position);

        public GameObject CreateEnemy(EnemyType type, Transform parent, Vector2 position)
        {
            EnemyData enemyData = _staticData.GetEnemyDataByType(type);
            GameObject enemy = Object.Instantiate(enemyData.Prefab, position, quaternion.identity, parent);

            MovingToHole mover = enemy.GetComponent<MovingToHole>();
            mover.Construct(_hole);
            mover.Init(enemyData.Speed);

            EnemyDamager damager = enemy.GetComponent<EnemyDamager>();
            damager.Construct(_hole);
            damager.Init(enemyData.Damage, enemyData.DamageDistance);

            Health health = enemy.GetComponent<Health>();
            health.Init(enemyData.Health);

            return enemy;
        }

        public GameObject CreateProjectile(Vector2 position) => 
            CreateGameObject(AssetsPath.ProjectilePath, position);

        public void CreateHole()
        {
            _hole = CreateGameObject(AssetsPath.HolePath, Vector2.zero).transform;
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