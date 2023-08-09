﻿using Sources.Infrastructure.DI;
using UnityEngine;

namespace Sources.Infrastructure.AssetManagement
{
    public interface IAssets : IService
    {
        GameObject Instantiate(string path);
        GameObject Instantiate(string path, Vector3 position);
    }
}