using Sources.Infrastructure.DI;
using UnityEngine;

namespace Sources.Infrastructure.AssetManagement
{
    public interface IAssets : IService
    {
        GameObject Instantiate(GameObject prefab, Vector3 position);
        GameObject Instantiate(string path, Vector3 position);
        GameObject GetPrefabByPath(string path);
    }
}