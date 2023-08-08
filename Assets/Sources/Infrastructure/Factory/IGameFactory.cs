using Sources.Infrastructure.Services;
using UnityEngine;

namespace Sources.Infrastructure.Factory
{
    public interface IGameFactory : IService
    {
        void CreateHud();
        GameObject CreateWeapon(Vector2 position);
    }
}