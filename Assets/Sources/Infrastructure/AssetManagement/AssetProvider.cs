using Unity.Mathematics;
using UnityEngine;

namespace Sources.Infrastructure.AssetManagement
{
    public class AssetProvider : IAssets
    {
        public GameObject Instantiate(string path, Vector3 position) => 
            Instantiate(GetPrefabByPath(path), position);

        public GameObject Instantiate(GameObject prefab, Vector3 position) => 
            Object.Instantiate(prefab, position, quaternion.identity);

        public GameObject GetPrefabByPath(string path) => 
            Resources.Load<GameObject>(path);
    }
}