using UnityEngine;

namespace Sources.Infrastructure
{
    public class GameFactory : IGameFactory
    {
        private const string _spawnPointTag = "SpawnPoint";
        private const string _weaponPath = "Weapon";
        private const string _hudPath = "HUD";

        public void CreateHud()
        {
            var hud = Instantiate(_hudPath);
        }

        public GameObject CreateWeapon()
        {
            GameObject spawnPoint = GameObject.FindGameObjectWithTag(_spawnPointTag);
            return Instantiate(_weaponPath, spawnPoint.transform.position);
        }

        private static GameObject Instantiate(string path, Vector3 position)
        {
            var prefab = Resources.Load<GameObject>(path);
            return Object.Instantiate(prefab);
        }

        private static GameObject Instantiate(string path)
        {
            return Instantiate(path, Vector3.zero);
        }
    }
}