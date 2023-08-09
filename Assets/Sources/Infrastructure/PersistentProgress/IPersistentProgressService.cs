using Sources.Infrastructure.DI;

namespace Sources.Infrastructure.PersistentProgress
{
    public interface IPersistentProgressService : IService
    {
        PlayerProgress PlayerProgress { get; set; }
    }
}