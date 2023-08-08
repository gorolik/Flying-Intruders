using Sources.Infrastructure.Services;
using Sources.Infrastructure.AssetManagement;
using Sources.Infrastructure.Factory;
using Sources.Services.Input;
using UnityEngine;

namespace Sources.Infrastructure.States
{
    public class BootstrapState : IState
    {
        private const string InitialScene = "Initial";
        private readonly GameStateMachine _gameStateMachine;
        private readonly SceneLoader _sceneLoader;
        private readonly AllServices _services;

        public BootstrapState(GameStateMachine gameStateMachine, SceneLoader sceneLoader, AllServices services)
        {
            _gameStateMachine = gameStateMachine;
            _sceneLoader = sceneLoader;
            _services = services;

            RegisterServices();
        }

        public void Enter()
        {
            _sceneLoader.Load(InitialScene, EnterLoadLevel);
        }

        public void Exit()
        {

        }

        private void EnterLoadLevel()
        {
            _gameStateMachine.Enter<LoadLevelState, string>("Game");
        }

        private void RegisterServices()
        {
            _services.RegisterSingle<IInputSurvice>(GetInputService());
            _services.RegisterSingle<IAssets>(new AssetProvider());
            _services.RegisterSingle<IGameFactory>(new GameFactory(AllServices.Container.Single<IAssets>()));
        }

        private IInputSurvice GetInputService()
        {
            if (SystemInfo.deviceType == DeviceType.Desktop)
                return new StandaloneInputService();
            else
                return new MobileInputService();
        }
    }
}
