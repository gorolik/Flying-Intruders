using Sources.Behaviour;
using Sources.Behaviour.UI;
using Sources.Infrastructure.Factory;
using Sources.Infrastructure.PersistentProgress;
using Sources.StaticData.Weapon;
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

        public LoadLevelState(GameStateMachine gameStateMachine, SceneLoader sceneLoader, Curtain curtain, IGameFactory gameFactory, IPersistentProgressService progressService)
        {
            _gameStateMachine = gameStateMachine;
            _sceneLoader = sceneLoader;
            _curtain = curtain;
            _gameFactory = gameFactory;
            _progressService = progressService;
        }

        public void Enter(string sceneName)
        {
            _curtain.Show();
            _gameFactory.CleanUp();
            
            _sceneLoader.Load(sceneName, OnLevelLoaded);
        }

        public void Exit()
        {
            _curtain.Hide();
        }

        private void OnLevelLoaded()
        {
            InitGameWorld();
            InformProgressReaders();
            
            _gameStateMachine.Enter<GameLoopState>();
        }

        private void InformProgressReaders()
        {
            foreach (ISavedProgressReader progressReader in _gameFactory.SavedProgressReaders)
                progressReader.LoadProgress(_progressService.PlayerProgress);
        }

        private void InitGameWorld()
        {
            _gameFactory.CreateHole();
            _gameFactory.CreateWeapon(_startWeaponType, GameObject.FindGameObjectWithTag(_weaponSpawnPointTag).transform.position);
            _gameFactory.CreateHud();
        }
    }
}
