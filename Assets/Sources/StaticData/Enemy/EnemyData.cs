using UnityEngine;

namespace Sources.StaticData.Enemy
{
    [CreateAssetMenu(fileName = "EnemyData", menuName = "Static Data/Enemy Data", order = 0)]
    public class EnemyData : ScriptableObject
    {
        [Header("Main")]
        [SerializeField] private EnemyType _type;
        
        [Header("Health")]
        [Range(1, 100)] [SerializeField] private float _health = 30;
        
        [Header("Attack")]
        [Range(0, 5)] [SerializeField] private float _damage = 1;
        [Range(0.1f, 1)] [SerializeField] private float _damageDistance = 0.3f;

        
        [Header("Movement")]
        [Range(0.1f, 30)] [SerializeField] private float _speed = 20;

        [SerializeField] private MoveType _moveType;

        [Header("Resources")]
        [SerializeField] private GameObject _prefab;

        public EnemyType Type => _type;
        
        public float Health => _health;
        
        public float Damage => _damage;
        public float DamageDistance => _damageDistance;
        
        public float Speed => _speed;
        public MoveType MoveType => _moveType;
        
        public GameObject Prefab => _prefab;
    }
}