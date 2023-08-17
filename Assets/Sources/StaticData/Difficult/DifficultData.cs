using UnityEngine;

namespace Sources.StaticData.Difficult
{
    [CreateAssetMenu(fileName = "DifficultData", menuName = "Static Data/Difficult Data")]
    public class DifficultData : ScriptableObject
    {
        [Header("Difficult Value")]
        [SerializeField] [Range(0, 10)] private float _difficultPerSecond;
        [SerializeField] [Range(1, 10)] private float _startEnemySpawnCoolDown;
        [SerializeField] [Range(1, 50)] private float _maxDifficultValue;

        [Header("Difficult Value")] 
        [SerializeField] [Range(0, 0.1f)] private float _enemyDifficultSpeedRatio;
        [SerializeField] [Range(0, 1f)] private float _enemyDifficultHealthRatio;

        public float DifficultPerSecond => _difficultPerSecond;
        public float StartEnemySpawnCooldown => _startEnemySpawnCoolDown;
        public float MaxDifficultValue => _maxDifficultValue;
        public float EnemyDifficultSpeedRatio => _enemyDifficultSpeedRatio;
        public float EnemyDifficultHealthRatio => _enemyDifficultHealthRatio;

    }
}