using Sources.Behaviour.Enemy;
using Sources.StaticData.Spawner;
using UnityEngine;

namespace Sources.StaticData.Difficult
{
    [CreateAssetMenu(fileName = "DifficultData", menuName = "Static Data/Difficult Data")]
    public class DifficultData : ScriptableObject
    {
        [Header("Difficult Value")]
        [SerializeField] [Range(0, 10)] private float _difficultPerSecond;
        [SerializeField] [Range(1, 50)] private float _maxDifficultValue;

        [Header("Enemy Values")] 
        [SerializeField] [Range(0, 0.1f)] private float _enemyDifficultSpeedRatio;
        [SerializeField] [Range(0, 1f)] private float _enemyDifficultHealthRatio;

        [Header("Spawner Values")] 
        [SerializeField] private SpawnerData _spawnerData;

        public float DifficultPerSecond => _difficultPerSecond;
        public float MaxDifficultValue => _maxDifficultValue;
        public float EnemyDifficultSpeedRatio => _enemyDifficultSpeedRatio;
        public float EnemyDifficultHealthRatio => _enemyDifficultHealthRatio;
        public SpawnerData SpawnerData => _spawnerData;
    }
}