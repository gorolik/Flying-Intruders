using System;
using UnityEngine;

namespace Sources.StaticData.Enemy
{
    [CreateAssetMenu(fileName = "EnemyData", menuName = "Static Data/Enemy Data")]
    public class EnemyData : ScriptableObject
    {
        [Header("Main")]
        [SerializeField] private EnemyType _type;
        
        [Header("Health")]
        [Range(1, 100)] [SerializeField] private float _health = 30;
        
        [Header("Attack")]
        [Range(0, 5)] [SerializeField] private float _damage = 1;
        [Range(0.1f, 1)] [SerializeField] private float _damageDistance = 0.3f;

        [Space]
        [Header("Movement")]
        [SerializeField] private MoveType _moveType;
        
        [Header("General")]
        [Range(0.1f, 10)] [SerializeField] private float _speed = 2;
        
        [Header("Sin Moving")]
        [Range(0.1f, 10)] [SerializeField] private float _verticalSpeed = 3;
        [Range(0.001f, 0.01f)] [SerializeField] private float _amplitude = 0.002f;

        [Header("Movement")]
        [SerializeField] private MoveData _moveData;

        [Header("Resources")]
        [SerializeField] private GameObject _prefab;

        [Header("Reward")]
        [SerializeField] private int _score = 1;

        public EnemyType Type => _type;

        public float Health => _health;

        public float Damage => _damage;
        public float DamageDistance => _damageDistance;

        public MoveData MoveData => _moveData;

        public GameObject Prefab => _prefab;

        public int Score => _score;
    }

    [Serializable]
    public struct MoveData
    {
        [SerializeField] private MoveType _moveType;

        [Header("General")]
        [Range(0.1f, 10)] [SerializeField] private float _speed;

        [Header("Sin Moving")]
        [Range(0.1f, 10)] [SerializeField] private float _verticalSpeed;
        [Range(0.001f, 0.01f)] [SerializeField] private float _amplitude;
        
        public MoveType MoveType => _moveType;
        public float Speed => _speed;
        public float VerticalSpeed => _verticalSpeed;
        public float Amplitude => _amplitude;
    }
}