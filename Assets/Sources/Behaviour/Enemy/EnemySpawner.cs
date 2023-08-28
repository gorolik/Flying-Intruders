using System.Collections;
using System.Collections.Generic;
using Sources.Behaviour.Extensions;
using Sources.Infrastructure.DI;
using Sources.Infrastructure.Factory;
using Sources.Services.Difficult;
using Sources.StaticData.Enemy;
using Sources.StaticData.Loot;
using Sources.StaticData.Spawner;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Sources.Behaviour.Enemy
{
    public class EnemySpawner : MonoBehaviour
    {
        private const float _xOffset = 1.5f;

        private float _startTime;
        private IGameFactory _gameFactory;
        private IDifficultService _difficultService;
        private Camera _camera;
        private Vector2 _screenSize;
        private SpawnerData _data;
        private int _upgradeIteration = 2;

        public void Construct(IGameFactory gameFactory, IDifficultService difficultService)
        {
            _gameFactory = gameFactory;
            _difficultService = difficultService;
        }

        public void Init(SpawnerData spanwerData) => 
            _data = spanwerData;

        private void Start()
        {
            _camera = Camera.main;
            _screenSize = GetScreenFrame(_camera);
            GetGameFactory();
            StartCoroutine(EnemySpawnCycle());
        }

        private IEnumerator EnemySpawnCycle()
        {
            _startTime = Time.time;

            while (true)
            {
                float difficultValue = GetDifficultValue();
                
                print("DV: " + difficultValue + ", SR: " + GetSpawnCooldown(difficultValue));

                yield return new WaitForSeconds(GetSpawnCooldown(difficultValue));

                SpawnEnemy(difficultValue);
            }
        }

        private float GetDifficultValue() =>
            _difficultService.GetDifficult(GetPlayTime());

        private float GetSpawnCooldown(float difficultValue) => 
            Mathf.Clamp(_data.StartEnemySpawnCoolDown * GameFormulas.CalculatePercentIncrease(-_data.SpawnCooldownDifficultPercent, difficultValue), _data.MinSpawnCoolDown, _data.StartEnemySpawnCoolDown);

        private float GetPlayTime() =>
            Time.time - _startTime;

        private void SpawnEnemy(float difficultValue)
        {
            GameObject enemy = _gameFactory.CreateEnemy(GetRandomEnemyTypeByDifficult(difficultValue), transform, GetRandomSpawnPoint(), difficultValue);

            TryGiveUpgradeLoot(difficultValue, enemy);
        }

        private bool TryGiveUpgradeLoot(float difficultValue, GameObject enemy)
        {
            if (difficultValue >= _data.UpgradeEnemyStep * _upgradeIteration)
            {
                _upgradeIteration++;
                _gameFactory.CreateEnemyLoot(enemy, LootType.UpgradeKit);
                return true;
            }

            return false;
        }

        private EnemyType GetRandomEnemyTypeByDifficult(float difficult)
        {
            List<EnemyType> availableTypes = new List<EnemyType>();
            List<float> probabilities = new List<float>();
            
            float totalProbability = 0;

            foreach (EnemySpawnData spawnData in _data.EnemySpawnData)
            {
                if (difficult >= spawnData.MinDifficult)
                {
                    availableTypes.Add(spawnData.Type);
                    
                    float probability = 1 / Mathf.Abs(spawnData.PreferredDifficult - difficult);
                    probability *= spawnData._spawnFrequency;
                    probabilities.Add(probability);
                    totalProbability += probability;
                }
            }

            for (int i = 0; i < probabilities.Count; i++) 
                probabilities[i] /= totalProbability;

            float randomValue = Random.Range(0f, 1f);
            float cumulativeProbability = 0;

            for (int i = 0; i < probabilities.Count; i++)
            {
                cumulativeProbability += probabilities[i];
                
                if (randomValue <= cumulativeProbability)
                    return availableTypes[i];
            }

            return availableTypes[availableTypes.Count - 1];
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
            new Vector2(camera.ScreenToWorldPoint(Vector3.right * Screen.width).x,
                camera.ScreenToWorldPoint(Vector3.up * Screen.height).y);

        private IGameFactory GetGameFactory() =>
            _gameFactory = AllServices.Container.Single<IGameFactory>();
    }
}