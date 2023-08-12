using UnityEngine;

namespace Sources.StaticData.Difficult
{
    [CreateAssetMenu(fileName = "DifficultData", menuName = "Static Data/Difficult Data")]
    public class DifficultData : ScriptableObject
    {
        [SerializeField] private float _difficultPerSecond;
        [SerializeField] private float _startEnemySpawnCoolDown;
        public float DifficultPerSecond => _difficultPerSecond;
        public float StartEnemySpawnCooldown => _startEnemySpawnCoolDown;
    }
}