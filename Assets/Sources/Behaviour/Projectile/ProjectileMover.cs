using System;
using UnityEngine;

namespace Sources.Behaviour.Projectile
{
    public class ProjectileMover : MonoBehaviour
    {
        private const int _flyerLayer = 6;
        private const float _destroyDelay = 0.1f;

        private Vector2 _direction;
        private float _projectileSpeed;
        private float _damage;
        private bool _inited;
        private Vector2 _previousPosition;
        private int _layerMask;

        public event Action<RaycastHit2D> OnCollided; 

        public void Init(Vector2 direction, float projectileSpeed, float damage)
        {
            _direction = direction;
            _projectileSpeed = projectileSpeed;
            _damage = damage;

            _layerMask = 1 << _flyerLayer;
            
            _inited = true;
        }

        private void Update() => 
            MoveAndTryCollide();

        private void MoveAndTryCollide()
        {
            if (_inited == true)
            {
                Move();
                TryCollide();
            }
        }

        private void Move()
        {
            _previousPosition = transform.position;
            transform.Translate(_direction * (_projectileSpeed * Time.deltaTime));
        }

        private void TryCollide()
        {
            if (IsCollided(out RaycastHit2D hit))
            {
                transform.position = hit.point;
                Destroy(gameObject, _destroyDelay);
                
                OnCollided?.Invoke(hit);
            }
        }

        private bool IsCollided(out RaycastHit2D hit)
        {
            Vector2 direction = _previousPosition - (Vector2)transform.position;
            float distance = Vector2.Distance(transform.position, _previousPosition);
            
            hit = Physics2D.Raycast(transform.position, direction, distance, _layerMask);
            
            return hit.collider != null;
        }
    }
}