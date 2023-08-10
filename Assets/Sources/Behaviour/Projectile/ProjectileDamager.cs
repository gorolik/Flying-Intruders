using Sources.Behaviour.HealthSystem;

namespace Sources.Behaviour.Projectile
{
    public class ProjectileDamager : Damager
    {
        public void Init(float damage) => 
            Damage = damage;
    }
}