using System.Collections;
using Sources.Infrastructure.DI;
using Sources.Infrastructure.Factory;
using UnityEngine;

namespace Sources.Behaviour.Enemy
{
    public class EnemyFactory : MonoBehaviour
    {
        [SerializeField] private float _cooldown = 2;
        [SerializeField] private float _xOffset;

        private IGameFactory _gameFactory;
        private Camera _camera;
        private Vector2 _screenSize;

        private void Start()
        {
            _camera = Camera.main;
            _screenSize = GetScreenFrame(_camera);
            GetGameFactory();
            StartCoroutine(EnemySpawnCycle());
        }

        private IEnumerator EnemySpawnCycle()
        {
            while (true)
            {
                yield return new WaitForSeconds(_cooldown);
                
                SpawnEnemy();
            }
        }

        private void SpawnEnemy()
        {
            GameObject enemy = _gameFactory.CreateEnemy(GetRandomSpawnPoint());
        }

        private Vector2 GetRandomSpawnPoint()
        {
            int direction;
            if (Random.Range(0, 2) == 0)
                direction = -1;
            else
                direction = 1;
            
            float xPos = _screenSize.x * direction + _xOffset * direction;
            float yPos = Random.Range(-_screenSize.y, _screenSize.y);
            
            return new Vector2(xPos, yPos);
        }

        private Vector2 GetScreenFrame(Camera camera) => 
             new Vector2(camera.ScreenToWorldPoint(Vector3.right * Screen.width).x, camera.ScreenToWorldPoint(Vector3.up * Screen.height).y);

        private IGameFactory GetGameFactory() => 
            _gameFactory = AllServices.Container.Single<IGameFactory>();
    }
}
