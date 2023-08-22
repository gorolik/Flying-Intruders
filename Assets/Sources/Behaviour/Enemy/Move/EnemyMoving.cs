using UnityEngine;

namespace Sources.Behaviour.Enemy.Move
{
    public abstract class EnemyMoving : MonoBehaviour
    {
        protected float Speed;
        protected Transform Hole;
        public float MoveSpeed => Speed;

        public void Construct(Transform hole) =>
            Hole = hole;

        public void Init(float speed)
        {
            Speed = speed;
            OnInited();
        }

        protected abstract void Move();

        protected virtual void OnInited() {}

        public void Update() => 
            Move();
    }
}