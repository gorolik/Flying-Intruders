using Sources.Infrastructure.DI;

namespace Sources.Infrastructure.PersistentProgress
{
    public interface ISaveLoadService : IService
    {
        void SaveProgress();
        PlayerProgress LoadProgress();
    }
}