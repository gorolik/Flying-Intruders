using UnityEngine;

namespace Sources.Behaviour.Projectile
{
    public class ProjectileDamager : MonoBehaviour
    {
        private float _damage;

        public void Init(float damage)
        {
            _damage = damage;
        }

        public bool TryDamage(RaycastHit2D hit)
        {
            Debug.Log("Damaged: " + _damage);
            return false;
        }
    }
}