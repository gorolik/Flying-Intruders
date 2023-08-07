using UnityEngine;

namespace Sources.Infrastructure
{
    internal class LoadLevelState : IPayloadState<string>
    {
        private const string _spawnPointTag = "SpawnPoint";
        private readonly GameStateMachine _gameStateMachine;
        private readonly SceneLoader _sceneLoader;

        public LoadLevelState(GameStateMachine gameStateMachine, SceneLoader sceneLoader)
        {
            _gameStateMachine = gameStateMachine;
            _sceneLoader = sceneLoader;
        }

        public void Enter(string sceneName)
        {
            _sceneLoader.Load(sceneName, OnLevelLoaded);
        }

        public void Exit()
        {

        }

        private void OnLevelLoaded()
        {
            GameObject spawnPoint = GameObject.FindGameObjectWithTag(_spawnPointTag);

            var weapon = Instantiate("Weapon", spawnPoint.transform.position);
            var hud = Instantiate("HUD");
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
