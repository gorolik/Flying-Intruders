using UnityEngine;

namespace Sources.Behaviour.Enemy
{
    public class ScoreCollector : MonoBehaviour
    {
        [SerializeField] private EnemyDie _enemyDie;
        
        private int _score;

        public delegate void Collected(int count);

        public static Collected OnCollected;
        
        public void Init(int score) => 
            _score = score;

        private void OnEnable() => 
            _enemyDie.OnDie += Collect;

        private void OnDisable() => 
            _enemyDie.OnDie -= Collect;

        private void Collect() => 
            OnCollected?.Invoke(_score);
    }
}