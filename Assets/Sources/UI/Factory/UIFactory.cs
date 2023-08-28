using Sources.Infrastructure;
using Sources.Infrastructure.AssetManagement;
using Sources.Services.StaticData;
using Sources.UI.Windows;
using UnityEngine;

namespace Sources.UI.Factory
{
    public class UIFactory : IUIFactory
    {
        private readonly IAssets _assets;
        private readonly IStaticDataService _staticData;
        private readonly IGameStateMachine _gameStateMachine;

        public Transform UIRoot { get; private set; }

        public UIFactory(IAssets assets, IStaticDataService staticData, IGameStateMachine gameStateMachine)
        {
            _assets = assets;
            _staticData = staticData;
            _gameStateMachine = gameStateMachine;
        }

        public void CreateUIRoot()
        {
            Transform uiRoot = Object.Instantiate(_assets.GetPrefabByPath(AssetsPath.UIRootPath)).transform;
            UIRoot = uiRoot;
        }

        public void CreatePause()
        {
            WindowBase window = InstantiateByWindowId(WindowId.Pause);
            
            PauseWindow pauseWindow = window as PauseWindow;
            pauseWindow.Construct(_gameStateMachine);
        }

        public void CreateGameOver()
        {
            WindowBase window = InstantiateByWindowId(WindowId.GameOver);
            
            GameOverWindow gameOverWindow = window as GameOverWindow;
            gameOverWindow.Construct(_gameStateMachine);
        }
        
        private WindowBase InstantiateByWindowId(WindowId id) =>
            Object.Instantiate(_staticData.GetWindowById(id).Prefab, UIRoot);
    }
}