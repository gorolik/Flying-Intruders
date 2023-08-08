using Sources.Infrastructure.Services;
using Sources.Infrastructure.AssetManagement;
using Sources.Infrastructure.Factory;
using Sources.Infrastructure.PersistentProgress;
using Sources.Services.Input;
using UnityEngine;

namespace Sources.Infrastructure.States
{
    public class BootstrapState : IState
    {
        private const string _initialScene = "Initial";
        
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

        public void Enter() => 
            _sceneLoader.Load(_initialScene, EnterLoadLevel);

        public void Exit()
        {

        }

        private void EnterLoadLevel()
        {
            _gameStateMachine.Enter<LoadProgressState>();
        }

        private void RegisterServices()
        {
            _services.RegisterSingle<IInputSurvice>(GetInputService());
            _services.RegisterSingle<IAssets>(new AssetProvider());
            _services.RegisterSingle<IPersistentProgressService>(new PersistentProgressService());
            _services.RegisterSingle<IGameFactory>(new GameFactory(_services.Single<IAssets>()));
            _services.RegisterSingle<ISaveLoadService>(new SaveLoadService(_services.Single<IPersistentProgressService>(), _services.Single<IGameFactory>()));
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
