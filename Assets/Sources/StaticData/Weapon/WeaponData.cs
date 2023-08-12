using Sources.Behaviour.Projectile;
using UnityEngine;

namespace Sources.StaticData.Weapon
{
    [CreateAssetMenu(fileName = "WeaponData", menuName = "Static Data/Weapon Data")]
    public class WeaponData : ScriptableObject
    {
        [SerializeField] private WeaponType _type;
        [SerializeField] private ProjectileProperties _projectileProperties;
        [SerializeField] private float _cooldown;
        [SerializeField] private GameObject _prefab;

        public WeaponType Type => _type;
        public ProjectileProperties ProjectileProperties => _projectileProperties;
        public float Cooldown => _cooldown;
        public GameObject Prefab => _prefab;
    }
}