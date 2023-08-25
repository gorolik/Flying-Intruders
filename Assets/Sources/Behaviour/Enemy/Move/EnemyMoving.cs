using Sources.StaticData.Enemy;
using UnityEngine;

namespace Sources.Behaviour.Enemy.Move
{
    public abstract class EnemyMoving : MonoBehaviour
    {
        private const int _rotationSpeed = 5;
        
        private Vector2 _previousPosition;

        protected MoveData MoveData;
        protected float Speed;
        protected float Mod;
        protected Transform Hole;

        public float MoveSpeed => Speed;
        public Vector2 TargetDirection => (Hole.position - transform.position).normalized;

        public void Construct(Transform hole) =>
            Hole = hole;

        public void Init(MoveData moveData, float mod)
        {
            MoveData = moveData;
            Speed = moveData.Speed + moveData.Speed * mod;
            Mod = mod;
            
            _previousPosition = transform.position;
            
            OnInited();
        }

        private void Update() => 
            MoveAndRotate();

        protected abstract void Move();

        protected virtual void OnInited() {}

        private void MoveAndRotate()
        {
            Move();
            Rotate();

            _previousPosition = transform.position;
        }

        private void Rotate()
        {
            Vector3 direction = transform.position - (Vector3)_previousPosition;
            Quaternion targetRotation = Quaternion.LookRotation(Vector3.forward, direction);
            transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, _rotationSpeed * Time.deltaTime);
        }
    }
}