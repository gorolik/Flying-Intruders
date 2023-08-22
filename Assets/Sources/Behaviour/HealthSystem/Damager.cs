using System;
using UnityEngine;

namespace Sources.Behaviour.HealthSystem
{
    public abstract class Damager : MonoBehaviour
    {
        protected virtual float Damage { get; set; }
        
        public Action DamageGived;

        public bool TryDamage(Transform target)
        {
            if (target.TryGetComponent(out IDamagable damagable))
            {
                GiveDamage(damagable);
                DamageGived?.Invoke();
                return true;
            }
            else
                return false;
        }

        public void GiveDamage(IDamagable damagable) => 
            damagable.TakeDamage(GetDamage());

        protected virtual float GetDamage() => 
            Damage;
    }
}