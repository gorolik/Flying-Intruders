using System;
using UnityEngine;

namespace Sources.Behaviour.Projectile
{
    [Serializable]
    public struct ProjectileProperties
    {
        public GameObject Prefab;
        public float Speed;
        public float Damage;
    }
}