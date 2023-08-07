using UnityEngine;

namespace Sources.Infrastructure
{
    internal class LoadLevelState : IPayloadState<string>
    {
        private const string _spawnPointTag = "SpawnPoint";
        private const string _weaponPath = "Weapon";
        private const string _hudPath = "HUD";

        private readonly GameStateMachine _gameStateMachine;
        private readonly SceneLoader _sceneLoader;

        private Curtain _curtain;

        public LoadLevelState(GameStateMachine gameStateMachine, SceneLoader sceneLoader, Curtain curtain)
        {
            _gameStateMachine = gameStateMachine;
            _sceneLoader = sceneLoader;
            _curtain = curtain;
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
            GameObject spawnPoint = GameObject.FindGameObjectWithTag(_spawnPointTag);

            var weapon = Instantiate(_weaponPath, spawnPoint.transform.position);
            var hud = Instantiate(_hudPath);

            _gameStateMachine.Enter<GameLoopState>();
        }

        private GameObject Instantiate(string path, Vector3 position)
        {
            var prefab = Resources.Load<GameObject>(path);
            return Object.Instantiate(prefab);
        }

        private GameObject Instantiate(string path)
        {
            return Instantiate(path, Vector3.zero);
        }
    }
}
