using Sources.Behaviour;
using Sources.Behaviour.UI;
using Sources.Infrastructure.Factory;
using Sources.Infrastructure.PersistentProgress;
using Sources.Services.Difficult;
using Sources.StaticData.Weapon;
using Sources.UI.Factory;
using UnityEngine;

namespace Sources.Infrastructure.States
{
    internal class LoadLevelState : IPayloadState<string>
    {
        private const string _weaponSpawnPointTag = "SpawnPoint";
        private const WeaponType _startWeaponType = WeaponType.Crossbow;

        private readonly GameStateMachine _gameStateMachine;
        private readonly SceneLoader _sceneLoader;
        private readonly IGameFactory _gameFactory;
        private readonly IPersistentProgressService _progressService;
        private readonly Curtain _curtain;
        private readonly IDifficultService _difficultService;
        private readonly IUIFactory _uiFactory;

        public LoadLevelState(GameStateMachine gameStateMachine, SceneLoader sceneLoader, Curtain curtain, IGameFactory gameFactory, IPersistentProgressService progressService, IDifficultService difficultService, IUIFactory uiFactory)
        {
            _gameStateMachine = gameStateMachine;
            _sceneLoader = sceneLoader;
            _curtain = curtain;
            _gameFactory = gameFactory;
            _progressService = progressService;
            _difficultService = difficultService;
            _uiFactory = uiFactory;
        }

        public void Enter(string sceneName)
        {
            _curtain.Show();
            _gameFactory.CleanUp();
            
            _sceneLoader.Load(sceneName, OnLevelLoaded);
        }

        public void Exit() => 
            _curtain.Hide();

        private void OnLevelLoaded()
        {
            InitUIRoot();
            InitGameWorld();
            InformProgressReaders();
            
            _gameStateMachine.Enter<GameLoopState>();
        }

        private void InitUIRoot() => 
            _uiFactory.CreateUIRoot();

        private void InformProgressReaders()
        {
            foreach (ISavedProgressReader progressReader in _gameFactory.SavedProgressReaders)
                progressReader.LoadProgress(_progressService.PlayerProgress);
        }

        private void InitGameWorld()
        {
            _gameFactory.CreateHole();
            _gameFactory.CreateWeaponUpgrader(_startWeaponType, GameObject.FindGameObjectWithTag(_weaponSpawnPointTag).transform.position);
            _gameFactory.CreateHud();
            _gameFactory.CreateEnemySpawner(_difficultService);
        }
    }
}
