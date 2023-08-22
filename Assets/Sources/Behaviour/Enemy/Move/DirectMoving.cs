using Sources.Behaviour.Extensions;
using UnityEngine;

namespace Sources.Behaviour.Enemy.Move
{
    public class DirectMoving : EnemyMoving
    {
        protected override void Move()
        {
            Vector3 direction = (Hole.position - transform.position).normalized;
            transform.Translate(direction * (Speed * Time.deltaTime), Space.World);
            transform.LookAt2D(transform.position + direction);
        }
    }
}