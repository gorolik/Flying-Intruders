using UnityEngine;

namespace Sources.Infrastructure.AssetManagement
{
    public interface IAssets
    {
        GameObject Instantiate(string path);
        GameObject Instantiate(string path, Vector3 position);
    }
}