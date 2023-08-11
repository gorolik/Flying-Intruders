using Sources.Infrastructure.DI;
using Sources.StaticData;
using Sources.StaticData.Enemy;
using Sources.StaticData.Hole;

namespace Sources.Services.StaticData
{
    public interface IStaticDataService : IService
    {
        void LoadHoleData();
        HoleData GetHoleData();
        void LoadEnemysData();
        EnemyData GetEnemyDataByType(EnemyType type);
    }
}