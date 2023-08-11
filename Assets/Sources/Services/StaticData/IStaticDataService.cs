using Sources.Infrastructure.DI;
using Sources.StaticData;
using Sources.StaticData.Enemy;

namespace Sources.Services.StaticData
{
    public interface IStaticDataService : IService
    {
        void LoadEnemysData();
        EnemyData GetEnemyDataByType(EnemyType type);
    }
}