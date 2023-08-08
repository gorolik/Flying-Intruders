using Sources.Infrastructure.Factory;
using Sources.Infrastructure.Services;
using UnityEngine;

namespace Sources.Infrastructure.States
{
    internal class LoadLevelState : IPayloadState<string>
    {
        private const string _weaponSpawnPointTag = "SpawnPoint";

        private readonly GameStateMachine _gameStateMachine;
        private readonly SceneLoader _sceneLoader;
        private readonly IGameFactory _gameFactory;

        private Curtain _curtain;

        public LoadLevelState(GameStateMachine gameStateMachine, SceneLoader sceneLoader, Curtain curtain, IGameFactory gameFactory)
        {
            _gameStateMachine = gameStateMachine;
            _sceneLoader = sceneLoader;
            _curtain = curtain;
            _gameFactory = gameFactory;
        }

        public void Enter(string sceneName)
        {
            _curtain.Show();

            _sceneLoader.Load(sceneName, OnLevelLoaded);
        }

        public void Exit()
        {
            _curtain.Hide();
        }

        private void OnLevelLoaded()
        {
            GameObject weapon = _gameFactory.CreateWeapon(GameObject.FindGameObjectWithTag(_weaponSpawnPointTag).transform.position);
            _gameFactory.CreateHud();

            _gameStateMachine.Enter<GameLoopState>();
        }
    }
}
