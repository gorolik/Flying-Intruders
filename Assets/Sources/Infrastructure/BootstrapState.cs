using Sources.Infrastructure;
using Sources.Services.Input;
using UnityEngine;

namespace Assets.Sources.Infrastructure
{
    public class BootstrapState : IState
    {
        private const string InitialScene = "Initial";
        private readonly GameStateMachine _gameStateMachine;
        private readonly SceneLoader _sceneLoader;

        public BootstrapState(GameStateMachine gameStateMachine, SceneLoader sceneLoader)
        {
            _gameStateMachine = gameStateMachine;
            _sceneLoader = sceneLoader;
        }

        public void Enter()
        {
            Game.InputSurvice = RegisterInputService();

            _sceneLoader.Load(InitialScene, EnterLoadLevel);
        }

        private void EnterLoadLevel()
        {

        }

        public void Exit()
        {

        }

        private IInputSurvice RegisterInputService()
        {
            if (SystemInfo.deviceType == DeviceType.Desktop)
                return new StandaloneInputService();
            else
                return new MobileInputService();
        }
    }
}
