using Sources.Behaviour.Projectile;
using UnityEngine;

namespace Sources.StaticData.Weapon
{
    [CreateAssetMenu(fileName = "WeaponData", menuName = "Static Data/Weapon Data")]
    public class WeaponData : ScriptableObject
    {
        [SerializeField] private WeaponType _type;
        [SerializeField] private ProjectileProperties _projectileProperties;
        [SerializeField] [Range(0.01f, 3)] private float _cooldown;
        [SerializeField] [Range(0, 30)] private float _spread;
        [SerializeField] [Range(1, 5)] private int _projectileCount;
        [SerializeField] private GameObject _prefab;

        public WeaponType Type => _type;
        public ProjectileProperties ProjectileProperties => _projectileProperties;
        public float Cooldown => _cooldown;
        public float Spead => _spread;
        public int ProjectilesCount => _projectileCount;
        public GameObject Prefab => _prefab;
    }
}