using System.Collections.Generic;
using Sources.Behaviour.Enemy;
using Sources.Behaviour.HealthSystem;
using Sources.Behaviour.Projectile;
using Sources.Behaviour.UI;
using Sources.Behaviour.Weapon;
using Sources.Infrastructure.AssetManagement;
using Sources.Infrastructure.PersistentProgress;
using Sources.Services.Input;
using Sources.Services.StaticData;
using Sources.StaticData.Enemy;
using Sources.StaticData.Hole;
using Sources.StaticData.Weapon;
using Unity.Mathematics;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Sources.Infrastructure.Factory
{
    public class GameFactory : IGameFactory
    {
        private readonly IAssets _assets;
        private readonly IStaticDataService _staticData;
        private readonly IInputSurvice _inputSurvice;

        private GameObject _hole;
        
        public List<ISavedProgressReader> SavedProgressReaders { get; } = new List<ISavedProgressReader>();
        public List<ISavedProgressUpdater> SavedProgressUpdaters { get; } = new List<ISavedProgressUpdater>();
        
        public GameFactory(IAssets assetProvider, IStaticDataService staticData, IInputSurvice inputSurvice)
        {
            _assets = assetProvider;
            _staticData = staticData;
            _inputSurvice = inputSurvice;
        }

        public void CreateHole()
        {
            HoleData holeData = _staticData.GetHoleData();
            
            _hole = CreateGameObject(AssetsPath.HolePath, Vector2.zero);

            Health health = _hole.GetComponent<Health>();
            health.Init(holeData.Health);
        }

        public void CreateHud()
        {
            GameObject hud = CreateGameObject(AssetsPath.HudPath);

            PlayerHealthUIView playerHealthUIView = hud.GetComponentInChildren<PlayerHealthUIView>();
            playerHealthUIView.Construct(_hole.GetComponent<IHealth>());
        }

        public void CreateWeapon(WeaponType type, Vector2 position)
        {
            WeaponData weaponData = _staticData.GetWeaponDataByType(type);
            GameObject weapon = CreateGameObject(weaponData.Prefab, position);

            WeaponAimer aimer = weapon.GetComponent<WeaponAimer>();
            aimer.Construct(_inputSurvice);
            
            WeaponShooter shooter = weapon.GetComponent<WeaponShooter>();
            shooter.Construct(this, _inputSurvice);
            shooter.Init(weaponData.ProjectileProperties, weaponData.Cooldown);
        }

        public GameObject CreateProjectile(ProjectileProperties properties, Vector2 position, Vector2 startDirection)
        {
            GameObject projectile = CreateGameObject(properties.Prefab, position);

            ProjectileMover mover = projectile.GetComponent<ProjectileMover>();
            mover.Init(startDirection, properties.Speed);
            
            ProjectileDamager damager = projectile.GetComponent<ProjectileDamager>();
            damager.Init(properties.Damage);
            
            return projectile;
        }

        public GameObject CreateEnemy(EnemyType type, Transform parent, Vector2 position)
        {
            EnemyData enemyData = _staticData.GetEnemyDataByType(type);
            GameObject enemy = Object.Instantiate(enemyData.Prefab, position, quaternion.identity, parent);

            MovingToHole mover = enemy.GetComponent<MovingToHole>();
            mover.Construct(_hole.transform);
            mover.Init(enemyData.Speed);

            EnemyDamager damager = enemy.GetComponent<EnemyDamager>();
            damager.Construct(_hole.transform);
            damager.Init(enemyData.Damage, enemyData.DamageDistance);

            Health health = enemy.GetComponent<Health>();
            health.Init(enemyData.Health);

            ScoreCollector scoreCollector = enemy.GetComponentInChildren<ScoreCollector>();
            scoreCollector.Init(enemyData.Score);
            
            return enemy;
        }

        public void CleanUp()
        {
            SavedProgressReaders.Clear();
            SavedProgressUpdaters.Clear();
        }
        
        private GameObject CreateGameObject(GameObject prefab, Vector2 position)
        {
            GameObject gameObject = _assets.Instantiate(prefab, position); 
            RegisterObject(gameObject);
            
            return gameObject;
        }        
        
        private GameObject CreateGameObject(string path, Vector2 position) => 
            CreateGameObject(_assets.GetPrefabByPath(path), position);

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