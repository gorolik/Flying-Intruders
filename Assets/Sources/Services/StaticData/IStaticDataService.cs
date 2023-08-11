using Sources.Infrastructure.DI;
using Sources.StaticData;

namespace Sources.Services.StaticData
{
    public interface IStaticDataService : IService
    {
        void LoadEnemysData();
        EnemyData GetEnemyDataByType(EnemyType type);
    }
}