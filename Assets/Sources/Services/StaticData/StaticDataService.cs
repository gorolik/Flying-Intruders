using System.Collections.Generic;
using System.Linq;
using Sources.StaticData.Difficult;
using Sources.StaticData.Enemy;
using Sources.StaticData.Hole;
using Sources.StaticData.Loot;
using Sources.StaticData.UI;
using Sources.StaticData.Weapon;
using Sources.StaticData.Weapon.Grade;
using Sources.UI;
using UnityEngine;

namespace Sources.Services.StaticData
{
    public class StaticDataService : IStaticDataService
    {
        private HoleData _holeData;
        private DifficultData _difficultData;
        private GradeData _gradeData;
        private Dictionary<EnemyType, EnemyData> _enemys;
        private Dictionary<WeaponType, WeaponData> _weapons;
        private Dictionary<LootType, LootData> _items;
        private Dictionary<WindowId, WindowConfig> _windows;
        
        public void LoadData()
        {
            LoadHoleData();
            LoadDifficultData();
            LoadGradeData();
            LoadEnemysData();
            LoadWeaponData();
            LoadLootData();
            LoadWindowsData();
        }

        public HoleData GetHoleData() => 
            _holeData;

        public DifficultData GetDifficultData() => 
            _difficultData;

        public GradeData GetGradeData() => 
            _gradeData;

        public EnemyData GetEnemyDataByType(EnemyType type) => 
            _enemys.TryGetValue(type, out EnemyData data) ? data : null;

        public WeaponData GetWeaponDataByType(WeaponType type) => 
            _weapons.TryGetValue(type, out WeaponData data) ? data : null;

        public LootData GetLootDataByType(LootType type) => 
            _items.TryGetValue(type, out LootData data) ? data : null;

        public WindowConfig GetWindowById(WindowId id) => 
            _windows.TryGetValue(id, out WindowConfig data) ? data : null;

        private void LoadHoleData() => 
            _holeData = Resources.Load<HoleData>("StaticData/Hole/Hole");

        private void LoadDifficultData() => 
            _difficultData = Resources.Load<DifficultData>("StaticData/Difficult/DifficultData");

        private void LoadGradeData() =>
            _gradeData = Resources.Load<GradeData>("StaticData/Grade/GradeData");

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

        private void LoadLootData()
        {
            _items = Resources
                .LoadAll<LootData>("StaticData/Loot")
                .ToDictionary(x => x.Type, x => x);
        }

        private void LoadWindowsData()
        {
            _windows = Resources
                .Load<WindowsStaticData>("StaticData/Windows/WindowsData")
                .Windows
                .ToDictionary(x => x.WindowId, x => x);
        }
    }
}