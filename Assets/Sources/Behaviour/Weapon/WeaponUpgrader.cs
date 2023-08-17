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
        private int _grade;
        private WeaponType _currentType;
        private Vector2 _spawnPosition;

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
            _grade++;

            if (_grade > 5 && (int)_currentType < Enum.GetNames(typeof(WeaponType)).Length - 1)
                ChangeWeapon(_currentType + 1);
            else if(_grade <= 5)
                _weapon.Shooter.Upgrade(_grade);
        }

        private void ChangeWeapon(WeaponType type)
        {
            _grade = 0;
            
            if(_weapon != null)
                Destroy(_weapon.gameObject);
            
            _weapon = _gameFactory.CreateWeapon(type, _spawnPosition).GetComponent<WeaponUnit>();
            _currentType = type;
        }
    }
}