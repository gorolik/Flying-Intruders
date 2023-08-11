using Sources.Behaviour.Extensions;
using Sources.Infrastructure.DI;
using Sources.Infrastructure.Factory;
using UnityEngine;

namespace Sources.Behaviour.Enemy
{
    public class MovingToHole : MonoBehaviour
    {
        private float _speed;
        private Transform _hole;
        
        public float Speed => _speed;

        public void Construct(Transform hole) =>
            _hole = hole;

        public void Init(float speed) =>
            _speed = speed;

        private void Update() =>
            Move();

        private void Move()
        {
            Vector3 direction = _hole.position - transform.position.normalized;
            transform.Translate(direction * (_speed * Time.deltaTime), Space.World);
            transform.LookAt2D(transform.position + direction);
        }
    }
}