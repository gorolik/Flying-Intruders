using Sources.Behaviour.Weapon;
using TMPro;
using UnityEngine;

namespace Sources.Behaviour.UI
{
    public class WeaponGradeUIView : MonoBehaviour
    {
        [SerializeField] private TMP_Text _display;
        [SerializeField] private string _prefix = "Grade: ";

        private WeaponUpgrader _weaponUpgrader;
    
    
        public void Construct(WeaponUpgrader weaponUpgrader)
        {
            _weaponUpgrader = weaponUpgrader;
            _weaponUpgrader.WeaponUpgraded += OnUpgrade;
            
            OnUpgrade(_weaponUpgrader.Grade);
        }

        private void OnDestroy() => 
            _weaponUpgrader.WeaponUpgraded -= OnUpgrade;

        private void OnUpgrade(int grade) => 
            _display.text = _prefix + grade;
    }
}
