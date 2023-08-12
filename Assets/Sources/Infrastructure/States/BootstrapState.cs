using Sources.Infrastructure.AssetManagement;
using Sources.Infrastructure.DI;
using Sources.Infrastructure.Factory;
using Sources.Infrastructure.PersistentProgress;
using Sources.Services.Difficult;
using Sources.Services.Input;
using Sources.Services.StaticData;
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

        public void Exit() {}

        private void EnterLoadLevel() => 
            _gameStateMachine.Enter<LoadProgressState>();

        private void RegisterServices()
        {
            RegisterStaticDataService();
            RegisterDifficultService();
            _services.RegisterSingle<IInputSurvice>(GetInputService());
            _services.RegisterSingle<IAssets>(new AssetProvider());
            _services.RegisterSingle<IPersistentProgressService>(new PersistentProgressService());
            _services.RegisterSingle<IGameFactory>(new GameFactory(_services.Single<IAssets>(), _services.Single<IStaticDataService>(), _services.Single<IInputSurvice>()));
            _services.RegisterSingle<ISaveLoadService>(new SaveLoadService(_services.Single<IPersistentProgressService>(), _services.Single<IGameFactory>()));
        }

        private void RegisterDifficultService()
        {
            float difficultPerSecond = _services.Single<IStaticDataService>().GetDifficultData().DifficultPerSecond;
            _services.RegisterSingle<IDifficultService>(new DifficultService(difficultPerSecond));
        }

        private IInputSurvice GetInputService()
        {
            if (SystemInfo.deviceType == DeviceType.Desktop)
                return new StandaloneInputService();
            else
                return new MobileInputService();
        }

        private void RegisterStaticDataService()
        {
            IStaticDataService staticDataService = new StaticDataService();
            staticDataService.LoadData();
            _services.RegisterSingle<IStaticDataService>(staticDataService);
        }
    }
}
