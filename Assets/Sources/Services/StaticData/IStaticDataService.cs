using Sources.Infrastructure.DI;
using Sources.StaticData.Enemy;
using Sources.StaticData.Hole;
using Sources.StaticData.Weapon;

namespace Sources.Services.StaticData
{
    public interface IStaticDataService : IService
    {
        void LoadData();
        HoleData GetHoleData();
        EnemyData GetEnemyDataByType(EnemyType type);
        WeaponData GetWeaponDataByType(WeaponType type);
    }
}