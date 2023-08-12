using System.Collections.Generic;
using System.Linq;
using Sources.StaticData.Difficult;
using Sources.StaticData.Enemy;
using Sources.StaticData.Hole;
using Sources.StaticData.Weapon;
using UnityEngine;

namespace Sources.Services.StaticData
{
    public class StaticDataService : IStaticDataService
    {
        private HoleData _holeData;
        private DifficultData _difficultData;
        private Dictionary<EnemyType, EnemyData> _enemys;
        private Dictionary<WeaponType, WeaponData> _weapons;

        public void LoadData()
        {
            LoadHoleData();
            LoadDifficultData();
            LoadEnemysData();
            LoadWeaponData();
        }

        public HoleData GetHoleData() => 
            _holeData;

        public DifficultData GetDifficultData() => 
            _difficultData;

        public EnemyData GetEnemyDataByType(EnemyType type) => 
            _enemys.TryGetValue(type, out EnemyData data) ? data : null;

        public WeaponData GetWeaponDataByType(WeaponType type) => 
            _weapons.TryGetValue(type, out WeaponData data) ? data : null;

        private void LoadHoleData() => 
            _holeData = Resources.Load<HoleData>("StaticData/Hole/Hole");

        private void LoadDifficultData() => 
            _difficultData = Resources.Load<DifficultData>("StaticData/Difficult/DifficultData");

        private void LoadEnemysData()
        {
            _enemys = Resources
                .LoadAll<EnemyData>("StaticData/Enemys")
                .ToDictionary(x => x.Type, x => x);
        }

        private void LoadWeaponData()
        {
            _weapons = Resources
                .LoadAll<WeaponData>("StaticData/Weapons")
                .ToDictionary(x => x.Type, x => x);
        }
    }
}