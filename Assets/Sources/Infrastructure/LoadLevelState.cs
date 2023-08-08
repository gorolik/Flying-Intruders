using UnityEngine;

namespace Sources.Infrastructure
{
    internal class LoadLevelState : IPayloadState<string>
    {
        private readonly GameStateMachine _gameStateMachine;
        private readonly SceneLoader _sceneLoader;
        private readonly IGameFactory _gameFactory;

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
            GameObject weapon = _gameFactory.CreateWeapon();
            _gameFactory.CreateHud();

            _gameStateMachine.Enter<GameLoopState>();
        }
    }
}
