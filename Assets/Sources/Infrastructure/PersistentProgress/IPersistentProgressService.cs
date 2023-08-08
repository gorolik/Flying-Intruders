using Sources.Infrastructure.Services;

namespace Sources.Infrastructure.PersistentProgress
{
    public interface IPersistentProgressService : IService
    {
        PlayerProgress PlayerProgress { get; set; }
    }
}