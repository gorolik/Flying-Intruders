using System.Collections.Generic;
using Sources.Behaviour.Enemy;
using Sources.Behaviour.HealthSystem;
using Sources.Behaviour.Loot;
using Sources.Behaviour.Projectile;
using Sources.Behaviour.UI;
using Sources.Behaviour.Weapon;
using Sources.Infrastructure.AssetManagement;
using Sources.Infrastructure.PersistentProgress;
using Sources.Services.Difficult;
using Sources.Services.Input;
using Sources.Services.StaticData;
using Sources.StaticData.Difficult;
using Sources.StaticData.Enemy;
using Sources.StaticData.Hole;
using Sources.StaticData.Loot;
using Sources.StaticData.Weapon;
using Sources.StaticData.Weapon.Grade;
using Sources.UI;
using Sources.UI.Services;
using UnityEngine;

namespace Sources.Infrastructure.Factory
{
    public class GameFactory : IGameFactory
    {
        private readonly IAssets _assets;
        private readonly IStaticDataService _staticData;
        private readonly IInputSurvice _inputSurvice;
        private readonly IWindowService _windowService;

        private GameObject _hole;
        private WeaponUpgrader _weaponUpgrader;

        public List<ISavedProgressReader> SavedProgressReaders { get; } = new List<ISavedProgressReader>();
        public List<ISavedProgressUpdater> SavedProgressUpdaters { get; } = new List<ISavedProgressUpdater>();
        
        public GameFactory(IAssets assetProvider, IStaticDataService staticData, IInputSurvice inputSurvice, IWindowService windowService)
        {
            _assets = assetProvider;
            _staticData = staticData;
            _inputSurvice = inputSurvice;
            _windowService = windowService;
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

            WeaponGradeUIView weaponGradeUIView = hud.GetComponentInChildren<WeaponGradeUIView>();
            weaponGradeUIView.Construct(_weaponUpgrader);

            foreach (OpenWindowButton openWindowButton in hud.GetComponentsInChildren<OpenWindowButton>())
                openWindowButton.Construct(_windowService);
        }

        public GameObject CreateWeapon(WeaponType type, Vector2 position)
        {
            GradeData gradeData = _staticData.GetGradeData();
            WeaponData weaponData = _staticData.GetWeaponDataByType(type);
            GameObject weapon = CreateGameObject(weaponData.Prefab, position);

            WeaponAimer aimer = weapon.GetComponent<WeaponAimer>();
            aimer.Construct(_inputSurvice);
            
            WeaponShooter shooter = weapon.GetComponent<WeaponShooter>();
            shooter.Construct(this, _inputSurvice, gradeData.GradeProperties);
            shooter.Init(weaponData.ProjectileProperties, weaponData.Cooldown, weaponData.Spead, weaponData.ProjectilesCount);

            WeaponUnit unit = weapon.GetComponent<WeaponUnit>();
            unit.Init(type);
            
            return weapon;
        }

        public void CreateWeaponUpgrader(WeaponType startWeaponType, Vector2 weaponPosition)
        {
            GameObject weaponUpgraderObject = CreateGameObject(AssetsPath.WeaponUpgrader);

            WeaponUpgrader upgrader = weaponUpgraderObject.GetComponent<WeaponUpgrader>();
            upgrader.Construct(this, startWeaponType, weaponPosition);

            _weaponUpgrader = upgrader;
        }

        public void CreateProjectile(ProjectileProperties properties, Vector2 position, Vector2 startDirection)
        {
            GameObject projectile = CreateGameObject(properties.Prefab, position);

            ProjectileMover mover = projectile.GetComponent<ProjectileMover>();
            mover.Init(startDirection, properties.Speed);
            
            ProjectileDamager damager = projectile.GetComponent<ProjectileDamager>();
            damager.Init(properties.Damage);
        }

        public GameObject CreateEnemy(EnemyType type, Transform parent, Vector2 position, float difficultValue)
        {
            DifficultData difficultData = _staticData.GetDifficultData();
            EnemyData enemyData = _staticData.GetEnemyDataByType(type);
            
            GameObject enemy = CreateGameObject(enemyData.Prefab, position);
            enemy.transform.parent = parent;

            MovingToHole mover = enemy.GetComponent<MovingToHole>();
            mover.Construct(_hole.transform);
            mover.Init(enemyData.Speed + enemyData.Speed * difficultValue * difficultData.EnemyDifficultSpeedRatio);

            EnemyDamager damager = enemy.GetComponent<EnemyDamager>();
            damager.Construct(_hole.transform);
            damager.Init(enemyData.Damage, enemyData.DamageDistance);

            Health health = enemy.GetComponent<Health>();
            health.Init(enemyData.Health + enemyData.Health * difficultValue * difficultData.EnemyDifficultHealthRatio);

            ScoreCollector scoreCollector = enemy.GetComponentInChildren<ScoreCollector>();
            scoreCollector.Init(enemyData.Score);

            return enemy;
        }

        public void CreateEnemyLoot(GameObject enemy, LootType lootType)
        {
            LootData lootData = _staticData.GetLootDataByType(lootType);
            EnemyLootContainer lootContainer = enemy.GetComponentInChildren<EnemyLootContainer>();

            Item item = CreateGameObject(lootData.Prefab, lootContainer.transform.position).GetComponent<Item>();
            item.Init(lootData);
            
            lootContainer.Init(item);
        }

        public void CreateEnemySpawner(IDifficultService difficult)
        {
            DifficultData difficultData = _staticData.GetDifficultData();
            GameObject spawnerObject = CreateGameObject(AssetsPath.EnemySpawnerPath);

            EnemySpawner spawner = spawnerObject.GetComponent<EnemySpawner>();
            spawner.Construct(this, difficult);
            spawner.Init(difficultData.SpawnerData);
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