using UnityEngine;

namespace Sources.Behaviour.Enemy.Move
{
    public class SinMoving : EnemyMoving
    {
        private Vector2 _sinDirection;

        protected override void OnInited() => 
            _sinDirection = Quaternion.Euler(0, 0, -90) * TargetDirection;

        protected override void Move()
        {
            var verticalSpeed = MoveData.VerticalSpeed + MoveData.VerticalSpeed * Mod;
            var amplitude = MoveData.Amplitude;
            
            float verticalOffset = Mathf.Sin(Time.time * verticalSpeed);
            Vector2 sinTranslation = _sinDirection * (verticalOffset * amplitude);
            Vector2 directTranslation = TargetDirection * (Speed * Time.deltaTime);

            Vector3 translation = directTranslation + sinTranslation;
            transform.position += translation;
        }
    }
}