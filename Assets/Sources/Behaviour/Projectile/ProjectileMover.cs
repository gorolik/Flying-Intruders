using System;
using Sources.Behaviour.Extensions;
using UnityEngine;

namespace Sources.Behaviour.Projectile
{
    public class ProjectileMover : MonoBehaviour
    {
        private const int _flyerLayer = 6;
        private const float _destroyDelay = 0.01f;

        private Vector2 _direction;
        private Vector2 _previousPosition;
        private float _projectileSpeed;
        private int _layerMask;
        private bool _isInited;
        private bool _isCollided;

        public event Action<RaycastHit2D> OnCollided; 

        public void Init(Vector2 direction, float projectileSpeed)
        {
            _direction = direction;
            _projectileSpeed = projectileSpeed;

            _layerMask = 1 << _flyerLayer;
            transform.LookAt2D((Vector2)transform.position + direction);
            
            _isInited = true;
        }

        private void Update() => 
            MoveAndTryCollide();

        private void MoveAndTryCollide()
        {
            if (ShouldMove())
            {
                Move();
                TryCollide();
            }
        }

        private bool ShouldMove() => 
            _isInited && !_isCollided;

        private void Move()
        {
            _previousPosition = transform.position;
            transform.Translate(_direction * (_projectileSpeed * Time.deltaTime), Space.World);
        }

        private void TryCollide()
        {
            if (IsCollided(out RaycastHit2D hit)) 
                Collided(hit);
        }

        private void Collided(RaycastHit2D hit)
        {
            transform.position = hit.point;
            Destroy(gameObject, _destroyDelay);

            _isCollided = true;
            OnCollided?.Invoke(hit);
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