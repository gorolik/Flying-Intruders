using Sources.Behaviour.Weapon;

namespace Sources.Behaviour.UI.Indicators
{
    public class WeaponGradeUIView : IndicatorDisplayer
    {
        private WeaponUpgrader _weaponUpgrader;
    
        public void Construct(WeaponUpgrader weaponUpgrader)
        {
            _weaponUpgrader = weaponUpgrader;
            _weaponUpgrader.WeaponUpgraded += Display;
            
            Display(_weaponUpgrader.Grade);
        }

        private void OnDestroy() => 
            _weaponUpgrader.WeaponUpgraded -= Display;
    }
}
