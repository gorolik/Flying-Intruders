using Sources.Infrastructure.AssetManagement;
using Sources.Infrastructure.DI;
using Sources.Infrastructure.Factory;
using Sources.Infrastructure.PersistentProgress;
using Sources.Services.Difficult;
using Sources.Services.Input;
using Sources.Services.StaticData;
using Sources.StaticData.Difficult;
using Sources.UI.Factory;
using Sources.UI.Services;
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
            _sceneLoader.Load(_initialScene, EnterMainMenu);

        public void Exit() {}

        private void EnterMainMenu() => 
            _gameStateMachine.Enter<LoadProgressState>();

        private void RegisterServices()
        {
            RegisterStaticDataService();
            RegisterDifficultService();
            _services.RegisterSingle<IGameStateMachine>(_gameStateMachine);
            _services.RegisterSingle<IInputSurvice>(GetInputService());
            _services.RegisterSingle<IAssets>(new AssetProvider());
            _services.RegisterSingle<IPersistentProgressService>(new PersistentProgressService());
            _services.RegisterSingle<IUIFactory>(new UIFactory(
                _services.Single<IAssets>(),
                _services.Single<IStaticDataService>(),
                _services.Single<IGameStateMachine>()));
            _services.RegisterSingle<IWindowService>(new WindowService(_services.Single<IUIFactory>()));
            _services.RegisterSingle<IMenuFactory>(new MenuFactory(
                _services.Single<IGameStateMachine>(),
                _services.Single<IAssets>(),
                _services.Single<IPersistentProgressService>()));
            _services.RegisterSingle<IGameFactory>(new GameFactory(
                _services.Single<IAssets>(),
                _services.Single<IStaticDataService>(),
                _services.Single<IInputSurvice>(),
                _services.Single<IWindowService>(),
                _services.Single<IUIFactory>()));
            _services.RegisterSingle<ISaveLoadService>(new SaveLoadService(_services.Single<IPersistentProgressService>(), _services.Single<IGameFactory>()));
        }

        private void RegisterDifficultService()
        {
            DifficultData difficultData = _services.Single<IStaticDataService>().GetDifficultData();
            _services.RegisterSingle<IDifficultService>(new DifficultService(difficultData.DifficultPerSecond, difficultData.MaxDifficultValue));
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
