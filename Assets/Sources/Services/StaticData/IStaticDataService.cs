﻿using Sources.Infrastructure.DI;
using Sources.StaticData.Difficult;
using Sources.StaticData.Enemy;
using Sources.StaticData.Hole;
using Sources.StaticData.Loot;
using Sources.StaticData.UI;
using Sources.StaticData.Weapon;
using Sources.StaticData.Weapon.Grade;
using Sources.UI;

namespace Sources.Services.StaticData
{
    public interface IStaticDataService : IService
    {
        void LoadData();
        HoleData GetHoleData();
        DifficultData GetDifficultData();
        GradeData GetGradeData();
        EnemyData GetEnemyDataByType(EnemyType type);
        WeaponData GetWeaponDataByType(WeaponType type);
        LootData GetLootDataByType(LootType lootType);
        WindowConfig GetWindowById(WindowId id);
    }
}