using UnityEngine;

namespace Sources.Infrastructure.AssetManagement
{
    public class AssetProvider : IAssets
    {
        public GameObject Instantiate(string path, Vector3 position)
        {
            var prefab = Resources.Load<GameObject>(path);
            return Object.Instantiate(prefab);
        }

        public GameObject Instantiate(string path)
        {
            return Instantiate(path, Vector3.zero);
        }
    }
}