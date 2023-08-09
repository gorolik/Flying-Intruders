using System;

namespace Sources.Behaviour.Projectile
{
    [Serializable]
    public struct ProjectileProperties
    {
        public float Speed;
        public float Damage;

        public ProjectileProperties(float speed, float damage)
        {
            Speed = speed;
            Damage = damage;
        }
    }
}