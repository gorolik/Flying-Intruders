using Sources.Infrastructure.AssetManagement;
using Sources.Services.StaticData;
using UnityEngine;

namespace Sources.UI.Factory
{
    public class UIFactory : IUIFactory
    {
        private readonly IAssets _assets;
        private readonly IStaticDataService _staticData;

        public Transform UIRoot { get; private set; }

        public UIFactory(IAssets assets, IStaticDataService staticData)
        {
            _assets = assets;
            _staticData = staticData;
        }

        public void CreateUIRoot()
        {
            Transform uiRoot = Object.Instantiate(_assets.GetPrefabByPath(AssetsPath.UIRootPath)).transform;
            UIRoot = uiRoot;
        }

        public void CreatePause() => 
            Object.Instantiate(_staticData.GetWindowById(WindowId.Pause).Prefab, UIRoot);
    }
}