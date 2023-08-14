using Sources.StaticData.Weapon;
using UnityEngine;

namespace Sources.Behaviour.Weapon
{
    public class WeaponUnit : MonoBehaviour
    {
        [SerializeField] private WeaponShooter _shooter;

        private WeaponType _type;
        
        public WeaponShooter Shooter => _shooter;
        public WeaponType Type => _type;

        public void Init(WeaponType type) => 
            _type = type;
    }
}