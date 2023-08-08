using Sources.Infrastructure.AssetManagement;
using UnityEngine;

namespace Sources.Infrastructure
{
    public class GameFactory : IGameFactory
    {
        private readonly IAssets _assets;

        public GameFactory(IAssets assetProvider) =>
            _assets = assetProvider;

        public void CreateHud() =>
            _assets.Instantiate(AssetsPath.HudPath);

        public GameObject CreateWeapon(Vector2 position) =>
            _assets.Instantiate(AssetsPath.WeaponPath, position);
    }
}