using System.Collections;
using Sources.Infrastructure.DI;
using Sources.Infrastructure.Factory;
using Sources.StaticData.Enemy;
using UnityEngine;

namespace Sources.Behaviour.Enemy
{
    public class EnemySpawner : MonoBehaviour
    {
        private const float _xOffset = 1.5f;

        private IGameFactory _gameFactory;
        private Camera _camera;
        private Vector2 _screenSize;
        private float _cooldown;

        public void Construct(IGameFactory gameFactory) =>
            _gameFactory = gameFactory;

        public void Init(float spawnCooldown) => 
            _cooldown = spawnCooldown;

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

        private void SpawnEnemy() => 
            _gameFactory.CreateEnemy(EnemyType.Fly, transform, GetRandomSpawnPoint());

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
