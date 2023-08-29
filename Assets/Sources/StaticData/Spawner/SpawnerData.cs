using System;
using System.Collections.Generic;
using UnityEngine;

namespace Sources.StaticData.Spawner
{
    [Serializable]
    public struct SpawnerData
    {
        [SerializeField] [Range(1, 10)] private float _startEnemySpawnCoolDown;
        [SerializeField] [Range(0.05f, 0.2f)] private float _minSpawnCoolDown;
        [SerializeField] [Range(0.01f, 0.3f)] private float _spawnCooldownDifficultPercent;
        [SerializeField] [Range(0.1f, 2)] private float _upgradeEnemyStep;
        [SerializeField] [Range(1, 9)] private int _playerHealthLootTrigger;
        [SerializeField] [Range(10, 30)] private float _healthLootCooldown;
        [SerializeField] private List<EnemySpawnData> _enemySpawnData;

        public float StartEnemySpawnCoolDown => _startEnemySpawnCoolDown;
        public float MinSpawnCoolDown => _minSpawnCoolDown;
        public float SpawnCooldownDifficultPercent => _spawnCooldownDifficultPercent;
        public float UpgradeEnemyStep => _upgradeEnemyStep;
        public int PlayerHealthLootTrigger => _playerHealthLootTrigger;
        public float HealthLootCooldown => _healthLootCooldown;
        public IEnumerable<EnemySpawnData> EnemySpawnData => _enemySpawnData;
    }
}