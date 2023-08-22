using System;
using Sources.Behaviour.Loot;
using Sources.Infrastructure.Factory;
using Sources.StaticData.Loot;
using Sources.StaticData.Weapon;
using UnityEngine;

namespace Sources.Behaviour.Weapon
{
    public class WeaponUpgrader : MonoBehaviour
    {
        private WeaponUnit _weapon;
        private IGameFactory _gameFactory;
        private WeaponType _currentType;
        private Vector2 _spawnPosition;
        private int _grade = 1;

        public int Grade => _grade;
        
        public Action<int> WeaponUpgraded;

        public void Construct(IGameFactory gameFactory, WeaponType startType, Vector2 spawnPosition)
        {
            _gameFactory = gameFactory;
            _spawnPosition = spawnPosition;
            
            ChangeWeapon(startType);
        }

        private void OnEnable() => 
            Item.ItemCollected += OnItemCollected;

        private void OnDisable() => 
            Item.ItemCollected -= OnItemCollected;

        private void OnItemCollected(LootType type)
        {
            if(type == LootType.UpgradeKit)
                Upgrade();
        }

        private void Upgrade()
        {
            if (_grade >= 5 && !GunsOver())
            {
                ChangeWeapon(_currentType + 1);
            }
            else if (_grade < 5)
            {
                _grade++;
                _weapon.Shooter.Upgrade(_grade);
            }
            
            WeaponUpgraded?.Invoke(_grade);
        }

        private bool GunsOver() => 
            (int)_currentType >= Enum.GetNames(typeof(WeaponType)).Length - 1;

        private void ChangeWeapon(WeaponType type)
        {
            _grade = 1;
            
            if(_weapon != null)
                Destroy(_weapon.gameObject);
            
            _weapon = _gameFactory.CreateWeapon(type, _spawnPosition).GetComponent<WeaponUnit>();
            _currentType = type;
        }
    }
}