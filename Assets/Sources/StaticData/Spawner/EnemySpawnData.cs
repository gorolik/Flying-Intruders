using System;
using Sources.StaticData.Enemy;
using UnityEngine;

namespace Sources.StaticData.Spawner
{
    [Serializable]
    public struct EnemySpawnData
    {
        [SerializeField] private EnemyType _type;
        [SerializeField] private float _minDifficult;
        [SerializeField] private float _preferredDifficult;
        [SerializeField] [Range(0f, 1f)] public float _spawnFrequency;

        public EnemyType Type => _type;
        public float MinDifficult => _minDifficult;
        public float PreferredDifficult => _preferredDifficult;
        public float SpawnFrequency => _spawnFrequency;
    }
}