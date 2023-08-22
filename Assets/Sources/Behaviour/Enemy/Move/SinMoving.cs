using Sources.Behaviour.Extensions;
using UnityEngine;

namespace Sources.Behaviour.Enemy.Move
{
    public class SinMoving : EnemyMoving
    {
        private Vector2 _normal;
        private Vector2 _startPos;
        private float _startTime;

        protected override void OnInited()
        {
            _normal = Hole.position - transform.position;
            _startPos = transform.position;
            _startTime = Time.time;
        }

        protected override void Move()
        {
            Vector2 direction = (Hole.position - transform.position).normalized;
            float sinDelta = Mathf.Sin(Time.time);
            float amplitude = 0.1f;

            float time = Time.time - _startTime;
            float x = _startPos.x + Speed * time;
            float y = _startPos.y + amplitude * Mathf.Sin(Speed * time);
            transform.position = new Vector3(x, y, 0);
            
            transform.LookAt2D((Vector2)transform.position + direction);
        }
    }
}