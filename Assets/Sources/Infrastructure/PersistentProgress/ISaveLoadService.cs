using Sources.Infrastructure.Services;

namespace Sources.Infrastructure.PersistentProgress
{
    public interface ISaveLoadService : IService
    {
        void SaveProgress();
        PlayerProgress LoadProgress();
    }
}