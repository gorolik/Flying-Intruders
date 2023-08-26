using Sources.Behaviour.UI.MainMenu;
using Sources.Infrastructure.AssetManagement;
using Sources.Infrastructure.PersistentProgress;
using UnityEngine;

namespace Sources.Infrastructure.Factory
{
    public class MenuFactory : IMenuFactory
    {
        private readonly IGameStateMachine _gameStateMachine;
        private readonly IAssets _assetProvider;
        private readonly IPersistentProgressService _persistentProgress;

        public MenuFactory(IGameStateMachine gameStateMachine, IAssets assetProvider, IPersistentProgressService persistentProgress)
        {
            _gameStateMachine = gameStateMachine;
            _assetProvider = assetProvider;
            _persistentProgress = persistentProgress;
        }

        public void CreateMenuHud()
        {
            GameObject prefab = _assetProvider.GetPrefabByPath(AssetsPath.MainMenuHudPath);
            GameObject hud = _assetProvider.Instantiate(prefab, Vector3.zero);

            PlayButton playButton = hud.GetComponentInChildren<PlayButton>();
            playButton.Construct(_gameStateMachine);

            RecordDisplayer recordDisplayer = hud.GetComponentInChildren<RecordDisplayer>();
            recordDisplayer.Init(_persistentProgress.PlayerProgress.BestScore);
        }
    }
}