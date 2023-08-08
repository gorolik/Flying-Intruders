using UnityEngine;

namespace Sources.Infrastructure
{
    public interface IGameFactory
    {
        void CreateHud();
        GameObject CreateWeapon();
    }
}